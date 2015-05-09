using System.Linq;

namespace TwoFactor.DataAccess
{
    public class OneTimeSecretReader : TwoFactorContextReader<IOneTimeSecret>
    {
        public OneTimeSecretReader(TwoFactorContext context)
            : base(context)
        {
            _viewModel = from o in context.OneTimeSecrets
                         select o;
        }

        protected override IQueryable<IOneTimeSecret> QueryableData
        {
            get { return _viewModel; }
        }

        private readonly IQueryable<IOneTimeSecret> _viewModel;
    }
}
