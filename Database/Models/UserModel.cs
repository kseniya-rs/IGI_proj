using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class UserModel
    {
        public enum SexEnumeration { Male, Female, Undefined }

        public int ID { get; set; }

        public string Name { get; set; }

        public SexEnumeration Sex { get; set; }

        public string Country { get; set; }

        public List<FriendshipModel> Friends { get; set; }

        public List<FriendshipModel> FriendOf { get; set; }

        public List<DialogModel> UserDialogs { get; set; }

        public List<DialogModel> DialogsToUser { get; set; }

        public List<MessageModel> Messages { get; set; }

        public CredentialModel Credential { get; set; } // primary

        public List<PostModel> Posts { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}