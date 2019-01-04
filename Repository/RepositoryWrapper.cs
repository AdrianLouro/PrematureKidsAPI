using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IUserRepository _user;
        private IParentRepository _parent;
        private IDoctorRepository _doctor;
        private IChildRepository _child;
        private IChildParentRepository _childParent;
        private IChildDoctorRepository _childDoctor;
        private ICategoryRepository _category;
        private IExerciseRepository _exercise;
        private IOpinionRepository _opinion;
        private IAssignmentRepository _assignment;
        private ISessionRepository _session;
        private IAdministratorRepository _administrator;
        private IExerciseAttachmentRepository _exerciseAttachment;
        private ISessionAttachmentRepository _sessionAttachment;

        public IParentRepository Parent => _parent ?? (_parent = new ParentRepository(_repoContext));

        public IUserRepository User => _user ?? (_user = new UserRepository(_repoContext));

        public IDoctorRepository Doctor => _doctor ?? (_doctor = new DoctorRepository(_repoContext));

        public IChildRepository Child => _child ?? (_child = new ChildRepository(_repoContext));

        public IChildParentRepository ChildParent =>
            _childParent ?? (_childParent = new ChildParentRepository(_repoContext));

        public IChildDoctorRepository ChildDoctor =>
            _childDoctor ?? (_childDoctor = new ChildDoctorRepository(_repoContext));

        public ICategoryRepository Category => _category ?? (_category = new CategoryRepository(_repoContext));

        public IExerciseRepository Exercise => _exercise ?? (_exercise = new ExerciseRepository(_repoContext));

        public IOpinionRepository Opinion => _opinion ?? (_opinion = new OpinionRepository(_repoContext));

        public IAssignmentRepository Assignment =>
            _assignment ?? (_assignment = new AssignmentRepository(_repoContext));

        public ISessionRepository Session => _session ?? (_session = new SessionRepository(_repoContext));

        public IAdministratorRepository Administrator =>
            _administrator ?? (_administrator = new AdministratorRepository(_repoContext));

        public IExerciseAttachmentRepository ExerciseAttachment =>
            _exerciseAttachment ?? (_exerciseAttachment = new ExerciseAttachmentRepository(_repoContext));

        public ISessionAttachmentRepository SessionAttachment =>
            _sessionAttachment ?? (_sessionAttachment = new SessionAttachmentRepository(_repoContext));

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
    }
}