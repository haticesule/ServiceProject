using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models
{
    public class TownMvc
    {
        public int TownId { get; set; }
        public string? TownName { get; set; }
        public int CityId { get; set; }
        public CityMvc? City { get; set; }
    }
}
