using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.Models.Contexts;
using LC.Models.Entities;
using TwseDataHub.ViewModels;
using GraphQL;
using GraphQL.Types;
using Main.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using LC.Models.Entities;

namespace TwseDataHub.Api.GraphQL
{

    public class UserProfileType : ObjectGraphType<UserProfile>
    {
        public UserProfileType()
        {
            Field(x => x.ID).Description("ID");
            Field(x => x.Name).Description("用戶名AAA");
            Field(x => x.Account).Description("帳號");
            Field(x => x.Password).Description("密碼");
            Field(x => x.CreateDatetime).Description("建立時間");
        }
    }

    public class AutoTestVMPage1Type : AutoRegisteringObjectGraphType<TestVMPage1>
    {
        public AutoTestVMPage1Type() : base(x => x.TestList)
        {
            Field<ListGraphType<AutoRegisteringObjectGraphType<UserProfile>>>(nameof(TestVMPage1.TestList))
                        .Description(typeof(TestVMPage1).GetDescription(nameof(TestVMPage1.TestList)))
                        .Resolve(context => context.Source.TestList);
        }
    }

    public class TestVMPage1Type : ObjectGraphType<TestVMPage1>
    {
        public TestVMPage1Type()
        {
            Field(x => x.ID).Description("ID");
            Field(x => x.Name).Description("用戶名");
            Field<ListGraphType<AutoRegisteringObjectGraphType<UserProfile>>>("testList", resolve: context => context.Source.TestList);
        }
    }
    //================================================================================================
    public class QL_County
    {
        /// <summary>
        /// 名稱
        /// </summary>  
        [Description("名稱")]
        public string Name { get; set; }

        /// <summary>
        /// 縣市代號
        /// </summary> 
        [Description("縣市代號")]
        public string CountyID { get; set; }

        /// <summary>
        /// 縣市代碼
        /// </summary>    
        [Description("縣市代碼")]
        public string CountyCode { get; set; }

        [Description("鄉鎮市區列表")]
        public List<QL_Town> TownList { get; set; }
    }

    public class AutoQL_CountyType : AutoRegisteringObjectGraphType<QL_County>
    {
        public AutoQL_CountyType() : base(x => x.TownList)
        {
            Field<ListGraphType<AutoRegisteringObjectGraphType<QL_Town>>>(nameof(QL_County.TownList))
                        .Description(typeof(QL_County).GetDescription(nameof(QL_County.TownList)))
                        .Resolve(context => context.Source.TownList);
        }
    }

    public class QL_Town
    {
        /// <summary>
        /// 名稱
        /// </summary>   
        [Description("名稱")]
        public string Name { get; set; }

        /// <summary>
        /// 鄉鎮市區代號
        /// </summary>   
        [Description("鄉鎮市區代號")]
        public string TownID { get; set; }

        /// <summary>
        /// 鄉鎮市區代碼
        /// </summary>  
        [Description("鄉鎮市區代碼")]
        public string TownCode { get; set; }
    }
    //================================================================================================
    public class QL_BankNo
    {
        /// <summary>
        /// 銀行代號
        /// </summary>
        [Description("銀行代號")]
        public string BankCode { get; set; }
        /// <summary>
        /// 分支機構代號
        /// </summary>
        [Description("分支機構代號")]
        public string BranchCode { get; set; }
        /// <summary>
        /// 金融機構名稱
        /// </summary>
        [Description("金融機構名稱")]
        public string BankName { get; set; }
        /// <summary>
        /// 分支機構名稱
        /// </summary>
        [Description("分支機構名稱")]
        public string BranchName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Description("地址")]
        public string Address { get; set; }
        /// <summary>
        /// 類別
        /// </summary>
        /// <value></value>
        [Description("類別")]
        public int ColType { get;  set; }
    }
    //================================================================================================
    public interface IDataService
    {
        public List<UserProfile> GetData();
        public List<TestVMPage1> GetData2();
        public List<County> GetCounty();
        public Task<List<Main.Services.BankApiDataModel.BankNo>> GetBanks();
    }

    public class DataService : IDataService
    {
        // [Inject]
        DatabaseContext _dbContext { get; set; }
        IBankApiDataService _bankApiDataService { get; set; }

        public DataService(
            IServiceProvider serviceProvider,
            IBankApiDataService bankApiDataService,
            DatabaseContext dbContext)
        {
            // serviceProvider.Inject(this);
            this._dbContext = dbContext;
            this._bankApiDataService = bankApiDataService;
        }

        public List<UserProfile> GetData()
        {
            // return new List<UserProfile>() { new UserProfile() { ID = 1, Name = "UserProfile1", Account = "acc1", Password = "pass1", IsDelete = false, CreateDatetime = DateTime.Now } };
            return _dbContext.UserProfile.Where(x => !x.IsDelete).ToList();
        }

        // https://graphql-dotnet.github.io/docs/getting-started/schema-types/
        public List<TestVMPage1> GetData2()
        {
            var usr = _dbContext.UserProfile.Where(x => !x.IsDelete).ToList();
            var result = usr.Select(a => new TestVMPage1()
            {
                ID = a.ID,
                Name = a.Name,
                // TestList = usr.Where(x => x.ID != usr.FirstOrDefault().ID)
                //         .Select(x => new TestVM() { ID = x.ID, Name = x.Name }).ToList()
                TestList = usr.Where(x => x.ID != a.ID).ToList()
            }).ToList();
            return result;
        }

        public List<County> GetCounty()


        {
            return _dbContext.County.Where(x => !x.IsDelete).ToList();


        }

        public async Task<List<Main.Services.BankApiDataModel.BankNo>> GetBanks()


        {
            return await _bankApiDataService.GetBankInfoAsync();


        }
    }

    public class Query : ObjectGraphType
    {
        // [Inject]
        // IDataService _dataService { get; set; }
        public Query(IDataService _dataService)
        {
            // serviceProvider.Inject(this);
            Field<ListGraphType<AutoRegisteringObjectGraphType<UserProfile>>>("userProfiles")
            // Field<ListGraphType<UserProfileType>>("userProfiles")
            .Description("取用戶資料")
            .Resolve(context => _dataService.GetData());


            // serviceProvider.Inject(this);
            Field<ListGraphType<AutoTestVMPage1Type>>("testQuery")
            // Field<ListGraphType<UserProfileType>>("testQuery")
            .Description("測試1")
            .Resolve(context => _dataService.GetData2());


            Field<ListGraphType<AutoQL_CountyType>>("getCounties")
            .Description("取得縣市、鄉鎮市區資料")
            .Resolve(context => _dataService.GetCounty()
                                .Select(x => new QL_County()
                                {
                                    Name = x.Name,
                                    CountyCode = x.CountyCode,
                                    CountyID = x.CountyID,
                                    TownList = new List<QL_Town>()
                                })
            );

            Field<ListGraphType<AutoRegisteringObjectGraphType<QL_BankNo>>>("getBanks")
            .Description("取得銀行與分行資料")
            .ResolveAsync(async context => (await _dataService.GetBanks())
                                            .Select(x => new QL_BankNo()
                                            {
                                                BankName = x.BankName,
                                                BankCode = x.BankCode,
                                                BranchCode = x.BranchCode,
                                                BranchName = x.BranchName,
                                                Address = x.Address,
                                                ColType = x.ColType
                                            })
            );
        }
    }

    public class PublicSchema : Schema
    {
        public PublicSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetService<Query>()!;
        }
        // public PublicSchema(Func<Type, GraphType> resolveType) : base(resolveType)
        // {
        //     Query = (Query)resolveType(typeof(Query));
        // }
    }
}
