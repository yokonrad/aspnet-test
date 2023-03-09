using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Data
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public DbSet<Test.Models.Actor> Actor { get; set; }

        public DbSet<Test.Models.Movie> Movie { get; set; }

        public DbSet<Test.Models.ActorMovie> ActorMovie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ActorMovie>()
            //        .HasKey(t => new { t.ActorId, t.MovieId });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Actor)
                .WithMany(a => a.ActorMovies)
                .HasForeignKey(am => am.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Movie)
                .WithMany(m => m.ActorMovies)
                .HasForeignKey(am => am.MovieId);
        }
    }
}
