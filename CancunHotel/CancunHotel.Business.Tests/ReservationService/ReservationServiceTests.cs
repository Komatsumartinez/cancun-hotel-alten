using CancunHotel.Business;
using CancunHotel.Repository.Contracts;
using Moq;

namespace CancunHotels.Business.Tests.ReservationServiceTests
{
    public class ReservationServiceTests
    {
        protected ReservationService Service { get; set; }
        protected Mock<IReservationRepository> MockReservationRepository { get; set; }
        public ReservationServiceTests()
        {
            MockReservationRepository = new Mock<IReservationRepository>();
            Service = new ReservationService(MockReservationRepository.Object);
        }
    }
}
