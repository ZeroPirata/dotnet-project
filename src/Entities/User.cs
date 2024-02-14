using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrainingRestFullApi.src.Entities
{
    public class User 
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        [JsonIgnore]
        public bool userConfirmed { get; set; } = false;
        [JsonIgnore]
        public string? Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        [JsonIgnore]
        public List<UserRole>? UserRoles { get; set; }
        public List<UserLikedMovie>? MovieLiked { get; set; }
        public User()
        {
            CreatedAt = DateTime.Now.ToUniversalTime();
        }
    }
}
