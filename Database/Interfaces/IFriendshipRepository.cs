using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Interfaces
{
    public interface IFriendshipsRepository : IRepository<FriendshipModel>
    {
        FriendshipModel GetFriendshipIfExists(int userId1, int userId2);

        List<FriendshipModel> GetUserFriends(int userId);
    }
}
