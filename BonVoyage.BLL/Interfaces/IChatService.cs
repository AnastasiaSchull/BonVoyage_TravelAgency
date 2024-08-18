using BonVoyage.BLL.DTOs;

namespace BonVoyage.BLL.Interfaces
{
    public interface IChatService
    {
        Task SendMessageAsync(string username, string message, string sentTime);
        Task ConnectUserAsync(string userName, string connectionId);
        Task<List<MessageDTO>> GetAllMessagesAsync();
        Task DisconnectUserAsync(string connectionId);
        Task<List<UserDTO>> GetActiveUsersAsync();
    }
}
