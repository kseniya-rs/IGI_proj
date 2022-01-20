using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Interfaces;
using Database.Models;

namespace SocialNetwork.Services.Interfaces
{
    public class PostsService: IPostsService
    {
        IPostsRepository _postsRepository;
        IFriendshipsRepository _friendshipsRepository;

        public PostsService(IPostsRepository postsRepository, IFriendshipsRepository friendshipsRepository)
        {
            _postsRepository = postsRepository;
            _friendshipsRepository = friendshipsRepository;
        }

        public PostModel GetPostIfExists(int postId)
        {
            return _postsRepository.GetItem(postId);
        }

        public IEnumerable<PostModel> GetUserFriendsPosts(int userId, int offset = 0, int count = 1)
        {
            var friendships = _friendshipsRepository.GetUserFriends(userId);
            var postsList = new List<PostModel>();
            foreach (var f in friendships)
            {
                var friendId = f.FriendID != userId ? f.FriendID : f.MeID;
                var posts = _postsRepository.GetUserPosts(friendId, offset, count);
                postsList.AddRange(posts);
            }
            return postsList.OrderByDescending(p => p.CreationDate);
        }

        public IEnumerable<PostModel> GetUserPosts(int userId)
        {
            return _postsRepository.GetUserPosts(userId).OrderByDescending(p=> p.CreationDate);
        }

        public IEnumerable<PostModel> GetUserPosts(int userId, int offset = 0, int count = 1)
        {
            return _postsRepository.GetUserPosts(userId, offset, count).OrderByDescending(p => p.CreationDate);
        }

        public void PostNewPost(PostModel post)
        {
            _postsRepository.Create(post);
        }

        public void RemovePost(int postId)
        {
            _postsRepository.Remove(postId);
        }
    }
}
