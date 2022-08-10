using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data
{
    public class SamuraiContext : DbContext
    {
        public SamuraiContext()
        {

        }

        public SamuraiContext(DbContextOptions<SamuraiContext> options):base(options)
        {

        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Sword> Swords { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<SamuraiBattleStat> SamuraiBattleStats { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sample2Db")
                .LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name},
                Microsoft.Extensions.Logging.LogLevel.Information).EnableSensitiveDataLogging();*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>().HasMany(s => s.Battles)
                .WithMany(b => b.Samurais)
                .UsingEntity<BattleSamurai>(bs => bs.HasOne<Battle>().WithMany(),
                bs => bs.HasOne<Samurai>().WithMany()).Property(bs => bs.DateJoined)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Sword>().HasMany(c => c.Elements)
                .WithMany(e => e.Swords)
                .UsingEntity<ElementSword>(ec => ec.HasOne<Element>().WithMany(),
                ec => ec.HasOne<Sword>().WithMany()).Property(bs => bs.DateJoined)
                .HasDefaultValueSql("getdate()");


            modelBuilder.Entity<SamuraiBattleStat>().HasNoKey().ToView("SamuraiBattleStats");
            modelBuilder.Entity<SwordElementStat>().HasNoKey().ToView("SwordElementStat");
        }
    }
}
