using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Business.Models
{
    public class EntityModel
    {
        public Guid ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
