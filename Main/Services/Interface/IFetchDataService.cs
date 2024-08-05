using System.Threading.Tasks;

namespace TwseDataHub.Services
{
    /// <summary>
    /// 由外部取得資料的排程 - 介面
    /// </summary>
    public interface IFetchDataService<T>
    {
        /// <summary>
        /// 程式進入點
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();

        /// <summary>
        /// 由外部API取得資料
        /// </summary>
        /// <returns></returns>
        Task<bool> GetDataAsync();

        /// <summary>
        /// 處理資料
        /// </summary>
        /// <returns></returns>
        Task<T> ProcessDataAsync();

        /// <summary>
        /// 儲存資料進資料庫
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        Task SaveDataAsync(T dataList);
    }

}
