using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.ViewModels
{
    public class DialogViewModel
    {
        public int ID { get; set; }

        public int AddresseeID { get; set; }

        public string Addressee { get; set; }

        public int MyID { get; set; }

        public string Me { get; set; }
  
        public List<MessageViewModel> Messages { get; set; }
    }
}
