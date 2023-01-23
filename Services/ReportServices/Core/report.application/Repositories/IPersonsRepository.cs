using report.domain.Entities; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report.application.Repositories
{
    public interface IPersonsRepository : IRepository<Persons>
    {
    }
}
