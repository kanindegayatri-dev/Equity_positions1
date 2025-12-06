using EquityPositions.Core.Entities;
using EquityPositions.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquityPositions.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction?> UpdateAsync(int id, Transaction transaction)
        {
            var existing = await _transactionRepository.GetByIdAsync(id);
            if (existing == null)
                return null;

            // map fields you allow to change
            existing.TradeId = transaction.TradeId;
            existing.Version = transaction.Version;
            existing.SecurityCode = transaction.SecurityCode;
            existing.Quantity = transaction.Quantity;
            existing.Action = transaction.Action;   // Insert/Update/Cancel
            existing.BuySell = transaction.BuySell;  // Buy/Sell

            await _transactionRepository.UpdateAsync(existing);
            await _transactionRepository.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _transactionRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _transactionRepository.DeleteAsync(existing);
            await _transactionRepository.SaveChangesAsync();
            return true;
        }
    }
}
