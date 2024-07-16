using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiess.Models
{
    public class Town
    {
        [Key]
        public int TownId { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City? City { get; set; }
        public string? TownName { get; set; }
    }
}
