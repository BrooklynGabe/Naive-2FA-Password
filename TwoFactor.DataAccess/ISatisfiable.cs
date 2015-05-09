namespace TwoFactor.DataAccess
{
    public interface ISatisfiable<in TheEntity>
    {
        bool SatisfiedBy(TheEntity entity);
    }
}
