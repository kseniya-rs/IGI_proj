using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Services.Interfaces
{
    public interface IPostsService
    {
        IEnumerable<PostModel> GetUserPosts(int userId);

        IEnumerable<PostModel> GetUserPosts(int userId, int offset=0, int count=1);

        IEnumerable<PostModel> GetUserFriendsPosts(int userId, int offset = 0, int count = 1);

        void PostNewPost(PostModel post);

        PostModel GetPostIfExists(int postId);

        void RemovePost(int postId);
    }
}
