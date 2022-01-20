using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Interfaces;
using SocialNetwork.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetwork.Services
{
    public class UsersService : IUsersService
    {
        IUsersRepository _usersRepository;
        ICredentialsRepository _credentialsRepository;
        IFriendshipsRepository _friendshipsRepository;

        public UsersService(IUsersRepository usersRepository, 
                            ICredentialsRepository credentialsRepository,
                            IFriendshipsRepository friendshipsRepository)
        {
            _usersRepository = usersRepository;
            _credentialsRepository = credentialsRepository;
            _friendshipsRepository = friendshipsRepository;
        }

        public UserModel GetUserById(int userId)
        {
            return _usersRepository.GetUserById(userId);
        }

        public UserModel GetUserByUsername(string userName)
        {
            var cred = _credentialsRepository.GetUserCredentialsByUsername(userName);
            return _usersRepository.GetUserById(cred.UserID);
        }

        public int GetUserIDByUsername(string userName)
        {
            var cred = _credentialsRepository.GetUserCredentialsByUsername(userName);
            if (cred != null)
                return cred.UserID;
            throw new ArgumentException("No such username!!!!)))");
        }

        public void UpdateUser(UserModel model)
        {
            _usersRepository.Update(model);
        }

        public List<UserModel> GetUsersByPartialName(string name)
        {
            return _usersRepository.GetUsersByPartialName(name);
        }

        public List<UserModel> GetUsers()
        {
            return _usersRepository.GetItems();
        }

        public void CreateFriendship(FriendshipModel friendship)
        {
            _friendshipsRepository.Create(friendship);
        }

        public void CreateFriendship(int userId1, int userId2)
        {
            _friendshipsRepository.Create(new FriendshipModel
            {
                MeID = userId1,
                FriendID = userId2
            });
        }

        public FriendshipModel GetFriendshipIfExists(int userId1, int userId2)
        {
            return _friendshipsRepository.GetFriendshipIfExists(userId1, userId2);
        }

        public List<FriendshipModel> GetUserFriends(int userId)
        {
            return _friendshipsRepository.GetUserFriends(userId);
        }

        public void RemoveFriendship(int friendshipId)
        {
            _friendshipsRepository.Remove(friendshipId);
        }

        public bool RemoveFriendshipIfExists(int userId1, int userId2)
        {
            var f = _friendshipsRepository.GetFriendshipIfExists(userId1, userId2);
            if (f is null)
            {
                return false;
            }
            _friendshipsRepository.Remove(f.ID);
            return true;
        }
    }
}
