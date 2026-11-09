using FlottaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlottaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private FlottaContext _context;
        [HttpGet("GetAll")]
        public IActionResult GetAllSofor()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                User users = _context.Users.FirstOrDefault(o => o.Id == id);
                if (users == null)
                {
                    _context.Users.Remove(users);
                    _context.SaveChanges();
                    return Ok("Sikeres törlés");
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("PostUser")]
        public IActionResult PostUser(User users)
        {
            try
            {
                _context.Users.Add(users);
                _context.SaveChanges();
                return Ok("Sikeres hozzáadás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("Change")]
        public IActionResult Put(User users)
        {
            try
            {
                if (users == null)
                {
                    return BadRequest("Missing data");
                }
                Sofor so = _context.Users.FirstOrDefault(s => s.Id == users.id);
                if (so == null)
                {
                    return NotFound("Missing driver");
                }
                _context.ChangeTracker.Clear();
                _context.Sofors.Update(so);
                _context.SaveChanges();
                return Ok("The driverdata modified");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
