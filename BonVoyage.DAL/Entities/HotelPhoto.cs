using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class HotelPhoto
	{
		public int HotelPhotoId { get; set; }
		public int HotelId { get; set; }
		public string? PhotoUrl { get; set; }
		public virtual Hotel? Hotel { get; set; }
	}
}
