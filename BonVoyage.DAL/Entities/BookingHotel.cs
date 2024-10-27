using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class BookingHotel
	{
		public int BookingHotelId { get; set; }
		public int UserId { get; set; }
		public int HotelId { get; set; }
		public DateTime BookingDate { get; set; }
		public int NumberOfPeople { get; set; }
		public string? Status { get; set; }//Pending,Confirmed, Cancelled etc

		public virtual User? User { get; set; }
		public virtual Hotel? Hotel { get; set; }
	}
}
