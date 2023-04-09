using CancunHotel.Business.Models;
using CancunHotel.Repository.Models;

namespace CancunHotel
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }
    }
}
