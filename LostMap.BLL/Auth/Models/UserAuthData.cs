namespace LostMap.BLL.Auth.Models
{
    public class UserAuthData
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
