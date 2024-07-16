using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitiess.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
    
        public int NeighborhoodId { get; set; }
        [ForeignKey("NeighborhoodId")]
        public Neighborhood? Neighborhood { get; set; }
        public string? DestinationTown { get; set; }
        public string? DestinationLocality { get; set; }
        public string? DestinationNeighborhood { get; set; }
        public DateTime? BookingTime { get; set; }
        public DateTime? BookingDate { get; set; }
    }
}
