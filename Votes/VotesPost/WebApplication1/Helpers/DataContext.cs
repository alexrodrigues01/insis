using Microsoft.EntityFrameworkCore;
using Votes.Model;
using VotesApp.Model;

namespace Votes.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Vote>(entity =>
            {

                entity.HasIndex(e => e.reviewId, "IDX_REVIEWID");

                entity.HasIndex(e => e.userId, "IDX_USERID");

                entity.HasKey(e => new { e.reviewId, e.userId, }).HasName("PK_VOTES");

                entity.Property(e => e.reviewId).HasColumnName("ReviewID").HasPrecision(10);

                entity.Property(e => e.vote).HasColumnName("Vote").IsUnicode(false);

                entity.Property(e => e.userId).HasColumnName("UserID").HasPrecision(10);
            });

            modelBuilder.Entity<Review>(entity =>
            {

                entity.HasIndex(e => e.reviewId, "IDX_REVIEWID");

                entity.HasKey(e => new { e.reviewId}).HasName("PK_REVIEW");

                entity.Property(e => e.reviewId).HasColumnName("ReviewID").HasPrecision(10);
            });
        }
    }
}
