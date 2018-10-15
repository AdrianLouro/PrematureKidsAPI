namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IParentRepository Parent { get; }

        IAccountRepository Account { get; }
    }
}