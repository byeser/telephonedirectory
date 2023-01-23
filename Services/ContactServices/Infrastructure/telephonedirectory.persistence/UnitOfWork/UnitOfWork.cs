using telephonedirectory.application.Repositories;
using telephonedirectory.application.UnitOfWork;
using telephonedirectory.persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telephonedirectory.persistence.UnitOfWork
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
