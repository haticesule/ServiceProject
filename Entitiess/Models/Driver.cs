using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiess.Models
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        public int DetailsId { get; set; }
        [ForeignKey("DetailsId")]
        public Details? Details { get; set; }
        public string? Plaque { get; set; }
        public string? DrivingLicence { get; set; }
        public string? Registry { get; set; }
        public string? ServiceArea { get; set; }
    }
}
