using System;
using System.Collections.Generic;
using ServerApp.Models;

namespace ServerApp.DTO
{
    public class UserForDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }//user birth time
        public DateTime Created { get; set; }//account create time
        public DateTime LastActive { get; set; }//last active time
        public string City { get; set; }
        public string Country { get; set; }
        public string Introduction { get; set; }
        public string Hobbies { get; set; }
        public string ProfileImageUrl { get; set; }
        public List<ImagesForDetails> Images { get; set; }
    }
}