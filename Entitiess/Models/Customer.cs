using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiess.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        
        public int DetailsId { get; set; }

        [ForeignKey("DetailsId")]
        public Details? Details { get; set; }


    }
}
