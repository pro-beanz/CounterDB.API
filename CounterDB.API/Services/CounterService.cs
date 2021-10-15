using CounterDB.API.Data;
using CounterDB.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CounterDB.API.Services
{
    public class CounterService
    {
        private readonly CounterContext _context;

        public CounterService(CounterContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CounterData>> GetAllAsync()
        {
            return await _context.Counter.ToListAsync();
        }

        public async Task<CounterData> GetAsync(long id)
        {
            return await _context.Counter.FindAsync(id);
        }

        public async Task<CounterData> UpdateAsync(long id, CounterAPI counter)
        {
            var data = ApiToData(id, counter);
            _context.Entry(data).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CounterExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return data;
        }

        public async Task<CounterData> CreateAsync(CounterAPI counter)
        {
            var data = ApiToData(counter);
            _context.Counter.Add(data);
            await _context.SaveChangesAsync();

            return data;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var counter = await _context.Counter.FindAsync(id);
            if (counter == null)
            {
                return false;
            }

            _context.Counter.Remove(counter);
            await _context.SaveChangesAsync();

            return true;
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
