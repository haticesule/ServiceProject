using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models
{
    public class LocalityMvc
    {
        public int LocalityId { get; set; }
        public string? LocalityName { get; set; }
        public int TownId { get; set; }
        public TownMvc? Town { get; set; }
    }
}
