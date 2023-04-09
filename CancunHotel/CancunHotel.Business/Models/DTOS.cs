using System;
using System.Collections.Generic;
using System.Text;

namespace CancunHotel.Business.Models
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
