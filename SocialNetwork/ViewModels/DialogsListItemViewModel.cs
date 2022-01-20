using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.ViewModels
{
    public class DialogsListItemViewModel
    {
        public int ID { get; set; }

        public string DialogName { get; set; }

        public string LastMessageText { get; set; }
    }
}
