using CancunHotel.Repository.Context;
using CancunHotel.Repository.Contracts;
using CancunHotel.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Linq;
using Xunit;

namespace CancunHotel.Repository.Tests
{
    public class ReservationRepositoryTests
    {
        private IReservationRepository _repository;
        private ApplicationDbContext _context;

        public ReservationRepositoryTests()
        {
            // Crear una instancia del contexto de la base de datos en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReservationDatabase")
                .Options;
            _context = new ApplicationDbContext(options);

            // Crear una instancia del ReservationRepository utilizando el contexto en memoria
            _repository = new ReservationRepository(_context);
        }

        [Fact]
        public void AddReservation_ShouldAddReservationToDatabase()
        {
            // Crear una nueva reserva
            var reservation = new Reservation
            {
                Id = new Guid(),
                StartDate = new DateTime(2023, 06, 01),
                EndDate = new DateTime(2023, 06, 03)
            };

            // Agregar la reserva al repositorio
            _repository.AddReservation(reservation);

            // Verificar que la reserva se agregó correctamente al contexto de la base de datos
            Assert.Equal(1, _context.Reservations.Count());
            Assert.Equal(reservation, _context.Reservations.Single());
        }

        [Fact]
        public void AddReservation_ShouldNotAllowOverlappingReservations()
        {
            // Agregar una reserva existente en la base de datos
            var existingReservation = new Reservation
            {
                Id = new Guid(),
                StartDate = new DateTime(2023, 06, 02),
                EndDate = new DateTime(2023, 06, 04)
            };
            _context.Reservations.Add(existingReservation);
            _context.SaveChanges();

            // Intentar agregar una nueva reserva que se superpone con la reserva existente
            var overlappingReservation = new Reservation
            {
                Id = new Guid(),

                StartDate = new DateTime(2023, 06, 03),
                EndDate = new DateTime(2023, 06, 05)
            };
            Assert.Throws<InvalidOperationException>(() => _repository.AddReservation(overlappingReservation));

            // Verificar que la reserva no se agregó a la base de datos
            Assert.Equal(1, _context.Reservations.Count());
            Assert.Equal(existingReservation, _context.Reservations.Single());
        }

        [Fact]
        public void GetReservation_ShouldReturnReservationWithMatchingId()
        {
            // Agregar una reserva existente en la base de datos
            var existingReservation = new Reservation
            {
                Id = new Guid(),

                StartDate = new DateTime(2023, 06, 01),
                EndDate = new DateTime(2023, 06, 03)
            };
            _context.Reservations.Add(existingReservation);
            _context.SaveChanges();

            // Obtener la reserva utilizando el ReservationRepository
            var reservation = _repository.GetReservation(existingReservation.Id);

            // Verificar que la reserva obtenida es igual a la reserva existente
            Assert.Equal(existingReservation, reservation);
        }

        [Fact]
        public void GetReservation_ShouldReturnNullForNonexistentId()
        {

            // Obtener una reserva que no existe en la base de datos
            var reservation = _repository.GetReservation(new Guid());

            // Verificar que la reserva obtenida es nula
            Assert.Null(reservation);

        }

        [Fact]
        public void CancelReservation_ShouldRemoveReservationFromDatabase()
        {
            // Agregar una reserva existente en la base de datos
            var existingReservation = new Reservation
            {
                Id = new Guid(),

                StartDate = new DateTime(2023, 06, 01),
                EndDate = new DateTime(2023, 06, 03)
            };
            _context.Reservations.Add(existingReservation);
            _context.SaveChanges();

            // Cancelar la reserva utilizando el ReservationRepository
            _repository.DeleteReservation(existingReservation.Id);

            // Verificar que la reserva se eliminó correctamente de la base de datos
            Assert.Equal(0, _context.Reservations.Count());
        }

        [Fact]
        public void CancelReservation_ShouldThrowExceptionForNonexistentId()
        {
            // Intentar cancelar una reserva que no existe en la base de datos
            Assert.Throws<InvalidOperationException>(() => _repository.DeleteReservation(new Guid()));

            // Verificar que no se eliminó ninguna reserva de la base de datos
            Assert.Equal(0, _context.Reservations.Count());
        }

        [Fact]
        public void ModifyReservation_ShouldUpdateReservationInDatabase()
        {
            // Agregar una reserva existente en la base de datos
            var existingReservation = new Reservation
            {
                Id = new Guid(),
                StartDate = new DateTime(2023, 06, 01),
                EndDate = new DateTime(2023, 06, 03)
            };
            _context.Reservations.Add(existingReservation);
            _context.SaveChanges();

            // Modificar la reserva utilizando el ReservationRepository
            existingReservation.StartDate = new DateTime(2023, 07, 01);
            existingReservation.EndDate = new DateTime(2023, 07, 03);
            _repository.UpdateReservation(existingReservation);

            // Verificar que la reserva se actualizó correctamente en la base de datos
            Assert.Equal(new DateTime(2023, 07, 01), _context.Reservations.Single().StartDate);
        }

        [Fact]
        public void ModifyReservation_ShouldThrowExceptionForOverlappingReservations()
        {
            // Agregar dos reservas existentes en la base de datos
            var existingReservation1 = new Reservation
            {
                Id = new Guid(),
                StartDate = new DateTime(2023, 06, 02),
                EndDate = new DateTime(2023, 06, 04)
            };
            var existingReservation2 = new Reservation
            {
                Id = new Guid(),
                StartDate = new DateTime(2023, 06, 05),
                EndDate = new DateTime(2023, 06, 07)
            };
            _context.Reservations.AddRange(existingReservation1, existingReservation2);
            _context.SaveChanges();

            // Intentar modificar la primera reserva para que se superponga con la segunda reserva
            existingReservation1.EndDate = new DateTime(2023, 06, 06);
            Assert.Throws<InvalidOperationException>(() => _repository.UpdateReservation(existingReservation1));

            // Verificar que la reserva no se actualizó en la base de datos
            Assert.Equal(existingReservation1.Id, _context.Reservations.Single(r => r.Id == existingReservation1.Id).Id);
        }

        [Fact]
        public void ModifyReservation_ShouldThrowExceptionForNonexistentId()
        {
            // Crear una nueva reserva que no existe en la base de datos
            var newReservation = new Reservation
            {
                Id = new Guid(),
                StartDate = new DateTime(2023, 06, 01),
                EndDate = new DateTime(2023, 06, 03)
            };

            // Intentar modificar la reserva que no existe en la base de datos
            Assert.Throws<InvalidOperationException>(() => _repository.UpdateReservation(newReservation));

            // Verificar que no se agregó ninguna reserva a la base de datos
            Assert.Equal(0, _context.Reservations.Count());
        }
    }
}
