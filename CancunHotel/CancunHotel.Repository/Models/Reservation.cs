using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Repository.Models
{
    public class Reservation
    { 
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
