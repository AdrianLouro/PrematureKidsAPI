using Entities.Models;

namespace Entities.Extensions
{
    public static class OpinionExtensions
    {
        public static void Map(this Opinion dbOpinion, Opinion opinion)
        {
            dbOpinion.Text = opinion.Text;
        }
    }
}