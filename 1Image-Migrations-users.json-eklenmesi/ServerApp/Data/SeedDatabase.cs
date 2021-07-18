using Microsoft.AspNetCore.Identity;
using ServerApp.Models;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ServerApp.Data
{
    public static class SeedDatabase
    {
        public static async Task Seed(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users =File.ReadAllText("Data/users.json");//oluşturduğumuz json dosyasını oku
                var listOfUsers = JsonConvert.DeserializeObject<List<User>>(users);//usersi al json türünden obje türüne User içerisindeki verilere göre list olarak çevir

                foreach(var user in listOfUsers)
                {
                    await userManager.CreateAsync(user,"SocialApp_123");//her kullanıcıya bu şifreyi verir
                }
            }
        }
    }
}