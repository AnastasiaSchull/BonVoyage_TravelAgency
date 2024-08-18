using BonVoyage.BLL.DTOs;
using BonVoyage.BLL.Interfaces;
using BonVoyage.DAL.Entities;
using BonVoyage.DAL.Interfaces;


public class ChatService : IChatService
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChatService(IUserRepository userRepository, IMessageRepository messageRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task SendMessageAsync(string username, string message, string sentTime)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user != null)
        {
            var newMessage = new Message { Text = message, Date = DateTime.Now, UserId = user.UserId };
            await _messageRepository.AddMessageAsync(newMessage);
            await _messageRepository.SaveChangesAsync();
        }
    }

    public async Task ConnectUserAsync(string userName, string connectionId)
    {
        var user = new User { ConnectionId = connectionId, UserName = userName, IsActive = true };
        await _userRepository.Create(user);
        await _unitOfWork.Save();
    }

    public async Task<List<MessageDTO>> GetAllMessagesAsync()
    {
        var messages = await _messageRepository.GetAllMessagesAsync();
        return messages.Select(m => new MessageDTO
        {
            ID = m.ID,
            Text = m.Text,
            Date = m.Date,
            UserName = m.User.UserName
        }).ToList();
    }

    public async Task DisconnectUserAsync(string connectionId)
    {
        var user = await _userRepository.GetUserByConnectionIdAsync(connectionId);
        if (user != null)
        {
            user.IsActive = false;
             _userRepository.Update(user);
            await _unitOfWork.Save();
        }
    }

    public async Task<List<UserDTO>> GetActiveUsersAsync()
    {
        var users = await _userRepository.GetActiveUsersAsync();
        return users.Select(u => new UserDTO
        {
            UserId = u.UserId,
            UserName = u.UserName,
            IsActive = u.IsActive
        }).ToList();
    }

}
