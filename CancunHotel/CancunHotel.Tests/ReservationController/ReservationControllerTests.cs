using AutoMapper;
using CancunHotel.Business.Contracts;
using CancunHotel.Business.Models;
using CancunHotel.Controllers;
using CancunHotel.Repository.Models;
using Moq;
using System;
using Xunit;

namespace CancunHotel.Tests.Controllers
{
    public class ReservationControllerTests
    {
        public Mock<IReservationService> MockService { get; set; }
        public IMapper MockMapper { get; set; }

        public ReservationsController Controller { get; set; }
        public ReservationControllerTests()
        {
            MockService = new Mock<IReservationService>();
            MockMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Reservation, ReservationDto>().ReverseMap();
            }).CreateMapper();
            Controller = new ReservationsController(MockService.Object, MockMapper);
        }

    }
}
