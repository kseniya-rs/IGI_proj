using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Database.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class FriendshipsRepository : BaseRepository<FriendshipModel>, IFriendshipsRepository
    {
        public FriendshipsRepository(DatabaseContext context) : base(context)
        {
        }

        public FriendshipModel GetFriendshipIfExists(int userId1, int userId2)
        {
            return DbSet.Where(f => (f.FriendID == userId1 && f.MeID == userId2) ||
                                    (f.MeID == userId1 && f.FriendID == userId2))
                                    .SingleOrDefault();
        }

        public List<FriendshipModel> GetUserFriends(int userId)
        {
            return DbSet.Where(f => f.FriendID == userId || f.MeID == userId)
                .Include(f => f.Friend)
                .Include(f => f.Me)
                .ToList();
        }
    }
}
