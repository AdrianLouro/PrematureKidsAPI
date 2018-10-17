using Entities.Models;

namespace Entities.Extensions
{
    public static class ParentExtensions
    {
        public static void Map(this Parent dbParent, Parent parent)
        {
            dbParent.Name = parent.Name;
            dbParent.IdNumber = parent.IdNumber;
            dbParent.Telephone = parent.Telephone;
        }
    }
}