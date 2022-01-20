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
    public class UserProfileController : Controller
    {
        IUsersService _usersService;
        IMapper _mapper;
        IPostsService _postsService;

        public UserProfileController(IUsersService usersService, IPostsService postsService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
            _postsService = postsService;
        }

        public IActionResult Index()
        {
            var user =_usersService.GetUserByUsername(User.Identity.Name);
            var userVM = _mapper.Map<UserProfileViewModel>(user);
            userVM.Posts = _postsService.GetUserPosts(user.ID).ToList();
            return View("MyProfile", userVM);
        }

        public IActionResult UserProfile(int id)
        {
            int myId = _usersService.GetUserIDByUsername(User.Identity.Name);
            if (id != myId)
            {
                var user = _usersService.GetUserById(id);
                bool isFriendOfCurrentUser = _usersService.GetFriendshipIfExists(id, myId) != null;
                var profile = _mapper.Map<UserProfileViewModel>(user, opts => opts.Items["IsFriend"] = isFriendOfCurrentUser);
                profile.Posts = _postsService.GetUserPosts(id).ToList();
                return View("UserProfile", profile);
            }
            return Index();
        }

        [HttpGet]
        public IActionResult UpdateProfile()
        {
            int myId = _usersService.GetUserIDByUsername(User.Identity.Name);
            var user = _usersService.GetUserById(myId);
            return View("EditProfile", _mapper.Map<UserProfileViewModel>(user));
        }

        [HttpPost]
        public IActionResult UpdateProfile(int id, UserProfileViewModel userProfile)
        {
            int myId = _usersService.GetUserIDByUsername(User.Identity.Name);
            if (!User.IsInRole("admin") && myId != id)
                return BadRequest();
            userProfile.ID = myId;
            _usersService.UpdateUser(_mapper.Map<UserModel>(userProfile));
            return Index();
        }

        public IActionResult PostNewPost(PostModel post)
        {
            post.AuthorID = _usersService.GetUserIDByUsername(User.Identity.Name);
            _postsService.PostNewPost(post);
            return Index();
        }

        public IActionResult RemovePost(int id)
        {
            _postsService.RemovePost(id);
            return Index();
        }
    }
}