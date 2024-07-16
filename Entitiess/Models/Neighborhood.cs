using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiess.Models
{
    public class Neighborhood
    {
        [Key] public int NeighborhoodId { get; set; }
        public string NeighborhoodName { get; set; }
        public int LocalityId { get; set; }
        //[ForeignKey("LocalityId")]
        public Locality? Locality { get; set; }
        

    }
}
