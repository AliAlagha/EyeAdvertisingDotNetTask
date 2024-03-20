using System;
using System.Collections.Generic;
using System.Text;

namespace EyeAdvertisingDotNetTask.Core.ViewModels.Auth
{
    public class AccessTokenViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiredAt { get; set; }
        public DateTime RefreshTokenExpiredAt { get; set; }
    }
}
