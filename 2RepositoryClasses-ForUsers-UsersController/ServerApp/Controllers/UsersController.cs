using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ServerApp.Data;
using System.Threading.Tasks;

namespace ServerApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISocialRepository _repository;

        public UsersController(ISocialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.GetUser(id);
            return Ok(user);
        }
    }
}