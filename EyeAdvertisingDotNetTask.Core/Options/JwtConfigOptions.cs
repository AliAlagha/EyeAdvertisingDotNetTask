namespace EyeAdvertisingDotNetTask.Core.Options
{
    public class JwtConfigOptions
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Site { get; set; }
    }
}
