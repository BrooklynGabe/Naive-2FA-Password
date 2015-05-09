using System;
using System.Linq;
using TwoFactor.Common;
using TwoFactor.DataAccess;

namespace TwoFactor.API.Models
{
    public class TwoFactorService
    {
        public TwoFactorService()
        {
            _context = new TwoFactorContext();
            _reader = new OneTimeSecretReader(_context);
            _options = new TimeBasedOneTimeOptions();
            _interval = new ValidUntilCalculator();

            // Hard-coded 5 minutes
            _options.TimeInterval = 300;
        }

        public bool DoesSecretExistForUser(string UserLoginName)
        {
            var now = DateTime.Now;
            var expires = _interval.RoundTimeUpToNearest(now, TimeSpan.FromSeconds(_options.TimeInterval));

            return _reader
                        .Any(s =>
                            s.UserLoginName == UserLoginName.Trim()
                            && now <= s.ValidUntil);
        }

        public bool VerifyPasswordForUser(string UserLoginName, string OneTimePassword)
        {
            var now = DateTime.Now;
            var expires = _interval.RoundTimeUpToNearest(now, TimeSpan.FromSeconds(_options.TimeInterval));

            var existingSecret = _reader
                                    .Where(s =>
                                        s.UserLoginName == UserLoginName.Trim()
                                        && now <= s.ValidUntil )
                                    .Select(s => s.Secret)
                                    .FirstOrDefault();

            if (existingSecret == null)
                return false;

            var secretProvider = new ExistingSecretProvider(existingSecret);

            var generator = new TimeBasedOneTimeGenerator(secretProvider, _options, _interval);

            var generatedPassword = generator.GeneratePassword();

            return OneTimePassword == generatedPassword;
        }

        public void SetSecretForUser(string UserLoginName)
        {
            var now = DateTime.Now;
            var expires = _interval.RoundTimeUpToNearest(now, TimeSpan.FromSeconds(_options.TimeInterval));
            var secretProvider = new ActiveNullSecretProvider();

            var command = new SaveSecretForUserCommand(_context)
            {
                Secret = secretProvider.Secret,
                ValidUntil = expires
            };
            command.UserLoginName = UserLoginName.Trim();

            try
            {
                command.Execute();
            }
            catch
            {
                //TODO: Some Exception Handling
            }
        }

        public void DeleteSecretForUser(string UserLoginName)
        {
            var command = new DeleteSecretForUserCommand(_context);
            command.UserLoginName = UserLoginName.Trim();

            try
            {
                command.Execute();
            }
            catch
            {
                //TODO: Some Exception Handling
            }
        }

        private readonly IOneTimePasswordOptions _options;
        private readonly IRoundTimeUp _interval;
        private readonly TwoFactorContext _context;
        private readonly OneTimeSecretReader _reader;
    }
}