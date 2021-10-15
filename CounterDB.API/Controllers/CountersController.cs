using CounterDB.API.Models;
using CounterDB.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CounterDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountersController : ControllerBase
    {
        private readonly CounterService _counterService;

        public CountersController(CounterService counterService)
        {
            _counterService = counterService;
        }

        // GET: api/Counters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CounterData>>> GetCounter()
        {
            return Ok(await _counterService.GetAllAsync());
        }

        // GET: api/Counters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CounterData>> GetCounter(long id)
        {
            var counter = await _counterService.GetAsync(id);
            return counter == null ? NotFound() : Ok(counter);
        }

        // PUT: api/Counters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCounter(long id, CounterAPI counter)
        {
            var data = await _counterService.UpdateAsync(id, counter);
            return data == null ? NotFound() : Ok(data);
        }

        // POST: api/Counters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CounterAPI>> PostCounter(CounterAPI counter)
        {
            var data = await _counterService.CreateAsync(counter);
            return CreatedAtAction("GetCounter", new { id = data.Id }, data);
        }

        // DELETE: api/Counters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounter(long id)
        {
            return await _counterService.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
