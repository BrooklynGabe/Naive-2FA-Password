using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwoFactor.Common
{
    public interface IOneTimePasswordOptions
    {
        long TimeInterval { get; set; }
        int PasswordLength { get; set; }
    }
}
