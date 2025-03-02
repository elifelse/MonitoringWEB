using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoringApp.Models;

namespace MonitoringApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceHealthLogsController : ControllerBase
    {
        private readonly MonitoringDbContext _context;

        public ServiceHealthLogsController(MonitoringDbContext context)
        {
            _context = context;
        }

        // GET: api/ServiceHealthLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicehealthlogs>>> GetServiceHealthLogs()
        {
            return await _context.Servicehealthlogs
                .Include(log => log.Application) // Application verisini de getir
                .ToListAsync();
        }


        // POST: api/ServiceHealthLogs
        [HttpPost]
        public async Task<ActionResult<Servicehealthlogs>> PostServiceHealthLog(Servicehealthlogs log)
        {
            _context.Servicehealthlogs.Add(log);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetServiceHealthLogs), new { id = log.Id }, log);
        }

        // GET: api/ServiceHealthLogs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Servicehealthlogs>> GetServiceHealthLog(int id)
        {
            var log = await _context.Servicehealthlogs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return log;
        }

        // PUT: api/ServiceHealthLogs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceHealthLog(int id, Servicehealthlogs log)
        {
            if (id != log.Id)
            {
                return BadRequest();
            }

            _context.Entry(log).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceHealthLogExists(id))
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

        // DELETE: api/ServiceHealthLogs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceHealthLog(int id)
        {
            var log = await _context.Servicehealthlogs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            _context.Servicehealthlogs.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Yardımcı Metod: Verilen ID ile bir sağlık logu olup olmadığını kontrol eder
        private bool ServiceHealthLogExists(int id)
        {
            return _context.Servicehealthlogs.Any(e => e.Id == id);
        }

    }
}
