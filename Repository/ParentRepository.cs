using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ParentRepository : RepositoryBase<Parent>, IParentRepository
    {
        public ParentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}