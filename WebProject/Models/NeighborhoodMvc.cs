using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class NeighborhoodMvc
    {
        public int NeighborhoodId { get; set; }
        public string NeighborhoodName { get; set; }
        public int LocalityId { get; set; }
        //[ForeignKey("LocalityId")]
        public LocalityMvc? Locality { get; set; }
    }

}
