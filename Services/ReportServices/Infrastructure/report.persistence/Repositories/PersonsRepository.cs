using report.application.Repositories;
using report.domain.Entities;
using report.persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report.persistence.Repositories
{
    public class PersonsRepository : Repository<Persons>, IPersonsRepository
    {
        public PersonsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
