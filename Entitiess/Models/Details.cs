using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiess.Models
{
    public class Details
    {
        [Key]
        public int DetailsId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? Bday { get; set; }
        public string? Tc { get; set; }
        public Admin? Admin { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
    }
}
