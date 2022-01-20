using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class CredentialModel
    {
        public int ID { get; set; }

        public int UserID { get; set; } // dependent

        public UserModel User { get; set; } 

        public string Role { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
