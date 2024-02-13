using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingRestFullApi.src.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Comment {  get; set; }

        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }

        public Movie Movie { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
