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
    public class UsersEmpsController : ControllerBase
    {
        private readonly RepairWorkshopContext _context;

        public UsersEmpsController(RepairWorkshopContext context)
        {
            _context = context;
        }

        // GET: api/UsersEmps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersEmp>>> GetUsersEmp()
        {
            return await _context.UsersEmp.ToListAsync();
        }

        // GET: api/UsersEmps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersEmp>> GetUsersEmp(decimal id)
        {
            var usersEmp = await _context.UsersEmp.FindAsync(id);

            if (usersEmp == null)
            {
                return NotFound();
            }

            return usersEmp;
        }

        // PUT: api/UsersEmps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersEmp(decimal id, UsersEmp usersEmp)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            usersEmp.PasswordSalt = Convert.ToBase64String(salt);
            usersEmp.Password = HashingHelper.HashUsingPbkdf2(usersEmp.Password, usersEmp.PasswordSalt);

            if (id != usersEmp.UserId)
            {
                return BadRequest();
            }

            _context.Entry(usersEmp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersEmpExists(id))
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

        // POST: api/UsersEmps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UsersEmp>> PostUsersEmp(UsersEmp usersEmp)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            usersEmp.Active = true;
            usersEmp.PasswordSalt = Convert.ToBase64String(salt);
            usersEmp.Password = HashingHelper.HashUsingPbkdf2(usersEmp.Password, usersEmp.PasswordSalt);

            _context.UsersEmp.Add(usersEmp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsersEmp", new { id = usersEmp.UserId }, usersEmp);
        }

        // DELETE: api/UsersEmps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsersEmp>> DeleteUsersEmp(decimal id)
        {
            var usersEmp = await _context.UsersEmp.FindAsync(id);
            if (usersEmp == null)
            {
                return NotFound();
            }

            _context.UsersEmp.Remove(usersEmp);
            await _context.SaveChangesAsync();

            return usersEmp;
        }

        private bool UsersEmpExists(decimal id)
        {
            return _context.UsersEmp.Any(e => e.UserId == id);
        }
    }
}
