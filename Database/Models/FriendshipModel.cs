using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class FriendshipModel
    {
        public int ID { get; set; }

        public int MeID { get; set; }
  
        public UserModel Me { get; set; }

        public int FriendID { get; set; }

        public UserModel Friend { get; set; }
    }
}
