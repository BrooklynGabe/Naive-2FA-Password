using System;
namespace TwoFactor.DataAccess
{
    public interface IDeleteSecretForUserCommandParameters
    {
        string UserLoginName { get; set; }
    }
}
