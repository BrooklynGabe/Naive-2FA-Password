using System;

namespace TwoFactor.DataAccess
{
    public interface IOneTimeSecret
    {
        string UserLoginName { get; set; }
        byte[] Secret { get; set; }
        DateTime ValidUntil { get; set; }
    }
}