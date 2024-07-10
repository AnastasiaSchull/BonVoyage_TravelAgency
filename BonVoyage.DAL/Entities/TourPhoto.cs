using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class TourPhoto
	{
		public int TourPhotoId { get; set; }
		public int TourId { get; set; }
		public string? PhotoUrl { get; set; }
		public virtual Tour? Tour { get; set; }
	}
}
