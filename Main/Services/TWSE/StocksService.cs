using System.Text.RegularExpressions;
using TwseDataHub.Services.Common;
using LC.Models.Contexts;
using LC.Models.Entities;

namespace TwseDataHub.Services.TWSE
{
    /// <summary>
    /// 上市個股資訊服務 - 介面
    /// </summary>
    public interface IStocksService<T> : IFetchDataService<T>
    {

    }

    /// <summary>
    /// 上市個股資訊服務
    /// </summary>
    public class StocksService : IStocksService<(List<Stocks>?, List<StockDaily>?)>
    {
        /// <summary>TWSE openapi - 上市個股日成交資訊</summary>
        private const string apiUrl = "https://openapi.twse.com.tw/v1/exchangeReport/STOCK_DAY_ALL";
        /// <summary>檔案基底路徑</summary>
        private const string basePath = "Files/Stocks";
        /// <summary>新檔案資料夾</summary>
        private const string newDirName = "New";
        /// <summary>原檔案資料夾</summary>
        private const string currentDirName = "Current";
        /// <summary>檢查碼檔案名稱</summary>
        private const string checksumFilename = "Checksum.txt";

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFileOperateService _fileOperateService;

        /// <summary>建構式</summary>
        public StocksService(
            IServiceScopeFactory serviceScopeFactory,
            IHttpClientFactory httpClientFactory,
            IFileOperateService fileOperateService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory)); ;
            _fileOperateService = fileOperateService;
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync()
        {
            if (!await GetDataAsync())
            {
                return;
            }

            var dataTuple = await ProcessDataAsync();
            if (dataTuple.Item1 != null && dataTuple.Item2 != null)
            {
                await SaveDataAsync(dataTuple);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> GetDataAsync()
        {
            #region 客製化部分(取得API資料)
            using var httpClient = _httpClientFactory.CreateClient();
            var result = await httpClient.GetAsync(apiUrl);
            #endregion 客製化部分(取得API資料)

            var fileName = result.Content.Headers.ContentDisposition.FileName;
            var newChecksumText = _fileOperateService.SaveAndCalculateChecksum(
                result.Content.ReadAsStream(),
                $"{basePath}/{newDirName}/{fileName}",
                checksumFilename
            );
            var currentChecksumText = _fileOperateService.ReadTextFile(
                $"{basePath}/{currentDirName}/{checksumFilename}");
            if (newChecksumText == currentChecksumText)
            {
                _fileOperateService.DeleteDirectory($"{basePath}/{newDirName}");
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<(List<Stocks?>, List<StockDaily?>)> ProcessDataAsync()
        {
            #region 客製化部分
            var dataDir = new DirectoryInfo($"{basePath}/New");
            var fileInfos = dataDir.GetFiles();
            var pattern = @"^*\.json$";
            var jsonfile = fileInfos
                .Where(f => Regex.IsMatch(f.Name, pattern))
                .FirstOrDefault();
            var dataContent = _fileOperateService.ReadTextFile(jsonfile.FullName);
            var dataResult = JsonConvert.DeserializeObject<List<Stocks?>>(dataContent);
            var dataResult2 = JsonConvert.DeserializeObject<List<StockDaily?>>(dataContent);
            var now = DateTime.Today;
            dataResult2.ForEach(d => d.TransactionDate = now);
            #endregion 客製化部分

            _fileOperateService.DeleteDirectory($"{basePath}/{currentDirName}");
            _fileOperateService.MoveDirectory($"{basePath}/{newDirName}", $"{basePath}/{currentDirName}");

            return (dataResult, dataResult2);
        }

        /// <inheritdoc/>
        public async Task SaveDataAsync((List<Stocks>?, List<StockDaily>?) dataTuple)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<DatabaseContext>();
                var existData = await db.Stocks.ToListAsync();

                var modifyData = new ModifyDataModel<Stocks>
                {
                    WillDelete = existData.Except(dataTuple.Item1,
                        (e, i) => e.Name == i.Name &&
                                  e.Code == i.Code).ToList(),
                    WillInsert = dataTuple.Item1.Except(existData,
                        (i, e) => e.Name == i.Name &&
                                  e.Code == i.Code).ToList()
                };

                //Insert
                var insertTask = db.Stocks.AddRangeAsync(modifyData.WillInsert);
                //Delete (soft)
                var updateTask = Task.Run(() => modifyData.WillDelete.ForEach(d =>
                {
                    d.IsDelete = true;
                }));

                //儲存
                await Task.WhenAll(insertTask, updateTask);
                var tx = await db.Database.BeginTransactionAsync();
                await db.SaveChangesAsync();
                await tx.CommitAsync();




                await db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE StockDaily");
                var modifyData2 = new ModifyDataModel<StockDaily>
                {
                    WillInsert = dataTuple.Item2
                };

                //Insert
                var insertTask2 = db.StockDaily.AddRangeAsync(modifyData2.WillInsert);

                //儲存
                await Task.WhenAll(insertTask2);
                var tx2 = await db.Database.BeginTransactionAsync();
                await db.SaveChangesAsync();
                await tx2.CommitAsync();
            }
        }
    }
}
