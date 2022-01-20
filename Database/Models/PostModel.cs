using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class PostModel
    {
        public int ID { get; set; }
        
        public int AuthorID { get; set; }

        public UserModel Author { get; set; }

        public string Content { get; set; }

        public string ImageLinkContent { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
