using FlottaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlottaApi.DTOS;

namespace FlottaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private FlottaContext _context;
        public LoginController(FlottaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.LoginName == loginDTO.LoginName);
                if (user == null)
                {
                    return NotFound("Missing user. ");
                }
                string phcString = user.Hash;
                var (isValid, parameters) = Argon2Sharp.Argon2PhcFormat.VerifyPhcString(LoginDTO.Password, phcString);
                if (isValid)
                {
                    return Ok("taken");
                }
                return BadRequest("Wrong Password.");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. " + ex.Message);
            }
        }
    }
}
