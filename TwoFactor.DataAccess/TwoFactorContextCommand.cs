using System;

namespace TwoFactor.DataAccess
{
    public abstract class TwoFactorContextCommand: ICommand
    {
        public TwoFactorContextCommand(TwoFactorContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public void Execute()
        {
            if (IsValid())
                CommandContents();
        }

        public abstract bool IsValid();

        protected abstract void CommandContents();

        protected TwoFactorContext _context;
    }
}
