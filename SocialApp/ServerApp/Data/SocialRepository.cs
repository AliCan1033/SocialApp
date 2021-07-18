using System.Collections.Generic;
using System.Threading.Tasks;
using ServerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

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
            await Task.Delay(1000);
            var users =  _context.Users
                                .Where(i => i.Id!=userParams.UserId)
                                .Include(i => i.Images)
                                .OrderByDescending(i =>i.LastActive)//sıralamayı son aktif olan kişiye göre yapar
                                .AsQueryable();
            
            if (userParams.Followers)//takip edenler
            {
                var result= await GetFollows(userParams.UserId,false);
                users=users.Where(u =>result.Contains(u.Id));
            }
            
            if (userParams.Followings)//takip edilenler{
            {
                var result= await GetFollows(userParams.UserId,true);
                users=users.Where(u =>result.Contains(u.Id));
                
            }
            if (!string.IsNullOrEmpty(userParams.Gender))
            {
                users =users.Where(i =>i.Gender==userParams.Gender);
            }
            if (userParams.minAge!=18 || userParams.maxAge!=100)
            {
                var today = DateTime.Now;
                var min = today.AddYears(-(userParams.maxAge+1));
                var max = today.AddYears(-userParams.minAge);
                users= users.Where(i =>i.DateOfBirth>=min && i.DateOfBirth<=max);
            }
            if (!string.IsNullOrEmpty(userParams.City))
            {
              users= users.Where(i => i.City.ToLower()==userParams.City.ToLower());
            }  
            if (!string.IsNullOrEmpty(userParams.Country))
            {
              users= users.Where(i => i.Country.ToLower()==userParams.Country.ToLower());
            }    

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                if (userParams.OrderBy=="age")
                {
                    users = users.OrderBy(i => i.DateOfBirth);
                }
                else if (userParams.OrderBy=="created")
                {
                    users =users.OrderByDescending(i =>i.Created);
                }
            }
            
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