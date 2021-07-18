using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;//identityuser için

namespace ServerApp.Models
{
    public class User:IdentityUser<int>//normalde id bilgisini string random karekter dizisi yapıyor bir burada int olacağını belittik
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }//user birth time
        public DateTime Created { get; set; }//account create time
        public DateTime LastActive { get; set; }//last active time
        public string City { get; set; }
        public string Country { get; set; }
        public string Introduction { get; set; }
        public string Hobbies { get; set; }
        public ICollection<Image> Images { get; set; }//kullanıcıya ait profiil fotosu ve ve fotonun özellikleri
        public ICollection<UserToUser> Followings { get; set; }//bir kullanıcının takip ettikleri
        public ICollection<UserToUser> Followers { get; set; }//bir kullanıcıyı takip edenler
    }
}