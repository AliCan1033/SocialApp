using System.Threading.Tasks;
using ServerApp.Models;
using System.Collections.Generic;

namespace ServerApp.Data
{
    public interface ISocialRepository
    {
        void Add<T>(T entity) where T:class;//burada sınıfı generic yapmak yerine methodları generic yaptık
        void Delete<T>(T entity) where T:class;
        Task<bool> SaveChanges();
         Task<User> GetUser(int id);
         Task<IEnumerable<User>> GetUsers();
    }
}