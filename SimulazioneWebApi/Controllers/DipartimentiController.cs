using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulazioneWebApi.Model;

namespace SimulazioneWebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("dipartimenti")]
    [ApiController]
    public class DipartimentiController : ControllerBase
    {
        private readonly DipartimentiContext _context;

        public DipartimentiController(DipartimentiContext context)
        {
            _context = context;
        }

        // GET: api/Dipartimenti
        [HttpGet]
        public IEnumerable<Dipartimenti> GetDipartimenti()
        {
            return _context.Dipartimenti;
        }

        // GET: api/Dipartimenti/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDipartimenti([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dipartimenti = await _context.Dipartimenti.FindAsync(id);

            if (dipartimenti == null)
            {
                return NotFound();
            }

            return Ok(dipartimenti);
        }

        // PUT: api/Dipartimenti/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDipartimenti([FromRoute] int id, [FromBody] Dipartimenti dipartimenti)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dipartimenti.Id)
            {
                return BadRequest();
            }

            _context.Entry(dipartimenti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DipartimentiExists(id))
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

        // POST: api/Dipartimenti
        [HttpPost]
        public async Task<IActionResult> PostDipartimenti([FromBody] Dipartimenti dipartimenti)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dipartimenti.Add(dipartimenti);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDipartimenti", new { id = dipartimenti.Id }, dipartimenti);
        }

        // DELETE: api/Dipartimenti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDipartimenti([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dipartimenti = await _context.Dipartimenti.FindAsync(id);
            if (dipartimenti == null)
            {
                return NotFound();
            }

            _context.Dipartimenti.Remove(dipartimenti);
            await _context.SaveChangesAsync();

            return Ok(dipartimenti);
        }

        private bool DipartimentiExists(int id)
        {
            return _context.Dipartimenti.Any(e => e.Id == id);
        }
    }
}