using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonVoyage.DAL.Entities
{
	public class User
	{
		public int UserId { get; set; }
		public string? UserName { get; set; }
		public string? UserSurname { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Salt { get; set; }
		public string? Role { get; set; }

		public virtual ICollection<Review>? Reviews { get; set; }
		public virtual ICollection<Booking>? Bookings { get; set; }
		public virtual ICollection<CustomerPreference>? Preferences { get; set; }

	}
}
