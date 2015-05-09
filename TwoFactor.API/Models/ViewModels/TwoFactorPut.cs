using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwoFactor.API.Models.ViewModels
{
    public class TwoFactorPut
    {
        public string UserLoginName { get; set; }
        public string OneTimePassword { get; set; }
    }
}