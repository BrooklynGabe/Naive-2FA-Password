using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactor.DataAccess
{
    public class SaveSecretForUserCommand : TwoFactorContextCommand, ISaveSecretForUserCommandParameters
    {
        public SaveSecretForUserCommand(TwoFactorContext context)
            : base(context)
        {
        }

        public override bool IsValid()
        {
            return isValidName(UserLoginName) && isValidSecret(Secret) && isValidDateTime(ValidUntil);
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

        public byte[] Secret
        {
            set
            {
                if (!isValidSecret(value))
                    throw new ArgumentException("Secret");

                _secret = value;
            }
            get
            {
                return _secret;
            }
        }

        public DateTime ValidUntil
        {
            set
            {
                if (!isValidDateTime(value))
                    throw new ArgumentException("ValidUntil");

                _validUntil = value;
            }
            get
            {
                return _validUntil;
            }
        }

        protected override void CommandContents()
        {
            _context.SetSecretForUser(UserLoginName, Secret, ValidUntil);
        }

        private bool isValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        private bool isValidSecret(byte[] secret)
        {
            return secret != null && secret.Length > 0;
        }

        private bool isValidDateTime(DateTime date)
        {
            return date != null && date > DateTime.MinValue;
        }

        private string _userLoginName;
        private byte[] _secret;
        private DateTime _validUntil;
    }
}
