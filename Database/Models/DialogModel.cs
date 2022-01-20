using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class DialogModel
    {
        public int ID { get; set; }

        public string DialogName { get; set; }

        public int? InitiatorID { get; set; }

        public UserModel Initiator { get; set; }

        public int? AddresseeID { get; set; }

        public UserModel Addressee { get; set; }

        public List<MessageModel> Messages { get; set; }
    }
}
