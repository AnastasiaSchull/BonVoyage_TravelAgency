using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class Tour
	{
		public int TourId { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int Duration { get; set; }
		public decimal Price { get; set; }
		public string? Country { get; set; }
		public string? Route { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		//navigation properties 
		public virtual ICollection<Review>? Reviews { get; set; }
		public virtual ICollection<Booking>? Bookings { get; set; }
		public virtual ICollection<Hotel>? Hotels { get; set; }
		public virtual ICollection<TourPhoto>? TourPhotos { get; set; }


	}
}
