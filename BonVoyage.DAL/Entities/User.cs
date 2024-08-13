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
        public string? ConnectionId { get; set; }// идентификатор соединения
        public bool IsActive { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
		public virtual ICollection<Booking>? Bookings { get; set; }
		public virtual ICollection<CustomerPreference>? Preferences { get; set; }
        public virtual ICollection<Message>? Messages { get; set; }
        // Конструктор для инициализации коллекций
        public User()
        {
            Reviews = new List<Review>();
            Bookings = new List<Booking>();
            Preferences = new List<CustomerPreference>();
            Messages = new List<Message>();
        }
    }
}
