using Entities.Models;

namespace Entities.Extensions
{
    public static class CategoryExtensions
    {
        public static void Map(this Category dbCategory, Category category)
        {
            dbCategory.Name = category.Name;
        }
    }
}