using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlottaApi.Models;

namespace FlottaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SoforController : ControllerBase
    {
        private FlottaContext _context;
        [HttpGet("GetAll")]
        public IActionResult GetAllSofor()
        {
            var soforok = _context.Sofors.ToList();
            return Ok(soforok);
        }

        [HttpDelete("DeleteSofor/{id}")]
        public IActionResult DeleteSofor(int id)
        {
            try
            { Sofor soforok = _context.Sofors.FirstOrDefault(o => o.Id == id);
                if (soforok == null)
                {
                    _context.Sofors Remove(soforok);
                    _context.SaveChanges();
                    return Ok("Sikeres törlés");
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                StatusCode(500, ex.Message);
            }
        }
        [HttpPost("PostSofor")]
        public IActionResult PostSofor(Sofor sofor)
        {
            try
            {
                _context.Sofors.Add(sofor);
                _context.SaveChanges();
                return Ok("Sikeres hozzáadás");
            }
            catch (Exception ex)
            {
                StatusCode(500, ex.Message);
            }
        }
        [HttpPut("Change")]
        public IActionResult Put(Sofor sofor)
        {
            try
            {
                if (sofor == null)
                {
                    return BadRequest("Missing data");
                }
                Sofor so = _context.Sofors.FirstOrDefault(s => s.Id == sofor.Id);
                if (so == null)
                {
                    return NotFound("Missing driver");
                }
                _context.ChangeTracker.Clear();
                _context.Sofors.Update(so);
                _context.SaveChanges();
                return Ok("The driverdata modified");


            }
        }
    }
}