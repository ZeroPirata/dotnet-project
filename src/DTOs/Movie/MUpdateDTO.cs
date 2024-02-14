using System.ComponentModel.DataAnnotations;

namespace TrainingRestFullApi.src.DTOs.Movie
{
    public class MUpdateDTO
    {
        public string? Id { get; set; } = string.Empty;
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<string> Cast { get; set; } = [];
        public List<string>? Genere { get; set; } = [];
    }
}
