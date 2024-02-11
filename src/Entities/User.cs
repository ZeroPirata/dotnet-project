namespace TrainingRestFullApi.src.Entities
{
    public class User 
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool userConfirmed { get; set; } = false;

        public string? Salt { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
