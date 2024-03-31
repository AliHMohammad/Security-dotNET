using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Security_CSharp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var transaction = _dataContext.Database.BeginTransaction();

            return transaction.GetDbTransaction();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
