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
    public class RoleMenuMappingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoleMenuMappingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RoleMenuMappings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleMenuMapping>>> GetRoleMenuMappings()
        {
            return await _context.RoleMenuMappings.ToListAsync();
        }

        // GET: api/RoleMenuMappings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleMenuMapping>> GetRoleMenuMapping(int id)
        {
            var roleMenuMapping = await _context.RoleMenuMappings.FindAsync(id);

            if (roleMenuMapping == null)
            {
                return NotFound();
            }

            return roleMenuMapping;
        }

        // PUT: api/RoleMenuMappings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleMenuMapping(int id, RoleMenuMapping roleMenuMapping)
        {
            if (id != roleMenuMapping.Id)
            {
                return BadRequest();
            }

            _context.Entry(roleMenuMapping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleMenuMappingExists(id))
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

        // POST: api/RoleMenuMappings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleMenuMapping>> PostRoleMenuMapping(RoleMenuMapping roleMenuMapping)
        {
            _context.RoleMenuMappings.Add(roleMenuMapping);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleMenuMapping", new { id = roleMenuMapping.Id }, roleMenuMapping);
        }

        // DELETE: api/RoleMenuMappings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleMenuMapping(int id)
        {
            var roleMenuMapping = await _context.RoleMenuMappings.FindAsync(id);
            if (roleMenuMapping == null)
            {
                return NotFound();
            }

            _context.RoleMenuMappings.Remove(roleMenuMapping);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleMenuMappingExists(int id)
        {
            return _context.RoleMenuMappings.Any(e => e.Id == id);
        }
    }
}
