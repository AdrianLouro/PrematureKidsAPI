using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IParentRepository _parent;
        private IAccountRepository _account;

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

        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContext);
                }

                return _account;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
    }
}