using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class RecommendationBasis
    {
        public int Id { get; set; }
        [DisplayName("What is your favorite artist?")]
        public string Artist { get; set; }
        [DisplayName("What tempo (BPM) do you prefer?")]
        [Range(0, 320, ErrorMessage = "Tempo must be in the range 0-320")]
        public float? Tempo { get; set; }
        [DisplayName("What is your current energy level percentage?")]
        [Range(0, 100, ErrorMessage = "Energy level must be in the range 0-100")]
        public float? Energy { get; set; }
        [DisplayName("Would you like to dance?")]
        public bool Danceability { get; set; }
        [DisplayName("Are you in a good mood?")]
        public bool Mode { get; set; }
    }
}