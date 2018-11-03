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

        public IParentRepository Parent
        {
            get
            {
                if (_parent == null)
                {
                    _parent = new ParentRepository(_repoContext);
                }

                return _parent;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }

                return _user;
            }
        }

        public IDoctorRepository Doctor
        {
            get
            {
                if (_doctor == null)
                {
                    _doctor = new DoctorRepository(_repoContext);
                }

                return _doctor;
            }
        }

        public IChildRepository Child
        {
            get
            {
                if (_child == null)
                {
                    _child = new ChildRepository(_repoContext);
                }

                return _child;
            }
        }

        public IChildParentRepository ChildParent
        {
            get
            {
                if (_childParent == null)
                {
                    _childParent = new ChildParentRepository(_repoContext);
                }

                return _childParent;
            }
        }

        public IChildDoctorRepository ChildDoctor
        {
            get
            {
                if (_childDoctor == null)
                {
                    _childDoctor = new ChildDoctorRepository(_repoContext);
                }

                return _childDoctor;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_repoContext);
                }

                return _category;
            }
        }

        public IExerciseRepository Exercise
        {
            get
            {
                if (_exercise == null)
                {
                    _exercise = new ExerciseRepository(_repoContext);
                }

                return _exercise;
            }
        }

        public IOpinionRepository Opinion
        {
            get
            {
                if (_opinion == null)
                {
                    _opinion = new OpinionRepository(_repoContext);
                }

                return _opinion;
            }
        }

        public IAssignmentRepository Assignment
        {
            get
            {
                if (_assignment == null)
                {
                    _assignment = new AssignmentRepository(_repoContext);
                }

                return _assignment;
            }
        }

        public ISessionRepository Session
        {
            get
            {
                if (_session == null)
                {
                    _session = new SessionRepository(_repoContext);
                }

                return _session;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
    }
}