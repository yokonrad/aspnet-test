using Microsoft.EntityFrameworkCore;
using Test.Data;

namespace Test.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TestContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TestContext>>()))
            {
                if (context.Actor.Any() || context.Movie.Any() || context.ActorMovie.Any())
                {
                    return;
                }

                var actor1 = new Actor { Name = "Marlon Brando" };
                var actor2 = new Actor { Name = "Al Pacino" };

                context.Add(actor1);
                context.Add(actor2);

                var movie1 = new Movie { Name = "The Godfather" };
                var movie2 = new Movie { Name = "Scarface" };

                context.Add(movie1);
                context.Add(movie2);

                context.SaveChanges();

                var actorMovies1 = new ActorMovie() { ActorId = actor1.Id, MovieId = movie1.Id };
                var actorMovies2 = new ActorMovie() { ActorId = actor2.Id, MovieId = movie1.Id };
                var actorMovies3 = new ActorMovie() { ActorId = actor2.Id, MovieId = movie2.Id };

                context.Add(actorMovies1);
                context.Add(actorMovies2);
                context.Add(actorMovies3);

                context.SaveChanges();
            }
        }
    }
}
