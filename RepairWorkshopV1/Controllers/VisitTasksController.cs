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
    public class VisitTasksController : ControllerBase
    {
        private readonly RepairWorkshopContext _context;

        public VisitTasksController(RepairWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/VisitTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitTasks>>> GetVisitTasks()
        {
            return await _context.VisitTasks.ToListAsync();
        }

        // GET: api/VisitTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitTasks>> GetVisitTasks(decimal id)
        {
            var visitTasks = await _context.VisitTasks.FindAsync(id);

            if (visitTasks == null)
            {
                return NotFound();
            }

            return visitTasks;
        }

        // PUT: api/VisitTasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitTasks(decimal id, VisitTasks visitTasks)
        {
            if (id != visitTasks.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(visitTasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitTasksExists(id))
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

        // POST: api/VisitTasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<VisitTasks>> PostVisitTasks(VisitTasks visitTasks)
        {
            _context.VisitTasks.Add(visitTasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisitTasks", new { id = visitTasks.TaskId }, visitTasks);
        }

        // DELETE: api/VisitTasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VisitTasks>> DeleteVisitTasks(decimal id)
        {
            var visitTasks = await _context.VisitTasks.FindAsync(id);
            if (visitTasks == null)
            {
                return NotFound();
            }

            _context.VisitTasks.Remove(visitTasks);
            await _context.SaveChangesAsync();

            return visitTasks;
        }

        private bool VisitTasksExists(decimal id)
        {
            return _context.VisitTasks.Any(e => e.TaskId == id);
        }
    }
}
