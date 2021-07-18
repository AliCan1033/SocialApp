using System;
using ServerApp.Models;

namespace ServerApp.DTO
{
    public class UserForListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }//user birth time
        public DateTime Created { get; set; }//account create time
        public DateTime LastActive { get; set; }//last active time
        public string City { get; set; }
        public string Country { get; set; }
        public ImagesForDetails Image { get; set; }
    }
}