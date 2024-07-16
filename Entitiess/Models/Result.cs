using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiess.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public Request? Request { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public State? State { get; set; }
        public int DriverId { get; set; }
        [ForeignKey("DriverId")]
        public Driver? DriverDetail { get; set; }


    }
}
