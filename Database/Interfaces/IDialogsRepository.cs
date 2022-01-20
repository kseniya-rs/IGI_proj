using System;
using System.Collections.Generic;
using System.Text;
using Database.Models;

namespace Database.Interfaces
{
    public interface IDialogsRepository: IRepository<DialogModel>
    {
        List<DialogModel> GetUserDialogs(int userID);
        DialogModel GetDialogWithMessages(int dialogID);
        DialogModel GetDialogBetweenUsersIfExists(int userId1, int userId2);
    }
}
