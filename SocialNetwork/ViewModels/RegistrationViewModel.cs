using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;

namespace SocialNetwork.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public UserModel.SexEnumeration Sex { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
