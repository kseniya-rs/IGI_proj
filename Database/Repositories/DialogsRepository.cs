using System;
using System.Collections.Generic;
using System.Text;
using Database.Models;
using Database.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class DialogsRepository : BaseRepository<DialogModel>, IDialogsRepository
    {
        public DialogsRepository(DatabaseContext context) : base(context)
        {
        }

        public List<DialogModel> GetUserDialogs(int userID)
        {
            return DbSet.Where(d => d.AddresseeID == userID || d.InitiatorID == userID)
                    .Include(d => d.Initiator)
                    .Include(d => d.Addressee)
                    .ToList();
        }

        public DialogModel GetDialogWithMessages(int dialogID)
        {
            var item = GetItem(dialogID);
            Context.Entry(item).Collection(d => d.Messages).Load();
            Context.Entry(item).Reference(d => d.Addressee).Load(); //TODO: is this really needed???
            Context.Entry(item).Reference(d => d.Initiator).Load();
            return item;
        }

        public DialogModel GetDialogBetweenUsersIfExists(int userId1, int userId2)
        {
            return DbSet.Where(d => (d.AddresseeID == userId1 && d.InitiatorID == userId2) ||
                                            (d.AddresseeID == userId2 && d.InitiatorID == userId1)
                                    ).SingleOrDefault();
        }
    }
}
