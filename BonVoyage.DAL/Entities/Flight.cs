using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class Flight
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
	}
}
