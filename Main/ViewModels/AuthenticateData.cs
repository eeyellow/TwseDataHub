namespace DataShareHub.ViewModels
{
    /// <summary>
    /// 註冊資料
    /// </summary>   
    public class RegisterModel
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

    /// <summary>
    /// 登入資料
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

    /// <summary>
    /// Represents the response.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Status
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string? Message { get; set; }
    }

    /// <summary>
    /// UserRoles
    /// </summary>
    public static class UserRoles
    {
        /// <summary>
        /// Admin
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// User
        /// </summary>
        public const string User = "User";
    }
}
