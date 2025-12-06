using EquityPositions.Core.Entities;
using EquityPositions.DataAccess.Data;
using EquityPositions.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.ToListAsync();
    }
    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(t => t.TransactionId == id);
    }
    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public Task UpdateAsync(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
