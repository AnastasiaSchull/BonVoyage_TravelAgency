﻿
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
        public string? Address { get; set; }
        public string? Country { get; set; }
		public string? Role { get; set; }     
        public virtual ICollection<Review>? Reviews { get; set; }
		public virtual ICollection<Booking>? Bookings { get; set; }
		public virtual ICollection<CustomerPreference>? Preferences { get; set; }
        public virtual ICollection<Message>? Messages { get; set; }
        public virtual ICollection<UserConnection> Connections { get; set; } 

        // constructor for initializing collections
        public User()
        {
            Reviews = new List<Review>();
            Bookings = new List<Booking>();
            Preferences = new List<CustomerPreference>();
            Messages = new List<Message>();
            Connections = new List<UserConnection>();
        }
    }
}
