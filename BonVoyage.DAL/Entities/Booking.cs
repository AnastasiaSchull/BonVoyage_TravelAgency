using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class Booking
	{
		public int BookingId { get; set; }
		public int UserId { get; set; }
		public int TourId { get; set; }
		public DateTime BookingDate { get; set; }
		public int NumberOfPeople { get; set; }
		public string? Status { get; set; }//Pending,Confirmed, Cancelled etc

		public virtual User? User { get; set; }
		public virtual Tour? Tour { get; set; }
	}
}
