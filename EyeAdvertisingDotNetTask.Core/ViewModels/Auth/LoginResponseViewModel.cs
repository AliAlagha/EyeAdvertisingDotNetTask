using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace EyeAdvertisingDotNetTask.Core.ViewModels.Auth
{
    public class LoginResponseViewModel : IBaseViewModel
    {
        public AccessTokenViewModel Token { get; set; }
        public string User { get; set; }

    }
}
