namespace TwoFactor.Common
{
    public interface IProvideUniqueSecrets
    {
        byte[] Secret { get; }
    }
}
