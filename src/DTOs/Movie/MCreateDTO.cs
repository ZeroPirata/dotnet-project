using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace TrainingRestFullApi.src.DTOs.Movie
{
    public class MCreateDTO
    {
        public string? Id { get; set; } = string.Empty;

        [Required]
        public string? Title { get; set; } = string.Empty;

        [Required]
        public string? Description { get; set; } = string.Empty;

        [Required]
        public Dictionary<string, string>? Crew { get; set; }

        [Required]
        public List<string> Cast { get; set; } = [];

        [Required]
        public List<string>? Genere { get; set; } = [];


    }
}
