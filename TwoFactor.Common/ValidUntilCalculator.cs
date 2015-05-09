using System;

namespace TwoFactor.Common
{
    public class ValidUntilCalculator : IRoundTimeUp
    {
        public DateTime RoundTimeUpToNearest(DateTime startTime, TimeSpan nextInterval)
        {
            return new DateTime(((startTime.Ticks + nextInterval.Ticks - 1) / nextInterval.Ticks) * nextInterval.Ticks);
        }
    }
}
