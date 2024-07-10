using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class Promotion
	{
		public int PromotionId { get; set; }
		public string? Description { get; set; }
		public decimal Discount { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
