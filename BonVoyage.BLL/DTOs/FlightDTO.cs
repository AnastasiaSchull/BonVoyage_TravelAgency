namespace BonVoyage.BLL.DTOs
{
    public class FlightDTO
    {
        public int FlightId { get; set; }
        public string? DepartureLocation { get; set; }
        public string? ArrivalLocation { get; set; }
        public decimal Price { get; set; }
        public string? Airline { get; set; }
        public string? FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int TourId { get; set; }
        public string? Tour { get; set; }
    }
}
