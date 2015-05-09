using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactor.Common
{
    public class TimeBasedOneTimeOptions : IOneTimePasswordOptions
    {
        public TimeBasedOneTimeOptions()
        {
            PasswordLength = TimeBasedOneTimeOptions.MinimumPasswordLength;
            TimeInterval = TimeBasedOneTimeOptions.MinimumTimeInterval;
        }

        public long TimeInterval
        {
            get
            {
                return _timeInterval;
            }
            set
            {
                if (value < TimeBasedOneTimeOptions.MinimumTimeInterval)
                    throw new ArgumentException(
                        string.Format("Must be greater than {0} seconds", TimeBasedOneTimeOptions.MinimumTimeInterval)
                        , "TimeInterval");
                _timeInterval = value;
            }
        }

        public int PasswordLength
        {
            get
            {
                return _passwordLength;
            }
            set
            {
                if (value < TimeBasedOneTimeOptions.MinimumPasswordLength)
                    throw new ArgumentException(
                        string.Format("Must be greater than {0} long", TimeBasedOneTimeOptions.MinimumPasswordLength)
                        , "PasswordLength");

                _passwordLength = value;
            }
        }

        public static readonly long MinimumTimeInterval = 30;
        public static readonly int MinimumPasswordLength = 6;
        
        private long _timeInterval;
        private int _passwordLength;
    }
}
