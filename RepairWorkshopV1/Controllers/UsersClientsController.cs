using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairWorkshopV1.Models;
using Microsoft.AspNetCore.Authorization;
using RepairWorkshopV1.Helpers;
using System.Security.Cryptography;
using RepairWorkshopV1.Interfaces;
using System.Security.Claims;


namespace RepairWorkshopV1.Controllers
{
    [Authorize(Policy = "OnlyNonBlockedEmployee")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersClientsController : ControllerBase
    {
        private readonly RepairWorkshopContext _context;

        public UsersClientsController(RepairWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/UsersClients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UsersClients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(decimal id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/UsersClients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(decimal id, Users users)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            users.PasswordSalt = Convert.ToBase64String(salt);
            users.Password = HashingHelper.HashUsingPbkdf2(users.Password, users.PasswordSalt);

            if (id != users.UserId)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/UsersClients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            users.Active = true;
            users.PasswordSalt = Convert.ToBase64String(salt);
            users.Password = HashingHelper.HashUsingPbkdf2(users.Password, users.PasswordSalt);
            

            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.UserId }, users);
        }

        // DELETE: api/UsersClients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(decimal id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(decimal id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
