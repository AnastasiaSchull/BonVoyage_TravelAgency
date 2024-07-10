using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class Review
	{
		public int ReviewId { get; set; }
		public int UserId { get; set; }
		public int TourId { get; set; }
		public int? HotelId { get; set; }
		public string? Text { get; set; }
		public int Rating { get; set; }

		public virtual User? User { get; set; }
		public virtual Tour? Tour { get; set; }
		public virtual Hotel? Hotel { get; set; }
	}
}
