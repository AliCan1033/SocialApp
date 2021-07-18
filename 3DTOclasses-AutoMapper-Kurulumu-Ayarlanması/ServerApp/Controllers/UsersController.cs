using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ServerApp.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using ServerApp.DTO;
using AutoMapper;

namespace ServerApp.Controllers
{
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
    }
}