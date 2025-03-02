using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoringApp.Models;

namespace MonitoringApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly MonitoringDbContext _context;

        public ApplicationsController(MonitoringDbContext context)
        {
            _context = context;
        }

        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Applications>>> GetApplications()
        {
            return await _context.Applications.ToListAsync();
        }

        // POST: api/Applications
        [HttpPost]
        public async Task<ActionResult<Applications>> PostApplication(Applications application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetApplications), new { id = application.Id }, application);
        }

        // GET: api/Applications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Applications>> GetApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return application;
        }

        // PUT: api/Applications/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Applications application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Applications/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Yardımcı Metod: Verilen ID ile bir uygulama olup olmadığını kontrol eder
        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }

    }
}
