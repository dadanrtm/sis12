using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSIS.Data;
using ApiSIS.Entities;

namespace ApiSIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MsUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MsUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MsUser>>> GetMsUsers()
        {
            return await _context.MsUsers.ToListAsync();
        }

        // GET: api/MsUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MsUser>> GetMsUser(int id)
        {
            var msUser = await _context.MsUsers.FindAsync(id);

            if (msUser == null)
            {
                return NotFound();
            }

            return msUser;
        }

        // PUT: api/MsUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMsUser(int id, MsUser msUser)
        {
            if (id != msUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(msUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MsUserExists(id))
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

        // POST: api/MsUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MsUser>> PostMsUser(MsUser msUser)
        {
            _context.MsUsers.Add(msUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMsUser", new { id = msUser.Id }, msUser);
        }

        // DELETE: api/MsUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMsUser(int id)
        {
            var msUser = await _context.MsUsers.FindAsync(id);
            if (msUser == null)
            {
                return NotFound();
            }

            _context.MsUsers.Remove(msUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MsUserExists(int id)
        {
            return _context.MsUsers.Any(e => e.Id == id);
        }
    }
}
