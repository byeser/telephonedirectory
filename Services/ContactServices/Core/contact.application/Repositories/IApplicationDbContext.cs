using contact.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact.application.Repositories
{
    public interface IApplicationDbContext
    { 
        public DbSet<Persons> Persons { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; } 
    }
}
