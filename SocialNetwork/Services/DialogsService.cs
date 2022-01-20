using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Interfaces;
using SocialNetwork.Services.Interfaces;

namespace SocialNetwork.Services
{
    public class DialogsService : IDialogsService
    {
        IUsersRepository _usersRepository;
        IDialogsRepository _dialogsRepository;
        IMessagesRepository _messagesRepository;

        public DialogsService(IUsersRepository usersRepository, IDialogsRepository dialogsRepository, IMessagesRepository messagesRepository)
        {
            _usersRepository = usersRepository;
            _dialogsRepository = dialogsRepository;
            _messagesRepository = messagesRepository;
        }

        public List<DialogModel> GetUserDialogs(int userID)
        {
            return _dialogsRepository.GetUserDialogs(userID);
        }

        public void CreateDialog(DialogModel dialog)
        {
            _dialogsRepository.Create(dialog);
        }

        public DialogModel GetDialog(int id)
        {
            return _dialogsRepository.GetDialogWithMessages(id);
        }

        public void SendMessage(MessageModel message)
        {
            _messagesRepository.Create(message);
        }

        public void RemoveDialog(int id)
        {
            _dialogsRepository.Remove(id);
        }

        public DialogModel GetDialogBetweenUsersIfExists(int userId1, int userId2)
        {
            return _dialogsRepository.GetDialogBetweenUsersIfExists(userId1, userId2);
        }
    }
}
