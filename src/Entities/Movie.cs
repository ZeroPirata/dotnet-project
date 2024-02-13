using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrainingRestFullApi.src.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        
        public string? Title { get; set; }
        
        public string? Description { get; set; }

        [Column(TypeName = "jsonb")]
        public string? Ratings { get; set; }

        [Column(TypeName = "jsonb")]
        public string? Crew { get; set; }

        public List<string>? Cast { get; set; }

        public List<string>? Genere { get; set; }
       
        public List<Review>? Reviews { get; set; }


        [NotMapped]
        public decimal? Total {  get; set; }

        public Movie()
        {
            Ratings = JsonConvert.SerializeObject(new Dictionary<string, object>
            {
                { "1", 0 },
                { "2", 0 },
                { "3", 0 },
                { "4", 0 },
                { "5", 0 },
            }).ToString();
        }

        public void AddCrewMember(Dictionary<string, string> crewMember)
        {
            var crewDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(Crew ?? "{}");

            foreach (var kvp in crewMember)
            {
                crewDictionary![kvp.Key] = kvp.Value;
            }

            Crew = JsonConvert.SerializeObject(crewDictionary);
        }

        public async Task<Movie?> GetMovieByGuid(ApplicationDbContext dbMovie, Guid id)
        {
            var query = await dbMovie.Movies
                .Where(b => b.Id == id)
                .Select(t => new Movie
                {
                    Id = t.Id,
                    Cast = t.Cast,
                    Crew = t.Crew,
                    Description = t.Description,
                    Genere = t.Genere,
                    Ratings = t.Ratings,
                    Title = t.Title,
                    Total = t.CalcularTotal(t)
                }).FirstOrDefaultAsync();
            return query;
        }

        public async Task<List<Movie>> GetAllMovies(ApplicationDbContext dbMovie)
        {
            var query = await dbMovie.Movies.Select(t => new Movie
            {
                Id = t.Id,
                Cast = t.Cast,
                Crew = t.Crew,
                Description = t.Description,
                Genere = t.Genere,
                Ratings = t.Ratings,
                Title = t.Title,
                Total = t.CalcularTotal(t)
            })
            .ToListAsync();
            return query;
        }

        private decimal CalcularTotal(Movie t) => Convert.ToDecimal(JsonConvert.DeserializeObject<Dictionary<string, object>>(t.Ratings!)!["1"]) +
                Convert.ToDecimal(JsonConvert.DeserializeObject<Dictionary<string, object>>(t.Ratings!)!["2"]) +
                Convert.ToDecimal(JsonConvert.DeserializeObject<Dictionary<string, object>>(t.Ratings!)!["3"]) +
                Convert.ToDecimal(JsonConvert.DeserializeObject<Dictionary<string, object>>(t.Ratings!)!["4"]) +
                Convert.ToDecimal(JsonConvert.DeserializeObject<Dictionary<string, object>>(t.Ratings!)!["5"]);

    }
}
