using System;
using System.IdentityModel.Tokens.Jwt; //JwtSecurityTokenHandler() için 
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;//async metodların Task kısımları 
using Microsoft.AspNetCore.Identity;//UserManager,SignInManager
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;//IConfiguration için 
using Microsoft.IdentityModel.Tokens;//SecurityTokenDescriptor için 
using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]// localhos:5000/api/user 
    public class AuthController:ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configurations;

        public AuthController(UserManager<User> userManager,SignInManager<User> signInManager, IConfiguration configurations)
        {
            _userManager=userManager;
            _signInManager = signInManager;
            _configurations = configurations;
        }

        [HttpPost("register")]// localhos:5000/api/user/register
        public async Task<IActionResult> Register(UserForRegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User{
                UserName = model.UserName,
                Email= model.Email,
                Name = model.Name,
                Gender = model.Gender,
                Created = DateTime.Now,
                LastActive = DateTime.Now
            };
            var result =await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
            {
                return StatusCode(201);
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("login")]// localhos:5000/api/user/login
        public async Task<IActionResult> Login(UserForLoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            //throw new Exception("Interval Exception");

            var user  = await _userManager.FindByNameAsync(model.UserName);

            if(user == null)
                return BadRequest(new{message="username is incorrect"});
            
            var result =await _signInManager.CheckPasswordSignInAsync(user,model.Password,false);//startup da  hatalı giriş yapıldığın da hesap kitleniyor olayını true yapmıştık burada ezerek false yaptık

            if (result.Succeeded)
            {
                //login
                return Ok( new {
                    token=GenerateJwtToken(user)
                });
            }
            return Unauthorized();

        }

        private object GenerateJwtToken(User user) //token oluşturma
        {
            var tokenHandler =new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configurations.GetSection("AppSettings:Secret").Value);//appsetting.jsonda oluşturduğumuz secret bilgisinin değerini aldık

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{//hangi bilgileri kullanacağımızı belirledik 
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                }),
                //token bilgisinin ne kadar süreceğini, hangi key ile hangi algoritmayı kullanacağını belirttik 
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}