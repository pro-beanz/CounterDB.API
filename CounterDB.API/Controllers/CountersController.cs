using CounterDB.API.Data;
using CounterDB.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CounterDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountersController : ControllerBase
    {
        private readonly CounterContext _context;

        public CountersController(CounterContext context)
        {
            _context = context;
        }

        // GET: api/Counters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CounterData>>> GetCounter()
        {
            return await _context.Counter.ToListAsync();
        }

        // GET: api/Counters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CounterData>> GetCounter(long id)
        {
            var counter = await _context.Counter.FindAsync(id);

            if (counter == null)
            {
                return NotFound();
            }

            return counter;
        }

        // PUT: api/Counters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCounter(long id, CounterAPI counter)
        {
            _context.Entry(ApiToData(id, counter)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CounterExists(id))
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

        // POST: api/Counters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CounterAPI>> PostCounter(CounterAPI counter)
        {
            CounterData data = ApiToData(counter);
            _context.Counter.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCounter", new { id = data.Id }, data);
        }

        // DELETE: api/Counters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounter(long id)
        {
            var counter = await _context.Counter.FindAsync(id);
            if (counter == null)
            {
                return NotFound();
            }

            _context.Counter.Remove(counter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CounterExists(long id)
        {
            return _context.Counter.Any(e => e.Id == id);
        }

        private static CounterData ApiToData(CounterAPI counter)
        {
            return new CounterData
            {
                Name = counter.Name,
                Count = counter.Count
            };
        }
        private static CounterData ApiToData(long id, CounterAPI counter)
        {
            return new CounterData
            {
                Id = id,
                Name = counter.Name,
                Count = counter.Count
            };
        }
    }
}
