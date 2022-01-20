using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services.Interfaces;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [Route("Frienships/[action]")]
    public class FriendshipsController : Controller
    {
        IUsersService _usersService;
        IMapper _mapper;
        public FriendshipsController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var user = _usersService.GetUserByUsername(User.Identity.Name);
            ViewData["MyID"] = user.ID;
            
            var friendships = _usersService.GetUserFriends(user.ID);
            var friendshipsVMs = _mapper.Map<List<FriendshipViewModel>>(friendships);
            foreach (var f in friendshipsVMs)
            {
                if (f.MeID != user.ID)
                {
                    f.FriendId = f.MeID;
                    f.MeID = user.ID;
                }
                var friendVM = _usersService.GetUserById(f.FriendId);
                f.Friend = _mapper.Map<UsersListItemViewModel>(friendVM);
            }
            return View("Index",friendshipsVMs);
        }

        public IActionResult NewFriendship(int id)
        {
            var myId = _usersService.GetUserIDByUsername(User.Identity.Name);
            _usersService.CreateFriendship(myId, id);
            return Redirect($"/UserProfile/UserProfile?id={id}");
        }

        public IActionResult GetPeople([FromQuery]string name)
        {
            List<UserModel> users;
            if (name == null || name == "")
            {
                users = _usersService.GetUsers();
            }
            else
            {
                users = _usersService.GetUsersByPartialName(name);
            }
            return View("Search", _mapper.Map<List<UsersListItemViewModel>>(users));
        }

        
       
        public IActionResult RemoveFriendship(int id)
        {
            _usersService.RemoveFriendship(id);
            return Index();
        }


        public IActionResult RemoveFriendshipWithUser(int userId)
        {
            var myId = _usersService.GetUserIDByUsername(User.Identity.Name);
            _usersService.RemoveFriendshipIfExists(myId, userId);
            return Redirect($"/UserProfile/UserProfile?id={userId}");
        }
    }
} 