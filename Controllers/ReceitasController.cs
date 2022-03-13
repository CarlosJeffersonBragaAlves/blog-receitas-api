using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using blog_receitas_api.Models;
using X.PagedList;
using blog_receitas_api.Models.Shared;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace blog_receitas_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment Environment;


        public ReceitasController(AppDbContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }




        // GET: api/Receitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receita>>> GetReceitas()
        {
            return await _context.Receitas.Include(r => r.Ingredientes)
                                          .Include(r => r.ModoDePreparos)
                                          .Include(r => r.Tipo)
                                          .ToListAsync();
        }


        [HttpGet("relacionado/{id}/{tipo}")]
        public async Task<ActionResult<IEnumerable<Receita>>> GetReceitas(int id,int tipo)
        {
           var itens = RandomizeGenericList(
                    await _context.Receitas.Include(r => r.Ingredientes)
                                          .Include(r => r.ModoDePreparos)
                                          .Include(r => r.Tipo)
                                          .Where(r => r.TipoId == tipo && r.Id != id)
                                          .ToListAsync()
                );

            return itens.GetRange(0, 6);
        }


        [HttpGet("slide")]
        public async Task<ActionResult<IEnumerable<Receita>>> GetReceitasSlide()
        {
            var itens = await _context.Receitas.ToListAsync();

            var receitas = itens.Where(r => r.destaque == 1).ToList();

            if(receitas.Count() < 6)
            {
                receitas.AddRange(itens.Where(r => r.destaque == 2)
                                       .OrderByDescending(r => r.Id)
                                       .ToList()
                                       .GetRange(0, (6 - receitas.Count())));
                
            }

            return receitas;
        }



        [HttpPost("paged")]
        public async Task<ActionResult> PostReceitas(OptionsFilter options)
        {
            var itens = _context.Receitas.Include(r => r.Ingredientes)
                                         .Include(r => r.ModoDePreparos)
                                         .Include(r => r.Tipo)
                                         .Where(r => r.Id > 0);

            if(options.Tipo > 0)
            {
                itens = itens.Where(r => r.TipoId == options.Tipo);
            }

            if(options.Status != 2)
            {
                if(options.Status == 1)
                {
                    itens = itens.Where(r => r.StatusId == 1);
                }
            }
            else
            {
                itens = itens.Where(r => r.StatusId == 2);
            }

            if(options.Destaque == 1)
            {
                itens = itens.Where(r => r.destaque == 1);
            }

            if(options.Filter.Length > 0)
            {
                options.Filter = options.Filter.Trim().ToUpper();
                itens = itens.Where(r =>
                    r.Title.ToUpper().Contains(options.Filter) ||
                    r.Subtitle.ToUpper().Contains(options.Filter) ||
                    r.Tipo.Desc.ToUpper().Contains(options.Filter)
                    );
            }


            var receitas = await itens.OrderByDescending(i => i.DataCriacao).ToPagedListAsync(options.Page, options.Size);




          
            return Ok(new
            {
                itens = receitas,
                metadata = receitas.GetMetaData()
            });
        }

        // GET: api/Receitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> GetReceita(int id)
        {
            var receita = await _context.Receitas.Include(r => r.Ingredientes)
                                                 .Include(r => r.ModoDePreparos)
                                                 .Include(r => r.Tipo)
                                                 .FirstOrDefaultAsync(r => r.Id == id);

            if (receita == null)
            {
                return NotFound();
            }

            return receita;
        }

        // PUT: api/Receitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceita(int id, Receita receita)
        {
            if (id != receita.Id)
            {
                return BadRequest();
            }
            if(receita.DataPublicacao != null)
                receita.DataPublicacao = receita.StatusId == 2 ? DateTime.Now : null;

            _context.Entry(receita).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceitaExists(id))
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

        // POST: api/Receitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receita>> PostReceita(Receita receita)
        {
            receita.DataCriacao = DateTime.Now;
            receita.DataPublicacao = receita.StatusId == 2 ? DateTime.Now : null;

            _context.Receitas.Add(receita);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceita", new { id = receita.Id }, receita);
        }

        // DELETE: api/Receitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceita(int id)
        {
            var receita = await _context.Receitas.FindAsync(id);
            if (receita == null)
            {
                return NotFound();
            }

            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceitaExists(int id)
        {
            return _context.Receitas.Any(e => e.Id == id);
        }

        public static List<T> RandomizeGenericList<T>(IList<T> originalList)
        {
            List<T> randomList = new List<T>();
            Random random = new Random();
            T value = default(T);

            //now loop through all the values in the list
            while (originalList.Count() > 0)
            {
                //pick a random item from th original list
                var nextIndex = random.Next(0, originalList.Count());
                //get the value for that random index
                value = originalList[nextIndex];
                //add item to the new randomized list
                randomList.Add(value);
                //remove value from original list (prevents
                //getting duplicates
                originalList.RemoveAt(nextIndex);
            }

            //return the randomized list
            return randomList;
        }
    }
}
