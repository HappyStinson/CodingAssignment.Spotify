using System.Data.Entity;
using WebClient.Models;

namespace WebClient.DAL
{
    internal sealed class RecommendationContextInitializer : DropCreateDatabaseAlways<RecommendationContext>
    {
        protected override void Seed(RecommendationContext context)
        {
            context.Basis.Add(new RecommendationBasis {Artist = "Kiss"});
        }
    }
}