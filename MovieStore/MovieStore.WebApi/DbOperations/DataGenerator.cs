using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.DbOperations
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
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
        }

        private static List<ActorMovie> GetActorMovie(MovieStoreDbContext context)
        {
            var movieDict = context.Movies.ToDictionary(m => m.Title, m => m.Id);
            var actorDict = context.Actors.ToDictionary(a => $"{a.Name} {a.Surname}", a => a.Id);
            
            var actorsInception = new List<ActorMovie>()
            {
                new ActorMovie()
                {
                    MovieId = movieDict["Inception"],
                    ActorId = actorDict["Leonardo DiCaprio"]
                }
            };
            var actorsSavingPrivateRyan = new List<ActorMovie>()
            {
                new ActorMovie()
                {
                    MovieId = movieDict["Saving Private Ryan"],
                    ActorId = actorDict["Tom Hanks"]
                }
            };
            var actorsPulpFiction = new List<ActorMovie>()
            {
                new ActorMovie()
                {
                    MovieId = movieDict["Pulp Fiction"],
                    ActorId = actorDict["Brad Pitt"]
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
            var genreDict = context.Genres.ToDictionary(g => g.Name, g => g.Id);
            var directorDict = context.Directors.ToDictionary(d => $"{d.Name} {d.Surname}", d => d.Id);
            
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Title="Inception",
                    DirectorId = directorDict["Christopher Nolan"],
                    GenreId = genreDict["Action"],
                    YearOfMovie=new DateTime(2010,1,1),
                    Price=150
                },
                new Movie()
                {
                    Title="Saving Private Ryan",
                    DirectorId = directorDict["Steven Spielberg"],
                    GenreId = genreDict["Drama"],
                    YearOfMovie=new DateTime(1998,1,1),
                    Price=120
                },
                new Movie()
                {
                    Title="Pulp Fiction",
                    DirectorId = directorDict["Quentin Tarantino"],
                    GenreId = genreDict["Drama"],
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
                new Actor { Name = "Tom", Surname = "Hanks" },    
                new Actor { Name = "Brad", Surname = "Pitt" }    

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
            var genreDict = context.Genres.ToDictionary(g => g.Name, g => g);

            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "John",
                    Surname = "Doe",
                    Email = "johndoe@example.com",
                    Password = "john123",
                    FavoriteGenre = new List<Genre> { genreDict["Drama"] },
                    IsActive = true
                },
                new Customer()
                {
                    Name = "Alice",
                    Surname = "Johnson",
                    Email = "alicej@example.com",
                    Password = "alice123",
                    FavoriteGenre = new List<Genre> { genreDict["Science Fiction"] },
                    IsActive = true
                },
                new Customer()
                {
                    Name = "Bob",
                    Surname = "Smith",
                    Email = "bobsmith@example.com",
                    Password = "bob123",
                    FavoriteGenre = new List<Genre> { genreDict["Action"] },
                    IsActive = true
                }
            };
            return customers;
        }
        
        private static List<Order> GetOrders(MovieStoreDbContext context)
        {
            var customerDict = context.Customers.ToDictionary(c => $"{c.Name}", c => c.Id);
            var movieDict = context.Movies.ToDictionary(m => m.Title, m => m.Id);
            
            var orders = new List<Order>()
            {
                new Order()
                {
                    TransactionTime = DateTime.Now,
                    CustomerId = customerDict["John"],
                    MovieId = movieDict["Inception"]
                },
                new Order()
                {
                    TransactionTime = DateTime.Now.AddDays(-1),
                    CustomerId = customerDict["Alice"],
                    MovieId = movieDict["Saving Private Ryan"]
                },
                new Order()
                {
                    TransactionTime = DateTime.Now.AddDays(-2),
                    CustomerId = customerDict["Bob"],
                    MovieId =  movieDict["Pulp Fiction"]
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
