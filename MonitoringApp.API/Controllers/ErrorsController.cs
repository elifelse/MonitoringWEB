using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoringApp.Persistence.Contexts;
using MonitoringApp.Persistence.Entities;

namespace MonitoringApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        private readonly MonitoringDbContext _context;

        public ErrorsController(MonitoringDbContext context)
        {
            _context = context;
        }

        // GET: api/Errors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Errors>>> GetErrors()
        {
            return await _context.Errors.ToListAsync();
        }

        // GET: api/Errors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Errors>> GetError(int id)
        {
            var error = await _context.Errors.FindAsync(id);

            if (error == null)
            {
                return NotFound();
            }

            return error;
        }

        // POST: api/Errors
        [HttpPost]
        public async Task<ActionResult<Errors>> PostError(Errors error)
        {
            _context.Errors.Add(error);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetError), new { id = error.Id }, error);
        }

        // DELETE: api/Errors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteError(int id)
        {
            var error = await _context.Errors.FindAsync(id);
            if (error == null)
            {
                return NotFound();
            }

            _context.Errors.Remove(error);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
