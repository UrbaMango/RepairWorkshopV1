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
    public class VehiclesController : ControllerBase
    {
        private readonly RepairWorkshopContext _context;

        public VehiclesController(RepairWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicles>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // GET: api/Vehicles/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Vehicles>> GetVehicles(decimal id)
        {
            var vehicles = await _context.Vehicles.FindAsync(id);

            if (vehicles == null)
            {
                return NotFound();
            }

            return vehicles;
        }*/

        // GET: api/Vehicles/{clientId}
        [HttpGet("{clientId}")]
        public async Task<ActionResult<IEnumerable<Vehicles>>> GetClientVehicles(decimal clientId)
        {
            var vehicles = await _context.Vehicles.Where(x => x.ClientId == clientId).ToListAsync();

            if (vehicles == null)
            {
                return NotFound();
            }

            return vehicles;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicles(decimal id, Vehicles vehicles)
        {
            if (id != vehicles.VehicleId)
            {
                return BadRequest();
            }

            _context.Entry(vehicles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiclesExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        
        [HttpPost]
        public async Task<ActionResult<Vehicles>> PostVehicles(Vehicles vehicles)
        {
            _context.Vehicles.Add(vehicles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicles", new { id = vehicles.VehicleId }, vehicles);
        }

        [HttpPost]
        [Route("multiple")]
        public async Task<ActionResult<IEnumerable<Vehicles>>> PostVehicles(Vehicles[] vehicles)
        {
            foreach(Vehicles vehicle in vehicles)
            {
                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }



        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicles>> DeleteVehicles(decimal id)
        {
            var vehicles = await _context.Vehicles.FindAsync(id);
            if (vehicles == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicles);
            await _context.SaveChangesAsync();

            return vehicles;
        }

        private bool VehiclesExists(decimal id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
    }
}
