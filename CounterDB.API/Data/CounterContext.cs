using CounterDB.API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CounterDB.API.Data
{
    public class CounterContext : DbContext
    {
        public CounterContext (DbContextOptions<CounterContext> options)
            : base(options)
        {
        }

        public DbSet<CounterData> Counter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
        }
    }
}
