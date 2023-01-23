using contact.application.Repositories;
using contact.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact.persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Persons> Persons { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }
    }
}