using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiess.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [DefaultValue("1")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public AdminType? AdminType { get; set; }
        public int DetailsId { get; set; }
        [ForeignKey("DetailsId")]
        public Details? Details { get; set; }
        public string? Password { get; set; }
        
        public Admin()
        {
            this.TypeId = 1;
        }
    }
}
