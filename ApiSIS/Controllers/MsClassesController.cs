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
    public class MsClassesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsClassesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MsClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MsClass>>> GetMsClasses()
        {

            return await _context.MsClasses
        .Include(c => c.HomeroomTeacherId)  // JOIN dengan Teacher
        .ToListAsync();
           
        }

        // GET: api/MsClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MsClass>> GetMsClass(int id)
        {
            var msClass = await _context.MsClasses.FindAsync(id);

            if (msClass == null)
            {
                return NotFound();
            }

            return msClass;
        }

        // PUT: api/MsClasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMsClass(int id, MsClass msClass)
        {
            if (id != msClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(msClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MsClassExists(id))
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

        // POST: api/MsClasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MsClass>> PostMsClass(MsClass msClass)
        {
            _context.MsClasses.Add(msClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMsClass", new { id = msClass.Id }, msClass);
        }

        // DELETE: api/MsClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMsClass(int id)
        {
            var msClass = await _context.MsClasses.FindAsync(id);
            if (msClass == null)
            {
                return NotFound();
            }

            _context.MsClasses.Remove(msClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MsClassExists(int id)
        {
            return _context.MsClasses.Any(e => e.Id == id);
        }
    }
}
