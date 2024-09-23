using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public DriversController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: /drivers
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            try
            {
                var drivers = await _context.Drivers.ToListAsync();
                return Ok(drivers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: /drivers
        [HttpPost]
        public async Task<IActionResult> AddDriver([FromBody] Driver driver)
        {
            if (driver == null)
            {
                return BadRequest("Driver object is null");
            }

            try
            {
                await _context.Drivers.AddAsync(driver);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAllDrivers), new { id = driver.DriverNb }, driver);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
