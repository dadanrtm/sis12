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
    public class MsTeachersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsTeachersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MsTeachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetMsClasses()
        {
            // Menggunakan LINQ Join
            var classesWithTeachers = await (
                from c in _context.MsClasses
                join t in _context.MsTeachers on c.Id equals t.Id into teacherGroup
                from t in teacherGroup.DefaultIfEmpty() // Left Join
                select new
                {
                    c.Id,
                    c.ClassName,
                    TeacherId = c.Id,
                    teacherPhone = t.Phone,
                    TeacherName = t != null ? t.TeacherName : "Belum ada guru",
                    TeacherNip = t != null ? t.Nip : null,
                    TeacherEmail = t != null ? t.Email : null
                }
            ).ToListAsync();

            return Ok(classesWithTeachers);
        }

        // GET: api/MsTeachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MsTeacher>> GetMsTeacher(int id)
        {
            var msTeacher = await _context.MsTeachers.FindAsync(id);

            if (msTeacher == null)
            {
                return NotFound();
            }

            return msTeacher;
        }

        // PUT: api/MsTeachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMsTeacher(int id, MsTeacher msTeacher)
        {
            if (id != msTeacher.Id)
            {
                return BadRequest();
            }

            _context.Entry(msTeacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MsTeacherExists(id))
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

        // POST: api/MsTeachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MsTeacher>> PostMsTeacher(MsTeacher msTeacher)
        {
            _context.MsTeachers.Add(msTeacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMsTeacher", new { id = msTeacher.Id }, msTeacher);
        }

        // DELETE: api/MsTeachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMsTeacher(int id)
        {
            var msTeacher = await _context.MsTeachers.FindAsync(id);
            if (msTeacher == null)
            {
                return NotFound();
            }

            _context.MsTeachers.Remove(msTeacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MsTeacherExists(int id)
        {
            return _context.MsTeachers.Any(e => e.Id == id);
        }
    }
}
