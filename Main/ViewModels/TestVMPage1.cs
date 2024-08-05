using LC.Models.Entities;

namespace TwseDataHub.ViewModels
{
    /// <summary>
    /// TestVMPage1
    /// </summary>
    public class TestVMPage1
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID")]
        public long ID { get; set; }
        /// <summary>
        /// 用戶名
        /// </summary>
        [Description("用戶名")]
        public string Name { get; set; } = "";
        /// <summary>
        /// 相關用戶
        /// </summary>
        [Description("相關用戶")]
        public List<UserProfile> TestList { get; set; } = new List<UserProfile>();
    }

    /// <summary>
    /// TestVM
    /// </summary>
    public class TestVM
    {
        /// <summary>
        /// ID
        /// </summary>
        [Description("ID")]
        public long ID { get; set; }
        /// <summary>
        /// 用戶名
        /// </summary>
        [Description("用戶名")]
        public string Name { get; set; } = "";
    }
}
