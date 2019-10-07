using System.Threading.Tasks;
using DatingApp.api.Deto;
using DatingApp.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }
        [HttpPost("register")]
        public async Task<IActionResult> register(registerUser user){
         // [FromBody]
           // if (!ModelState.IsValid)
            //{
              //  return BadRequest(ModelState);
            //}
            user.name=user.name.ToLower();
            if (await _repo.userExists(user.name))
            {
                return BadRequest("UserName Is Exsist");
            }
            var createUser =new User{
                userName=user.name
            };
            var createdName=await _repo.register(createUser,user.password);
            return StatusCode(201);
        }
    }
}