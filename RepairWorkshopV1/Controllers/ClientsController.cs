using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairWorkshopV1.Models;

namespace RepairWorkshopV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly RepairWorkshopContext _context;

        public ClientsController(RepairWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clients>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/{username/email}
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<Clients>>> GetClientId(string email)
        {
            var clients = await _context.Clients.Where(x => x.Email == email).ToListAsync();

            if (clients == null)
            {
                return NotFound();
            }

            return clients;
        }



        // GET: api/Clients/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Clients>> GetClients(decimal id)
        {
            var clients = await _context.Clients.FindAsync(id);

            if (clients == null)
            {
                return NotFound();
            }

            return clients;
        }
        */

        // PUT: api/Clients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClients(decimal id, Clients clients)
        {
            if (id != clients.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(clients).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientsExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Clients>> PostClients(Clients clients)
        {
            _context.Clients.Add(clients);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClients", new { id = clients.ClientId }, clients);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Clients>> DeleteClients(decimal id)
        {
            var clients = await _context.Clients.FindAsync(id);
            if (clients == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(clients);
            await _context.SaveChangesAsync();

            return clients;
        }

        private bool ClientsExists(decimal id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
