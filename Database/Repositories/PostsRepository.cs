using System;
using System.Collections.Generic;
using System.Text;
using Database.Models;
using Database.Interfaces;
using System.Linq;

namespace Database.Repositories
{
    public class PostsRepository : BaseRepository<PostModel>, IPostsRepository
    {
        public PostsRepository(DatabaseContext context) : base(context)
        {
            
        }

        public IEnumerable<PostModel> GetUserPosts(int userId)
        {
            return DbSet.Where(p => p.AuthorID == userId);
        }

        public IEnumerable<PostModel> GetUserPosts(int userId, int offset = 0, int count = 1)
        {
            return DbSet.Skip(offset * count).Take(count);
        }
    }
}
