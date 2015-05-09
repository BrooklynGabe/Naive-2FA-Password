using System;

namespace TwoFactor.Common
{
    public interface IRoundTimeUp
    {
        DateTime RoundTimeUpToNearest(DateTime startTime, TimeSpan nextInterval);
    }
}
