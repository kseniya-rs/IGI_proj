using Database.Models;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SocialNetwork.Hubs
{
    public class ChatHub: Hub
    {
        IDialogsService _dialogsService;

        public ChatHub(IDialogsService dialogsService)
        {
            _dialogsService = dialogsService;
        }

        public async Task SendMessage(int dialogId, int senderId, string message)
        {
            var newMessage = new MessageModel();
            newMessage.DateSent = DateTime.Now;
            newMessage.AuthorID = senderId;
            newMessage.DialogID = dialogId;
            newMessage.Content = message;
            newMessage.ID = 0;
            _dialogsService.SendMessage(newMessage);

            await Clients.All.SendAsync("ReceiveMessage", dialogId, senderId, message);
        }
    }
}
