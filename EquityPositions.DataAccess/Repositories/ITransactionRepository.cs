using EquityPositions.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquityPositions.DataAccess.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync();
        Task<Transaction?> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
        Task SaveChangesAsync();
    }
}
