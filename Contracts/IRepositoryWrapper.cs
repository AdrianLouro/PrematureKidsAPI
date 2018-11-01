namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IParentRepository Parent { get; }

        IUserRepository User { get; }

        IDoctorRepository Doctor { get; }

        IChildRepository Child { get; }

        IChildParentRepository ChildParent { get; }
         
        IChildDoctorRepository ChildDoctor { get; }

        ICategoryRepository Category{ get; }

        IExerciseRepository Exercise { get; }

        IOpinionRepository Opinion { get; }
    }
}