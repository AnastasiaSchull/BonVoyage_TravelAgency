using BonVoyage.DAL.EF;
using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

public class MessageRepository : IMessageRepository
{
    private readonly BonVoyageContext db;

    public MessageRepository(BonVoyageContext context)
    {
        db = context;
    }

    public async Task AddMessageAsync(Message message)
    {
         db.Messages.Add(message);
    }

    public async Task<List<Message>> GetAllMessagesAsync()
    {
        return await db.Messages.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await db.SaveChangesAsync();
    }
}
