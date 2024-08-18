using BonVoyage.BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;


namespace BonVoyage_TravelAgency.SignalR
{
    /*
    Ключевой сущностью в SignalR, через которую клиенты обмениваются сообщеними 
    с сервером и между собой, является хаб (hub). 
    Хаб представляет некоторый класс, который унаследован от абстрактного класса 
    Microsoft.AspNetCore.SignalR.Hub.
    */

    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        // Отправка сообщений
        public async Task Send(string username, string message, string sentTime)
        {
            await _chatService.SendMessageAsync(username, message, sentTime);
            // Вызов метода AddMessage на всех клиентах
            await Clients.All.SendAsync("AddMessage", username, message, sentTime);
        }

        // Подключение нового пользователя
        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;
            await _chatService.ConnectUserAsync(userName, id);

            var messages = await _chatService.GetAllMessagesAsync();
            // Отправляем историю сообщений только что подключившемуся пользователю
            await Clients.Caller.SendAsync("ReceiveMessageHistory", messages);

            var activeUsers = await _chatService.GetActiveUsersAsync();
            // Вызов метода Connected и NewUserConnected
            await Clients.Caller.SendAsync("Connected", id, userName, activeUsers);
            await Clients.AllExcept(id).SendAsync("NewUserConnected", id, userName);
        }

        // OnDisconnectedAsync срабатывает при отключении клиента
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var id = Context.ConnectionId;
            await _chatService.DisconnectUserAsync(id);
            var activeUsers = await _chatService.GetActiveUsersAsync();
            await Clients.All.SendAsync("UpdateActiveUsers", activeUsers);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
