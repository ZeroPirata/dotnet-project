using Microsoft.EntityFrameworkCore;
using TrainingRestFullApi.src.Entities;

namespace TrainingRestFullApi.src.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Movie> Movies { get; set; }   
        public DbSet<Review> Reviews { get; set; }
    }
}
