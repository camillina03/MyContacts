using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyContacts.Connections;
using MyContacts.Entities;

namespace MyContacts.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    // Controller per gestire le richieste relative ai contatti
    [Route("api/[controller]")]
    public class ContattiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContattiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Contatti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contatto>>> GetContatti()
        {
            return await _context.Contatti.ToListAsync();
        }

        // GET: api/Contatti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contatto>> GetContatto(int id)
        {
            var contatto = await _context.Contatti.FindAsync(id);

            if (contatto == null)
            {
                return NotFound();
            }

            return contatto;
        }

        // POST: api/Contatti
        [HttpPost]
        public async Task<ActionResult<Contatto>> PostContatto(Contatto contatto)
        {
            _context.Contatti.Add(contatto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContatto), new { id = contatto.Id }, contatto);
        }

        // PUT: api/Contatti/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContatto(int id, Contatto contatto)
        {
            if (id != contatto.Id)
            {
                return BadRequest();
            }

            _context.Entry(contatto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContattoExists(id))
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

        // DELETE: api/Contatti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContatto(int id)
        {
            var contatto = await _context.Contatti.FindAsync(id);
            if (contatto == null)
            {
                return NotFound();
            }

            _context.Contatti.Remove(contatto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContattoExists(int id)
        {
            return _context.Contatti.Any(e => e.Id == id);
        }
    }
}
