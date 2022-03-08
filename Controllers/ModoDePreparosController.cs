using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using blog_receitas_api.Models;

namespace blog_receitas_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModoDePreparosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModoDePreparosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ModoDePreparos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModoDePreparo>>> GetModoDePreparos()
        {
            return await _context.ModoDePreparos.ToListAsync();
        }

        // GET: api/ModoDePreparos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModoDePreparo>> GetModoDePreparo(int id)
        {
            var modoDePreparo = await _context.ModoDePreparos.FindAsync(id);

            if (modoDePreparo == null)
            {
                return NotFound();
            }

            return modoDePreparo;
        }

        // PUT: api/ModoDePreparos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModoDePreparo(int id, ModoDePreparo modoDePreparo)
        {
            if (id != modoDePreparo.Id)
            {
                return BadRequest();
            }

            _context.Entry(modoDePreparo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModoDePreparoExists(id))
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

        // POST: api/ModoDePreparos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModoDePreparo>> PostModoDePreparo(ModoDePreparo modoDePreparo)
        {
            _context.ModoDePreparos.Add(modoDePreparo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModoDePreparo", new { id = modoDePreparo.Id }, modoDePreparo);
        }

        // DELETE: api/ModoDePreparos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModoDePreparo(int id)
        {
            var modoDePreparo = await _context.ModoDePreparos.FindAsync(id);
            if (modoDePreparo == null)
            {
                return NotFound();
            }

            _context.ModoDePreparos.Remove(modoDePreparo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModoDePreparoExists(int id)
        {
            return _context.ModoDePreparos.Any(e => e.Id == id);
        }
    }
}
