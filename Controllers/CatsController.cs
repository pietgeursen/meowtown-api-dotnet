using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using meowtown_api.Models;

namespace meowtown_api.Controllers
{
    [Route("api/cats")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly MeowtownContext _context;

        public CatsController(MeowtownContext context)
        {
            _context = context;
        }

        // GET: api/Cats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cat>>> GetCats()
        {
            return await _context.Cats.ToListAsync();
        }

        // GET: api/Cats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> GetCat(long id)
        {
            var cat = await _context.Cats.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            cat.Lives -= 1;

            if(cat.Lives == 0){
              _context.Cats.Remove(cat);
              return NotFound();
            }else{
              _context.Entry(cat).State = EntityState.Modified;
              await _context.SaveChangesAsync();
            }

            return cat;
        }


        // PUT: api/Cats/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCat(long id, Cat cat)
        {
            if (id != cat.Id)
            {
                return BadRequest();
            }

            _context.Entry(cat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatExists(id))
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

        // POST: api/Cats
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cat>> PostCat(Cat cat)
        {
          if(!ModelState.IsValid){
            return BadRequest();
          }
          if(await TryUpdateModelAsync<ICatInputModel>(cat)){
            cat.Lives = 9;
            _context.Cats.Add(cat);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCat", new { id = cat.Id }, cat);
          }
          return BadRequest();
        }

        // DELETE: api/Cats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cat>> DeleteCat(long id)
        {
            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();

            return cat;
        }

        private bool CatExists(long id)
        {
            return _context.Cats.Any(e => e.Id == id);
        }
    }
}
