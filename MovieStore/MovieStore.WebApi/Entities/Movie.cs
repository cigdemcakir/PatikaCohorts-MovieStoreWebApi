using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.WebApi.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime YearOfMovie { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public decimal Price { get; set; }
        public ICollection<ActorMovie> Actors { get; set; } = new List<ActorMovie>();
        
        public bool IsActive { get; set; } = true;
    }
}
