using contact.application.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact.application.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        IContactInfoRepository ContactInfoRepository { get; }
        IPersonsRepository PersonsRepository { get; } 
    }
}
