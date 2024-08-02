using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.Models.Contexts;
using LC.Models.Entities;


namespace Main.Api.GraphQL
{
    public class CountyQL
    {
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

        public interface ICountyTownDataService
        {
            public List<County> GetCounty();
        }
        public class CountyTownDataService : ICountyTownDataService
        {
            DatabaseContext _dbContext { get; set; }

            public CountyTownDataService(IServiceProvider serviceProvider, DatabaseContext dbContext)
            {
                this._dbContext = dbContext;
            }

            public List<County> GetCounty()
            {
                return _dbContext.County.Where(x => !x.IsDelete).ToList();
            }
        }

        public class CountyTownQuery : ObjectGraphType
        {
            public CountyTownQuery(ICountyTownDataService _dataService)
            {
                Field<ListGraphType<AutoQL_CountyType>>("getCounties")
                .Description("取得縣市、鄉鎮市區資料")
                .Resolve(context => _dataService.GetCounty()
                .Select(x => new QL_County()
                {
                    Name = x.Name,
                    CountyCode = x.CountyCode,
                    CountyID = x.CountyID,
                    TownList = new List<QL_Town>()
                }));
            }
        }

        public class CountyTownSchema : Schema
        {
            public CountyTownSchema(IServiceProvider serviceProvider) : base(serviceProvider)
            {
                Query = serviceProvider.GetService<CountyTownQuery>()!;
            }
        }
    }
}
