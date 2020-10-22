using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fund.Web.Models;

namespace Fund.Web.Models
{
    public class FundContext : DbContext
    {
        public FundContext(DbContextOptions<FundContext>options): base(options) { }


        public DbSet<Group> group { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasIndex(t => t.name)
                .IsUnique();

            modelBuilder.Entity<Group>()
               .HasIndex(t => t.name)
               .IsUnique();
        }


        public DbSet<Person> person { get; set; }


        public DbSet<Fund.Web.Models.Payment> Payment { get; set; }

        

    }
}
