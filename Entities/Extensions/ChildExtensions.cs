using Entities.Models;

namespace Entities.Extensions
{
    public static class ChildExtensions
    {
        public static void Map(this Child dbChild, Child child)
        {
            dbChild.Name = child.Name;
            dbChild.DateOfBirth = child.DateOfBirth;
            dbChild.Gender = child.Gender;
        }
    }
}