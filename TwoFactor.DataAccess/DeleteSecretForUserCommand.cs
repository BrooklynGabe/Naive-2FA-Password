using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactor.DataAccess
{
    public class DeleteSecretForUserCommand : TwoFactorContextCommand, IDeleteSecretForUserCommandParameters
    {
        public DeleteSecretForUserCommand(TwoFactorContext context)
            : base(context)
        {
        }

        public override bool IsValid()
        {
            return isValidName(UserLoginName);
        }

        public string UserLoginName
        {
            set
            {
                if (!isValidName(value))
                    throw new ArgumentException("UserLoginName");

                _userLoginName = value.Trim();
            }
            get
            {
                return _userLoginName;
            }
        }

        protected override void CommandContents()
        {
            _context.DeleteSecretForUser(UserLoginName);
        }

        private bool isValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        private string _userLoginName;
    }
}
