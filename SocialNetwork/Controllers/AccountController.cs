using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Database.Interfaces;
using Database.Models;
using Database;
using SocialNetwork.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        private ICredentialsRepository _credentialsRepository;
        private IUsersRepository _usersRepository;
        public AccountController(ICredentialsRepository credentialsRepository, IUsersRepository usersRepository)
        {
            _credentialsRepository = credentialsRepository;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(CredentialViewModel credential)
        {
            if (ModelState.IsValid)
            {
                var userCredential = _credentialsRepository.GetUserCredentialsIfValid(credential.Username, credential.Password);
                if (userCredential != null)
                {
                    await Authenticate(userCredential.Username, userCredential.Role);
                    return RedirectToAction("Index", "UserProfile");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(credential);
        }

        public async Task<IActionResult> Register(RegistrationViewModel registrationData)
        {
            if (ModelState.IsValid)
            {
                if (registrationData.Password != registrationData.RePassword)
                {
                    ModelState.AddModelError("RePassword", "Passwords don't match");
                }                    
                else if (!_credentialsRepository.UsernameExists(registrationData.Username))
                {
                    var newUserCredentials = new CredentialModel
                    {
                        Username = registrationData.Username,
                        Password = registrationData.Password,
                        Role = "user",
                    };

                    var newUser = new UserModel
                    {
                        Name = registrationData.Name,
                        Sex = registrationData.Sex,
                        Country = registrationData.Country,
                        Credential = newUserCredentials
                    };

                    _usersRepository.Create(newUser); 
                    
                    await Authenticate(newUserCredentials.Username, newUserCredentials.Role);
                    return RedirectToAction("Index", "UserProfile");
                }
                else
                    ModelState.AddModelError("Username", "Username already used");
            }
            return View(registrationData);
        }

        private async Task Authenticate(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}