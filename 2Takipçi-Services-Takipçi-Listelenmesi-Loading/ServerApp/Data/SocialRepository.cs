using System.Collections.Generic;
using System.Threading.Tasks;
using ServerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ServerApp.Data
{
    public class SocialRepository : ISocialRepository
    {
        private readonly SocialContext _context;

        public SocialRepository(SocialContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(i => i.Images)
                                           .FirstOrDefaultAsync(i =>i.Id==id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers(UserQueryParams userParams)
        {
            await Task.Delay(3000);
            var users =  _context.Users
                                .Where(i => i.Id!=userParams.UserId)
                                .Include(i => i.Images)
                                .AsQueryable();
            
            if (userParams.Followers)//takip edenler
            {
                var result= await GetFollows(userParams.UserId,false);
                users=users.Where(u =>result.Contains(u.Id));
            }
            
            if (userParams.Followings)
            {
                var result= await GetFollows(userParams.UserId,true);
                users=users.Where(u =>result.Contains(u.Id));
                
            }//takip edilenler{
                
            
            return await users.ToListAsync();
        }
        private async Task<IEnumerable<int>> GetFollows(int userId,bool IsFollowers)
        {
            var user = await _context.Users
                                    .Include(i =>i.Followers)
                                    .Include(i =>i.Followings)
                                    .FirstOrDefaultAsync(i => i.Id==userId);
            if (IsFollowers)//takipçiler
            {
                return user.Followers
                            .Where(i =>i.FollowerId==userId)
                            .Select(i =>i.UserId);
            }else//takip edilenler
            {
                return user.Followings
                            .Where(i =>i.UserId==userId)
                            .Select(i =>i.FollowerId);
            }
        }

        public async Task<bool> IsAlreadyFollowed(int followerUserId, int userdId)
        {
            return await _context.UserToUSers.AnyAsync(i =>i.FollowerId == followerUserId && i.UserId==userdId);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}