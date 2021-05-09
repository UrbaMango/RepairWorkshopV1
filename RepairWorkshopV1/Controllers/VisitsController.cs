using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairWorkshopV1.Models;

namespace RepairWorkshopV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly RepairWorkshopContext _context;

        public VisitsController(RepairWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Visits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visits>>> GetVisits()
        {
            return await _context.Visits.ToListAsync();
        }

        // GET: api/Visits/5
        /*
        [HttpGet("{id}")]
        public async Task<ActionResult<Visits>> GetVisits(decimal id)
        {
            var visits = await _context.Visits.FindAsync(id);

            if (visits == null)
            {
                return NotFound();
            }

            return visits;
        }
        */

        [HttpGet("{clientId}")]
        public async Task<ActionResult<IEnumerable<Visits>>> GetVisits(decimal clientId)
        {
            var visits = await _context.Visits.Where(x => x.ClientId == clientId).ToListAsync();

            if (visits == null)
            {
                return NotFound();
            }

            return visits;
        }

        // PUT: api/Visits/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisits(decimal id, Visits visits)
        {
            if (id != visits.VisitId)
            {
                return BadRequest();
            }

            _context.Entry(visits).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitsExists(id))
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

        // POST: api/Visits
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Visits>> PostVisits(Visits visits)
        {
            _context.Visits.Add(visits);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisits", new { id = visits.VisitId }, visits);
        }

        // DELETE: api/Visits/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Visits>> DeleteVisits(decimal id)
        {
            var visits = await _context.Visits.FindAsync(id);
            if (visits == null)
            {
                return NotFound();
            }

            _context.Visits.Remove(visits);
            await _context.SaveChangesAsync();

            return visits;
        }

        private bool VisitsExists(decimal id)
        {
            return _context.Visits.Any(e => e.VisitId == id);
        }
    }
}
