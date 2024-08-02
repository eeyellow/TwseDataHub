
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Main.Services.BankApiDataModel;
using IndexAttribute = CsvHelper.Configuration.Attributes.IndexAttribute;

namespace Main.Services
{
    namespace BankApiDataModel
    {
        public class BankNo
        {
            /// <summary>
            /// 銀行代號
            /// </summary>
            //[Name("銀行代號")]
            [Index(0)]
            public string BankCode { get; set; }
            /// <summary>
            /// 分支機構代號
            /// </summary>
            //[Name("分支機構代號")]
            [Index(1)]
            public string BranchCode { get; set; }
            /// <summary>
            /// 金融機構名稱
            /// </summary>
            //[Name("金融機構名稱")]
            [Index(2)]
            public string BankName { get; set; }
            /// <summary>
            /// 分支機構名稱
            /// </summary>
            //[Name("分支機構名稱")]
            [Index(3)]
            public string BranchName { get; set; }
            /// <summary>
            /// 地址
            /// </summary>
            //[Name("地址")]
            [Index(4)]
            public string Address { get; set; }
            public int ColType { get; private set; }

            public enum ColTypeEnum
            {
                /// <summary>
                /// 銀行資訊
                /// </summary>
                BankInfo = 1,
                /// <summary>
                /// 分行資訊
                /// </summary>
                SubBankInfo = 2
            }

            public BankNo Init()
            {
                if (!string.IsNullOrWhiteSpace(this.BankCode)
                     && !string.IsNullOrWhiteSpace(this.BranchCode)
                     && !string.IsNullOrWhiteSpace(this.BankName)
                     && !string.IsNullOrWhiteSpace(this.BranchName)
                     && !string.IsNullOrWhiteSpace(this.Address)
                     )
                {
                    this.ColType = (int)ColTypeEnum.SubBankInfo;
                }
                else
                {
                    this.ColType = (int)ColTypeEnum.BankInfo;
                }
                return this;
            }
        }
    }

    /// <summary>
    /// https://joshclose.github.io/CsvHelper/examples/configuration/class-maps/ignoring-properties/
    /// </summary>
    public sealed class BankNoMap : ClassMap<BankNo>
    {
        public BankNoMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.ColType).Ignore();
        }
    }

    public interface IBankApiDataService
    {
        public Task<List<BankNo>> GetBankInfoAsync();
    }

    /// <summary>
    /// 使用API取得資料，並更新到資料庫
    /// </summary>
    public class BankApiDataService : IBankApiDataService
    {
        //https://data.gov.tw/dataset/24323 中央銀行->「總分支機構位置」查詢一覽表
        private string TaiwanBankInfoLink = "https://www.fisc.com.tw/TC/OPENDATA/R2_Location.csv";

        //////https://data.gov.tw/dataset/6041 金融監督管理委員會->金融機構基本資料查詢
        ////private string BankInfoLink = "https://www.banking.gov.tw/ch/ap/bankno_excel.jsp";

        private readonly IHttpClientFactory _httpClientFactory = null!;
        // private readonly ICurrentUserService _currentUserService;
        // private readonly OrganicDBContext _context;

        #region Repository
        // private IBankRepository _bankRe;
        // private IBranchRepository _branchRe;
        #endregion

        public BankApiDataService(
             // OrganicDBContext context
             // , IBankRepository bankRe
             // , IBranchRepository branchRe
             // , ICurrentUserService currentUserService
             IHttpClientFactory httpClientFactory)
        {
            // _context = context;
            // _bankRe = bankRe;
            // _branchRe = branchRe;
            // _currentUserService = currentUserService;
            _httpClientFactory = httpClientFactory;
        }


        // private List<BankNo> GetDataNotInDB(List<BankNo> csvData)
        // {
        //     var branches = (from bank in _bankRe.FindAll()
        //                     join branch in _branchRe.FindAll() on bank.ID equals branch.BankID
        //                     select new
        //                     {
        //                         BankCode = bank.Code,
        //                         BankName = bank.Name,
        //                         BranchCode = branch.Code,
        //                         BranchName = branch.Name
        //                     }).ToList();
        //     var banks = branches.GroupBy(x => new { x.BankCode, x.BankName }).Select(x => x.Key);

        //     // 只取與現有DB銀行有對應的CSV資料
        //     var dataHasBankInDB = from data in csvData
        //                           join bank in banks on new { data.BankCode, data.BankName } equals new { bank.BankCode, bank.BankName }
        //                           select data;
        //     //已在DB的分行
        //     var currentBranches = from data in dataHasBankInDB
        //                           join branch in branches on new { data.BankCode, data.BranchCode } equals new { branch.BankCode, branch.BranchCode }
        //                           select data;
        //     var insertedData = dataHasBankInDB.Except(currentBranches).ToList();
        //     return insertedData;
        // }

        /// <summary>
        /// 通過API取得銀行資訊CSV
        /// </summary>
        /// <returns></returns>
        public async Task<List<BankNo>> GetBankInfoAsync()
        {
            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient())
                {
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/csv");
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
                    var response = await _httpClient.GetAsync(TaiwanBankInfoLink);
                    if (response.IsSuccessStatusCode)
                    {
                        return await GetCSVDataAsync(response);
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 讀取CSV資料，並轉為Model
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        private async Task<List<BankNo>> GetCSVDataAsync(HttpResponseMessage resp)
        {
            var stream = await resp.Content.ReadAsStreamAsync();
            try
            {
                using (var reader = new StreamReader(stream))
                {
                    var bankList = new List<BankNo>();
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ",",
                        BadDataFound = x => {/*部分錯誤，但不影響讀取，因此略過*/},
                        TrimOptions = TrimOptions.Trim,
                        MissingFieldFound = null,
                        HasHeaderRecord = false
                    };
                    var csv = new CsvReader(reader, config);
                    csv.Context.RegisterClassMap<BankNoMap>();
                    var bank = csv.GetRecords<BankNo>();
                    var i = 0;
                    foreach (var line in bank)
                    {
                        if (i < 1)
                        {
                            ++i;
                            continue;
                        }
                        bankList.Add(line.Init());
                    }
                    return bankList.Where(x => x.ColType == (int)BankNo.ColTypeEnum.SubBankInfo).ToList();
                }
            }
            catch (Exception e)
            {
                var err = e.Message;
            }
            return new List<BankNo>();
        }
    }
}
