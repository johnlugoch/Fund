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

        public DbSet<Fund.Web.Models.Group> group { get; set; }


        public DbSet<Fund.Web.Models.Person> person { get; set; }


        public DbSet<Fund.Web.Models.Payment> Payment { get; set; }

        

    }
}
