using System;
using System.Collections.Generic;
using System.Text;
using Database.Models;
using Database.Interfaces;

namespace Database.Repositories
{
    public class MessagesRepository : BaseRepository<MessageModel>, IMessagesRepository
    {
        public MessagesRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
