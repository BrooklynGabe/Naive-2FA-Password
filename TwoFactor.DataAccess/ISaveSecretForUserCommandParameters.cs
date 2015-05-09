using System;
namespace TwoFactor.DataAccess
{
    public interface ISaveSecretForUserCommandParameters
    {
        byte[] Secret { get; set; }
        string UserLoginName { get; set; }
        DateTime ValidUntil { get; set; }
    }
}
