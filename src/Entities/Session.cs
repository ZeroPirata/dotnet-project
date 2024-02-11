namespace TrainingRestFullApi.src.Entities
{
    public class Session
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string? BerearToken { get; set; }
        public User? User { get; set; }
    }
}
