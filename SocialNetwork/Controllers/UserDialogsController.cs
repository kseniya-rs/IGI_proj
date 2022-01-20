using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Interfaces;
using Database.Models;
using SocialNetwork.Services.Interfaces;
using SocialNetwork.ViewModels;
using AutoMapper;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class UserDialogsController: Controller
    {
        IDialogsService _dialogsService;
        IUsersService _usersService;
        IMapper _mapper;

        public UserDialogsController(IDialogsService dialogsService, IUsersService usersService, IMapper mapper)
        {
            _dialogsService = dialogsService;
            _usersService = usersService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            int userID = _usersService.GetUserIDByUsername(User.Identity.Name);
            return View("Index", _dialogsService.GetUserDialogs(userID));
        }

        public IActionResult NewDialog(int userId)
        {
            int myId = _usersService.GetUserIDByUsername(User.Identity.Name);

            var existingDialog = _dialogsService.GetDialogBetweenUsersIfExists(myId, userId);
            if (existingDialog != null)
                return Dialog(existingDialog.ID);

            var newDialog = new DialogModel
            {
                InitiatorID = myId,
                AddresseeID = userId,
                DialogName = "Dialog",
            };

            _dialogsService.CreateDialog(newDialog);
            return Dialog(newDialog.ID);
        }

        public IActionResult NewMessage(int id, MessageViewModel newMessage)
        {
            newMessage.DateSent = DateTime.Now;
            newMessage.AuthorID = _usersService.GetUserIDByUsername(User.Identity.Name);
            newMessage.DialogID = id;
            newMessage.ID = 0;
            _dialogsService.SendMessage(_mapper.Map<MessageModel>(newMessage));
            return Dialog(id); 
        }

        public IActionResult RemoveDialog(int id)
        {
            _dialogsService.RemoveDialog(id);
            return Index();
        }

        public IActionResult Dialog(int id)
        {
            var myId = _usersService.GetUserIDByUsername(User.Identity.Name);
            var dialog = _dialogsService.GetDialog(id);
            var messageVMs = _mapper.Map<List<MessageViewModel>>(dialog.Messages)
                                    .OrderBy(m => m.DateSent)
                                    .ToList();

            int addresseeID = (dialog.AddresseeID.Value == myId) ? dialog.InitiatorID.Value : dialog.AddresseeID.Value;
            string addressee = "";

            if (dialog.AddresseeID.Value == myId)
            {
                addresseeID = dialog.InitiatorID.Value;
                addressee = dialog.Initiator.Name;
            }
            else
            {
                addresseeID = dialog.AddresseeID.Value;
                addressee = dialog.Addressee.Name;
            }

            var dialogViewModel = new DialogViewModel
            {
                ID = id,
                Messages = messageVMs,
                AddresseeID = addresseeID,
                Addressee = addressee,
                MyID = myId,
                Me = User.Identity.Name
            };    

            return View("Dialog", dialogViewModel);
        }
    }
}
