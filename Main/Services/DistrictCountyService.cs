using System.Text.RegularExpressions;
using TwseDataHub.Services.Common;
using LC.Models.Contexts;
using LC.Models.Entities;

namespace TwseDataHub.Services
{
    /// <summary>
    /// 處理縣市的服務 - 介面
    /// </summary>
    public interface IDistrictCountyService<T> : IFetchDataService<T>
    {

    }

    /// <summary>
    /// 處理縣市的服務
    /// </summary>
    public class DistrictCountyService : IDistrictCountyService<List<County>?>
    {
        /// <summary>政府開放資料平台 - 鄉鎮市區界線(TWD97經緯度)</summary>
        private const string apiUrl = "https://data.moi.gov.tw/MoiOD/System/DownloadFile.aspx?DATA=72874C55-884D-4CEA-B7D6-F60B0BE85AB0";
        /// <summary>檔案基底路徑</summary>
        private const string basePath = "Files/DistrictCounty";
        /// <summary>新檔案資料夾</summary>
        private const string newDirName = "New";
        /// <summary>原檔案資料夾</summary>
        private const string currentDirName = "Current";
        /// <summary>檢查碼檔案名稱</summary>
        private const string checksumFilename = "Checksum.txt";

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpClientFactory _httpClientFactory;        
        private readonly IFileOperateService _fileOperateService;
        private readonly IShapefileReaderService _shapefileReaderService;
        
        /// <summary>建構式</summary>
        public DistrictCountyService(
            IServiceScopeFactory serviceScopeFactory,
            IHttpClientFactory httpClientFactory,            
            IFileOperateService fileOperateService,
            IShapefileReaderService shapefileReaderService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory)); ;            
            _fileOperateService = fileOperateService;
            _shapefileReaderService = shapefileReaderService;            
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync() {
            if(!await GetDataAsync())
            {
                return;
            }

            var dataList = await ProcessDataAsync();

            await SaveDataAsync(dataList);                               
        }

        /// <inheritdoc/>
        public async Task<bool> GetDataAsync()
        {
            #region 客製化部分(取得API資料)
            using var httpClient = _httpClientFactory.CreateClient();
            var result = await httpClient.GetAsync(apiUrl);
            #endregion 客製化部分(取得API資料)

            
            var newChecksumText = _fileOperateService.SaveAndCalculateChecksum(result.Content.ReadAsStream(), $"{basePath}/{newDirName}/Data.zip", checksumFilename);
            var currentChecksumText = _fileOperateService.ReadTextFile($"{basePath}/{currentDirName}/{checksumFilename}");
            if (newChecksumText == currentChecksumText)
            {
                _fileOperateService.DeleteDirectory($"{basePath}/{newDirName}");
                return false;
            }

            return true;

           
        }

        /// <inheritdoc/>
        public async Task<List<County?>> ProcessDataAsync()
        {
            #region 客製化部分
            _fileOperateService.ExtractFile($"{basePath}/{newDirName}/Data.zip", $"{basePath}/Data");

            var pattern = @"^COUNTY_MOI.*\.shp$";
            var dataDir = new DirectoryInfo($"{basePath}/Data");
            var fileInfos = dataDir.GetFiles();
            var shpfile = fileInfos
                .Where(f => Regex.IsMatch(f.Name, pattern))
                .FirstOrDefault();
            var dataResult = _shapefileReaderService.ReadCounty(shpfile.FullName);
            _fileOperateService.DeleteDirectory($"{basePath}/Data");
            #endregion 客製化部分

            _fileOperateService.DeleteDirectory($"{basePath}/{currentDirName}");
            _fileOperateService.MoveDirectory($"{basePath}/{newDirName}", $"{basePath}/{currentDirName}");

            return dataResult;
        }

        /// <inheritdoc/>
        public async Task SaveDataAsync(List<County> dataList)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<DatabaseContext>();
                var existData = await db.County.ToListAsync();

                var modifyData = new ModifyDataModel<County>
                {
                    WillDelete = existData.Except(dataList,
                        (e, i) => e.Name == i.Name &&                                   
                                  e.CountyID == i.CountyID && 
                                  e.CountyCode == i.CountyCode &&                                   
                                  e.EngName == i.EngName).ToList(),
                    WillInsert = dataList.Except(existData,
                        (i, e) => e.Name == i.Name &&
                                  e.CountyID == i.CountyID &&
                                  e.CountyCode == i.CountyCode &&
                                  e.EngName == i.EngName).ToList(),
                    WillUpdate = existData.Intersect(dataList,
                        (i, e) => e.Name == i.Name &&
                                  e.CountyID == i.CountyID &&
                                  e.CountyCode == i.CountyCode &&
                                  e.EngName == i.EngName).ToList(),
                };

                //Delete
                var deleteTask = Task.Run(() => db.County.RemoveRange(modifyData.WillDelete));
                //Insert
                var insertTask = db.County.AddRangeAsync(modifyData.WillInsert);
                //Update
                var updateTask = Task.Run(() => modifyData.WillUpdate.ForEach(d =>
                {
                    d.Geom = dataList.FirstOrDefault(a => a.CountyCode == d.CountyCode)?.Geom ?? null;
                }));

                //儲存
                await Task.WhenAll(deleteTask, insertTask, updateTask);
                var tx = await db.Database.BeginTransactionAsync();
                await db.SaveChangesAsync();
                await tx.CommitAsync();
            }
        }
    }
}
