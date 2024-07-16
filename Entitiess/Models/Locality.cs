using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiess.Models
{
    public class Locality
    {
        [Key]
        public int LocalityId { get; set; }
        public string LocalityName {  get; set; }
        public int TownId { get; set; }
        [ForeignKey("TownId")]
        public Town? Town { get; set; }
    }
}
