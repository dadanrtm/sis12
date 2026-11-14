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
    public class MsStudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsStudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MsStudents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MsStudent>>> GetMsStudents()
        {
            return await _context.MsStudents.ToListAsync();
        }

        // GET: api/MsStudents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MsStudent>> GetMsStudent(int id)
        {
            var msStudent = await _context.MsStudents.FindAsync(id);

            if (msStudent == null)
            {
                return NotFound();
            }

            return msStudent;
        }

        // PUT: api/MsStudents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMsStudent(int id, MsStudent msStudent)
        {
            if (id != msStudent.Id)
            {
                return BadRequest();
            }

            _context.Entry(msStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MsStudentExists(id))
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

        // POST: api/MsStudents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MsStudent>> PostMsStudent(MsStudent msStudent)
        {
            _context.MsStudents.Add(msStudent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMsStudent", new { id = msStudent.Id }, msStudent);
        }

        // DELETE: api/MsStudents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMsStudent(int id)
        {
            var msStudent = await _context.MsStudents.FindAsync(id);
            if (msStudent == null)
            {
                return NotFound();
            }

            _context.MsStudents.Remove(msStudent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MsStudentExists(int id)
        {
            return _context.MsStudents.Any(e => e.Id == id);
        }
    }
}
