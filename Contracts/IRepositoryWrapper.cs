namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IParentRepository Parent { get; }

        IUserRepository User { get; }

        IDoctorRepository Doctor { get; }
    }
}