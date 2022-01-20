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
    public class AdministartionController : Controller
    {
        private IUsersService _usersService;
        
        public AdministartionController(IUsersService usersService)
        { }

        public IActionResult Index()
        {
            return View("Index", _usersService.GetUsers());
        }

        public IActionResult User(int id)
        {
            return View();
        }

        public IActionResult UserDialogs(int id)
        {
            return View();
        }

        public IActionResult RemoveUser(int id)
        {
            return View();
        }

        public IActionResult RemoveDialog(int id)
        {
            return View();
        }

        public IActionResult RemoveMessage(int id)
        {
            return View();
        }
    }
}