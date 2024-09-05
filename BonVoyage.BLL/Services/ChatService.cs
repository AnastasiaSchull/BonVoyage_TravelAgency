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
        var user = await _userRepository.GetUserByUsernameAsync(userName);
        if (user != null)
        {
            var existingConnection = await _userRepository.GetUserByConnectionIdAsync(connectionId);
            if (existingConnection == null)
            {
                var userConnection = new UserConnection
                {
                    UserId = user.UserId,
                    ConnectionId = connectionId,
                    IsActive = true,
                    ConnectedAt = DateTime.Now
                };
                await _userRepository.CreateUserConnection(userConnection);
                await _unitOfWork.Save();
            }
            else
            {
                existingConnection.IsActive = true;
                _userRepository.UpdateUserConnection(existingConnection);
                await _unitOfWork.Save();
            }
        }
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
        var userConnection = await _userRepository.GetUserByConnectionIdAsync(connectionId);
        if (userConnection != null)
        {
            userConnection.IsActive = false;
            userConnection.DisconnectedAt = DateTime.Now;
            _userRepository.UpdateUserConnection(userConnection);
            await _unitOfWork.Save();
        }
    }

    public async Task<List<UserDTO>> GetActiveUsersAsync()
    {
        var userActivePairs = await _userRepository.GetActiveUsersAsync();
        return userActivePairs.Select(ua => new UserDTO
        {
            UserId = ua.User.UserId,
            UserName = ua.User.UserName,
            IsActive = ua.IsActive 
        }).ToList();
    }

}
