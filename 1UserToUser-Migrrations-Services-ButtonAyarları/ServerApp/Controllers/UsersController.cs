using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ServerApp.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using ServerApp.DTO;
using AutoMapper;
using System.Security.Claims;
using ServerApp.Helpers;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [ServiceFilter(typeof(LastActiveActionFilter))]////uygulamaya girildiği anda las active alanı güncellenmiyor o alanın otomatik güncellenmesi için 
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISocialRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(ISocialRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetUsers();
            var result = _mapper.Map<IEnumerable<UserForListDTO>>(users);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.GetUser(id);
            var result =_mapper.Map<UserForDetailsDTO>(user);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UptadeUser(int id, UserForUpdateDTO model)
        {
            if(id!= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))//kullanıcı sadece kendi bilgilerini güncelleyebilsin diğerlerine müdehale edemesin diye
             return BadRequest("not valid request");

            if(!ModelState.IsValid)
             return BadRequest(ModelState);

            var user =await _repository.GetUser(id);
            _mapper.Map(model,user);

            if(await _repository.SaveChanges())
             return Ok();

            throw new System.Exception("güncelleme sırasında hata oluştu");
            
        }
        //localhost:5001/api/users/follow/3..
        [HttpPost("{followerUserId}/follow/{userId}")]
        public async Task<IActionResult> FollowerUser(int followerUserId, int userId)
        {
            if(followerUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))//token kiminse işlem yapar
             return Unauthorized();

            if(followerUserId==userId)
             return BadRequest("Kendinizi Takip Edemezsiniz");
            
            var IsAlreadyFollowed = await _repository.IsAlreadyFollowed(followerUserId,userId);

            if(IsAlreadyFollowed)
             return BadRequest("Zaten Kullanıcıyı Takip Ediyorsunuz");

            if(await _repository.GetUser(userId)==null)
             return NotFound();
            
            var follow = new UserToUser(){
                UserId =userId,
                FollowerId = followerUserId
            };
            
            _repository.Add<UserToUser>(follow);
            
            if(await _repository.SaveChanges())
             return Ok();
            
            return BadRequest("Hata Oluştu");
                        
        }
    }
}