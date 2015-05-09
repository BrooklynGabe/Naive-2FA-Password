using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TwoFactor.Common
{
    public class TimeBasedOneTimeGenerator : IGenerateOneTimePasswords
    {
        public TimeBasedOneTimeGenerator(
            IProvideUniqueSecrets secretProvider
            , IOneTimePasswordOptions passwordOptions
            , IRoundTimeUp validUntilCalculator)
        {
            if (secretProvider == null)
                throw new ArgumentNullException("secretProvider");

            if (passwordOptions == null)
                throw new ArgumentNullException("passwordOptions");

            if (validUntilCalculator == null)
                throw new ArgumentNullException("validUntilCalculator");

            _secretProvider = secretProvider;
            _passwordOptions = passwordOptions;
            _validUntilCalculator = validUntilCalculator;
        }

        public string GeneratePassword()
        {
            var expirationTime = _validUntilCalculator.RoundTimeUpToNearest(DateTime.Now, TimeSpan.FromSeconds(_passwordOptions.TimeInterval));
            var seconds = expirationTime.Subtract(DateTime.MinValue).TotalSeconds;

            var counter = BitConverter.GetBytes(seconds);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(counter);

            var hmac = new HMACSHA1(_secretProvider.Secret, true);

            var hash = hmac.ComputeHash(counter);

            var offset = hash[hash.Length - 1] & 0xf;

            var binary =
                ((hash[offset] & 0x7f) << 24)
                | ((hash[offset + 1] & 0xff) << 16)
                | ((hash[offset + 2] & 0xff) << 8)
                | (hash[offset + 3] & 0xff);

            var password = binary % (int)Math.Pow(10, _passwordOptions.PasswordLength);

            return password.ToString(new string('0', _passwordOptions.PasswordLength));
        }

        private IProvideUniqueSecrets _secretProvider;
        private IOneTimePasswordOptions _passwordOptions;
        private IRoundTimeUp _validUntilCalculator;
    }
}
