using report.application.Repositories;
using report.application.UnitOfWork;
using report.persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report.persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(
            ApplicationDbContext applicationDbContext,
            IContactInfoRepository ContactInfoRepository,
            IPersonsRepository PersonsRepository
            )
        {
            _applicationDbContext = applicationDbContext;
            ContactInfoRepository = ContactInfoRepository;
            PersonsRepository = PersonsRepository;
        }

        public IContactInfoRepository ContactInfoRepository { get; }

        public IPersonsRepository PersonsRepository { get; }
         

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _applicationDbContext.Database.BeginTransactionAsync();
        }
    }
}
