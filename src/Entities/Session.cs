using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingRestFullApi.src.Entities
{
    public class Session
    {
        [Key]
        public Guid TokenId { get; set; }
        public User? User { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}
