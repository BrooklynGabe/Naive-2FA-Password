namespace TwoFactor.DataAccess
{
    public interface ICommand
    {
        void Execute();
        bool IsValid();
    }
}
