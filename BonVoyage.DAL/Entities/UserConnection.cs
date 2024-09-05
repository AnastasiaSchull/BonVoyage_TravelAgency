
namespace BonVoyage.DAL.Entities
{
    public class UserConnection
    {
        public int UserConnectionId { get; set; }
        public int UserId { get; set; }
        public string? ConnectionId { get; set; } 
        public bool IsActive { get; set; } // состояние активности пользователя
        public DateTime ConnectedAt { get; set; } 
        public DateTime? DisconnectedAt { get; set; } 

        public virtual User? User { get; set; } 
    }
}
