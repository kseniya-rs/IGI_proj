using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Services.Interfaces;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        IUsersService _usersService;
        IPostsService _postsService;
        const int POSTS_ON_PAGE = 10;

        public NewsController(IUsersService usersService, IPostsService postsService)
        {
            _usersService = usersService;
            _postsService = postsService;
        }

        public IActionResult Notifications()
        {
            return View();
        }

        public IActionResult News([FromQuery]int? pageNumber)
        {
            var user = _usersService.GetUserByUsername(User.Identity.Name);
            var userNews = _postsService.GetUserFriendsPosts(user.ID, pageNumber ?? 0, POSTS_ON_PAGE);
            return View("News", userNews);
        }
    }
}