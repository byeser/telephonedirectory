using contact.application.Repositories;
using contact.domain.Entities;
using contact.persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact.persistence.Repositories
{
    public class PersonsRepository : Repository<Persons>, IPersonsRepository
    {
        public PersonsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
