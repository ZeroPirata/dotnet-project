using System.Diagnostics.CodeAnalysis;

namespace TrainingRestFullApi.src.DTOs.Rating
{
    public class RatingDTO
    {
        [AllowNull]
        public decimal Score { get; set; }
        [AllowNull]
        public bool Like { get; set; }
        [AllowNull]
        public bool Assessmeted { get; set; }
    }
}
