namespace Library.Backend.Helpers
{
    public class AppSettingConfig
    {
        public const string AppSettings = "AppSettings";
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int TokenExpired { get; set; }
    }
}
