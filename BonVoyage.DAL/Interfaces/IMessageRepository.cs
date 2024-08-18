using BonVoyage.DAL.Entities;

namespace BonVoyage.DAL.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);
        Task<List<Message>> GetAllMessagesAsync();
        Task SaveChangesAsync();
    }
}
