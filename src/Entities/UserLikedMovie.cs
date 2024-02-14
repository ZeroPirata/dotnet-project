using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingRestFullApi.src.Entities;

public class UserLikedMovie
{
    [Key]
    public Guid LikeId { get; set; } 
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    [ForeignKey("Movie")]
    public Guid MovieId { get; set; }
    public decimal Score { get; set; }
    public bool? LikedMovie { get; set; }
    public Movie? ViewLikedMovie { get; set; }
    public bool? AssessmentedMovie { get; set; }
}
