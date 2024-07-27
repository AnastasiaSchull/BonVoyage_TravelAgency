using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.BLL.Infrastructure;
using BonVoyage.BLL.DTOs;
using AutoMapper;

namespace BonVoyage.BLL.Services
{
    public class FlightService: IFlightService
    {
        IUnitOfWork Database { get; set; }

        public FlightService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task CreateFlightAsync(FlightDTO flightDTO)
        {
            var flight = new Flight
            {
                FlightId = flightDTO.FlightId,
                DepartureLocation = flightDTO.DepartureLocation,
                ArrivalLocation = flightDTO.ArrivalLocation,
                Price = flightDTO.Price,
                Airline = flightDTO.Airline,
                FlightNumber = flightDTO.FlightNumber,
                DepartureTime = flightDTO.DepartureTime,
                ArrivalTime = flightDTO.ArrivalTime,
                Duration = flightDTO.Duration,
                TourId = flightDTO.TourId
            };
            await Database.Flights.Create(flight);
            await Database.Save();
        }
        public async Task UpdateFlightAsync(FlightDTO flightDTO)
        {
            var flight = new Flight
            {
                FlightId = flightDTO.FlightId,
                DepartureLocation = flightDTO.DepartureLocation,
                ArrivalLocation = flightDTO.ArrivalLocation,
                Price = flightDTO.Price,
                Airline = flightDTO.Airline,
                FlightNumber = flightDTO.FlightNumber,
                DepartureTime = flightDTO.DepartureTime,
                ArrivalTime = flightDTO.ArrivalTime,
                Duration = flightDTO.Duration,
                TourId = flightDTO.TourId
            };
            Database.Flights.Update(flight);
            await Database.Save();
        }
        public async Task DeleteFlightAsync(int id)
        {
            await Database.Flights.Delete(id);
            await Database.Save();
        }
        public async Task<FlightDTO> GetFlightByIdAsync(int id)
        {
            var flight = await Database.Flights.Get(id);
            if (flight == null)
                throw new ValidationException("Wrong Flight!", "");
            return new FlightDTO
            {
                FlightId = flight.FlightId,
                DepartureLocation = flight.DepartureLocation,
                ArrivalLocation = flight.ArrivalLocation,
                Price = flight.Price,
                Airline = flight.Airline,
                FlightNumber = flight.FlightNumber,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                Duration = flight.Duration,
                TourId = flight.TourId,
                Tour = flight.Tour?.Title
            };
        }
        // Automapper 
        public async Task<IQueryable<FlightDTO>> GetAllFlightsAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Flight, FlightDTO>()
            .ForMember("Tour", opt => opt.MapFrom(c => c.Tour.Title)));
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<Flight>, IQueryable<FlightDTO>>(await Database.Flights.GetAll());
        }
    }
}
