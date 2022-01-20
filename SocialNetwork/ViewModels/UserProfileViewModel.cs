using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;

namespace SocialNetwork.ViewModels
{
    public class UserProfileViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Country { get; set; }

        public UserModel.SexEnumeration Sex { get; set; }

        public bool IsFriend { get; set; }

        public List<PostModel> Posts { get; set; }
    }
}
