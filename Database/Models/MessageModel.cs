using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class MessageModel
    {
        public int ID { get; set; }

        public int AuthorID { get; set; }

        public UserModel Author { get; set; }

        public int DialogID { get; set; }

        public DialogModel Dialog { get; set; }

        public DateTime DateSent { get; set; }

        public string Content { get; set; }
    }
}
