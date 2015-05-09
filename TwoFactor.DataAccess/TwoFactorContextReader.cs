using System;

namespace TwoFactor.DataAccess
{
    public abstract class TwoFactorContextReader<TheEntity> : QueryReader<TheEntity>
    {
        public TwoFactorContextReader(TwoFactorContext context)
        {
            if (context == null)
                throw new ArgumentNullException("Context");

            _context = context;
        }

        protected readonly TwoFactorContext _context;
    }
}
