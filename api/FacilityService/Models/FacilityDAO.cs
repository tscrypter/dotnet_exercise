using System.ComponentModel.DataAnnotations;

namespace FacilityService.Models
{
    public class FacilityDAO
    {
        
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        public string State { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}