using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.UnitTests.Extensions
{
    public static class MovieStoreDbContextExtensions
    {
        public static void Initialize(this MovieStoreDbContext context)
        {
            if (!context.Movies.Any())
            {
                context.Genres.AddRange(GetGenres());
                context.Directors.AddRange(GetDirector());
                context.Actors.AddRange(GetActor());
                context.SaveChanges();

                context.Movies.AddRange(GetMovie(context));
                context.SaveChanges();

                context.ActorsMovies.AddRange(GetActorMovie(context));
                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {
                context.Customers.AddRange(GetCustomers(context));
                context.SaveChanges();
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetOrders(context));
                context.SaveChanges();
            }
        }

        private static List<ActorMovie> GetActorMovie(MovieStoreDbContext context)
        {
            var actorsInception = new List<ActorMovie>()
            {
                new ActorMovie()
                {
                    MovieId=context.Movies.FirstOrDefault(m=>m.Title=="Inception").Id,
                    ActorId=context.Actors.FirstOrDefault(a=>a.Name=="Leonardo" && a.Surname=="DiCaprio").Id
                }
            };
            var actorsSavingPrivateRyan = new List<ActorMovie>()
            {
                new ActorMovie()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "Saving Private Ryan").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Tom" && a.Surname == "Hanks").Id
                }
            };
            var actorsPulpFiction = new List<ActorMovie>()
            {
                new ActorMovie()
                {
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "Pulp Fiction").Id,
                    ActorId = context.Actors.FirstOrDefault(a => a.Name == "Brad" && a.Surname == "Pitt").Id
                }
            };
            
            var actorsMovies = new List<ActorMovie>();
            actorsMovies.AddRange(actorsInception);
            actorsMovies.AddRange(actorsSavingPrivateRyan);
            actorsMovies.AddRange(actorsPulpFiction);
            return actorsMovies;
        }

        private static List<Movie> GetMovie(MovieStoreDbContext context)
        {
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Title="Inception",
                    DirectorId=context.Directors.FirstOrDefault(d=>d.Name=="Christopher" && d.Surname=="Nolan").Id,
                    GenreId=context.Genres.FirstOrDefault(g=>g.Name=="Action").Id,
                    YearOfMovie=new DateTime(2010,1,1),
                    Price=150
                },
                new Movie()
                {
                    Title="Saving Private Ryan",
                    DirectorId=context.Directors.FirstOrDefault(d=>d.Name=="Steven" && d.Surname=="Spielberg").Id,
                    GenreId=context.Genres.FirstOrDefault(g=>g.Name=="Drama").Id,
                    YearOfMovie=new DateTime(1998,1,1),
                    Price=120
                },
                new Movie()
                {
                    Title="Pulp Fiction",
                    DirectorId=context.Directors.FirstOrDefault(d=>d.Name=="Quentin" && d.Surname=="Tarantino").Id,
                    GenreId=context.Genres.FirstOrDefault(g=>g.Name=="Drama").Id,
                    YearOfMovie=new DateTime(1994,1,1),
                    Price=100
                }
            };
            return movies;
        }

        private static List<Actor> GetActor()
        {
            var actors = new List<Actor>
            {
                new Actor { Name = "Leonardo", Surname = "DiCaprio" }, // Inception
                new Actor { Name = "Tom", Surname = "Hanks" },    // Spielberg filmleri
                new Actor { Name = "Brad", Surname = "Pitt" }    // Tarantino filmleri

            };
            return actors;
        }

        private static List<Director> GetDirector()
        {
            var directors = new List<Director>()
            {
                new Director() { Name="Christopher", Surname="Nolan" },
                new Director() { Name="Steven", Surname="Spielberg" },
                new Director() { Name="Quentin", Surname="Tarantino" }
            };
            
            return directors;
        }
        
        private static List<Customer> GetCustomers(MovieStoreDbContext context)
        {
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "John",
                    Surname = "Doe",
                    Email = "johndoe@example.com",
                    Password = "john123",
                    FavoriteGenre = new List<Genre> { context.Genres.FirstOrDefault(g => g.Name == "Drama") },
                    IsActive = true
                },
                new Customer()
                {
                    Name = "Alice",
                    Surname = "Johnson",
                    Email = "alicej@example.com",
                    Password = "alice123",
                    FavoriteGenre = new List<Genre> { context.Genres.FirstOrDefault(g => g.Name == "Science Fiction") },
                    IsActive = true
                },
                new Customer()
                {
                    Name = "Bob",
                    Surname = "Smith",
                    Email = "bobsmith@example.com",
                    Password = "bob123",
                    FavoriteGenre = new List<Genre> { context.Genres.FirstOrDefault(g => g.Name == "Action") },
                    IsActive = true
                }
            };
            return customers;
        }
        
        private static List<Order> GetOrders(MovieStoreDbContext context)
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    TransactionTime = DateTime.Now,
                    CustomerId = context.Customers.FirstOrDefault(c => c.Name == "John").Id,
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "Inception").Id
                },
                new Order()
                {
                    TransactionTime = DateTime.Now.AddDays(-1),
                    CustomerId = context.Customers.FirstOrDefault(c => c.Name == "Alice").Id,
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "Saving Private Ryan").Id
                },
                new Order()
                {
                    TransactionTime = DateTime.Now.AddDays(-2),
                    CustomerId = context.Customers.FirstOrDefault(c => c.Name == "Bob").Id,
                    MovieId = context.Movies.FirstOrDefault(m => m.Title == "Pulp Fiction").Id
                }
            };
            return orders;
        }


        private static List<Genre> GetGenres()
        {
            var genres = new List<Genre>()
            {
                new Genre() { Name="Science Fiction" },
                new Genre() { Name="Action" },
                new Genre() { Name="Music" },
                new Genre() { Name="Drama" },
                new Genre() { Name="Comedy"},
                new Genre() { Name="Horror"},
                new Genre() { Name="Thriller"},
            };
            return genres;
        }
    }
}
