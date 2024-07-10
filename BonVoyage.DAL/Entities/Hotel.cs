using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class Hotel
	{
		public int HotelId { get; set; }
		public string? Name { get; set; }
		public string? Location { get; set; }
		public decimal PricePerNight { get; set; }
		public int StarRating { get; set; }
		public bool HasSwimmingPool { get; set; }
		public int TourId { get; set; }
		public virtual Tour? Tour { get; set; }
		public virtual ICollection<Review>? Reviews { get; set; }
		public virtual ICollection<HotelPhoto>? HotelPhotos { get; set; }

	}
}
