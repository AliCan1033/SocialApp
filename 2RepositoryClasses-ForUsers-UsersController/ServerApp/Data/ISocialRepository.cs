using System.Threading.Tasks;
using ServerApp.Models;
using System.Collections.Generic;

namespace ServerApp.Data
{
    public interface ISocialRepository
    {
         Task<User> GetUser(int id);
         Task<IEnumerable<User>> GetUsers();
    }
}