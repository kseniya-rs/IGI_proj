using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Interfaces
{
    public interface IPostsRepository : IRepository<PostModel>
    {
        IEnumerable<PostModel> GetUserPosts(int userId);

        IEnumerable<PostModel> GetUserPosts(int userId, int offset = 0, int count = 1);
    }
}
