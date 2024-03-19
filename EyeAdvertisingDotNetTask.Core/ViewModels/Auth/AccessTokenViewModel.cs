using System;
using System.Collections.Generic;
using System.Text;

namespace EyeAdvertisingDotNetTask.Core.ViewModels.Auth
{
    public class AccessTokenViewModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
