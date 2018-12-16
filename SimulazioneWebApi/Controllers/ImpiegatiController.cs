using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulazioneWebApi.Model;
using SimulazioneWebApi.Model.DTOs;

namespace SimulazioneWebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("impiegati")]
    [ApiController]
    public class ImpiegatiController : ControllerBase
    {
        private readonly DipartimentiContext _context;

        public ImpiegatiController(DipartimentiContext context)
        {
            _context = context;
        }

        // GET: api/Impiegati
        [HttpGet]
        public IEnumerable<ImpiegatoDTO> GetImpiegati()
        {
            var impiegati = _context.Impiegati.ToList();
            List<ImpiegatoDTO> impDTO = new List<ImpiegatoDTO>();
            foreach(var imp in impiegati)
            {
                ImpiegatoDTO newImp = new ImpiegatoDTO(imp);
                if(imp.IdDipartimento > 0)
                {
                    newImp.NomeDipartimento = _context.Dipartimenti.FirstOrDefault(d => d.Id == imp.IdDipartimento).Nome;
                }       
                impDTO.Add(newImp);
            }
            return impDTO;
        }

        // GET: api/Impiegati/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImpiegati([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var impiegati = await _context.Impiegati.FindAsync(id);

            if (impiegati == null)
            {
                return NotFound();
            }

            return Ok(impiegati);
        }

        // PUT: api/Impiegati/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImpiegati([FromRoute] int id, [FromBody] Impiegati impiegati)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != impiegati.Id)
            {
                return BadRequest();
            }

            _context.Entry(impiegati).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImpiegatiExists(id))
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

        // POST: api/Impiegati
        [HttpPost]
        public async Task<IActionResult> PostImpiegati([FromBody] Impiegati impiegati)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Impiegati.Add(impiegati);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImpiegati", new { id = impiegati.Id }, impiegati);
        }

        // DELETE: api/Impiegati/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImpiegati([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var impiegati = await _context.Impiegati.FindAsync(id);
            if (impiegati == null)
            {
                return NotFound();
            }

            _context.Impiegati.Remove(impiegati);
            await _context.SaveChangesAsync();

            return Ok(impiegati);
        }

        private bool ImpiegatiExists(int id)
        {
            return _context.Impiegati.Any(e => e.Id == id);
        }
    }
}