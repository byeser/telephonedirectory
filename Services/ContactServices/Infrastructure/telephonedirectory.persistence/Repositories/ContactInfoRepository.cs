using telephonedirectory.application.Repositories;
using telephonedirectory.domain.Entities;
using telephonedirectory.persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telephonedirectory.persistence.Repositories
{
    public class ContactInfoRepository : Repository<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
