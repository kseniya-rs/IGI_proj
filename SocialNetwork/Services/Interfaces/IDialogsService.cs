using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services.Interfaces
{
    public interface IDialogsService
    {
        List<DialogModel> GetUserDialogs(int userID);

        DialogModel GetDialog(int id);

        void CreateDialog(DialogModel dialog);

        void SendMessage(MessageModel message);

        void RemoveDialog(int id);

        DialogModel GetDialogBetweenUsersIfExists(int userId1, int userId2);
    }
}
