using System;

namespace TwoFactor.Common
{
    public class ActiveNullSecretProvider : IProvideUniqueSecrets
    {
        public byte[] Secret
        {
            get
            {
                if(_secret == null)
                    _secret = Guid.NewGuid().ToByteArray();
                
                return _secret;
            }
        }

        private byte[] _secret;
    }
}
