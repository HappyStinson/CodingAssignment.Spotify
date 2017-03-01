using System.Data.Entity;
using WebClient.Models;

namespace WebClient.DAL
{
    public class RecommendationContext : DbContext
    {
        public RecommendationContext() : base("​DefaultConnection ") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new RecommendationContextInitializer());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RecommendationBasis> Basis { get; set; }
    }
}