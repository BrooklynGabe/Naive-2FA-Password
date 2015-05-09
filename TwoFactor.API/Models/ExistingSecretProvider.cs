using TwoFactor.Common;

namespace TwoFactor.API.Models
{
    public class ExistingSecretProvider : IProvideUniqueSecrets
    {
        public ExistingSecretProvider(byte[] secret)
        {
            _secret = secret;
        }
        public byte[] Secret
        {
            get
            {
                return _secret;
            }
        }

        private byte[] _secret;
    }
}