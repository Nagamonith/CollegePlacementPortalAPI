using System.ComponentModel.DataAnnotations;

namespace CollegePlacementAPI.Models
{
    public class PlacementOfficer
    {
        [Key]
        public int OfficerId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "PlacementOfficer";
    }
}
