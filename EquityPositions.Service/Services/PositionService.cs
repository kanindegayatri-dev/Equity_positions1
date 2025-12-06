using EquityPositions.Core.Entities;
using EquityPositions.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquityPositions.Service.Services
{
    public class PositionService : IPositionService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IPositionRepository _positionRepo;

        public PositionService(
            ITransactionRepository transactionRepo,
            IPositionRepository positionRepo)
        {
            _transactionRepo = transactionRepo;
            _positionRepo = positionRepo;
        }

        public async Task RecalculatePositionsAsync()
        {
            var transactions = await _transactionRepo.GetAllAsync();

            var grouped = transactions
                .GroupBy(t => t.SecurityCode)
                .Select(g => new Position
                {
                    SecurityCode = g.Key,
                    Quantity = g.Sum(t =>
                        t.BuySell == "Buy" ? t.Quantity : -t.Quantity)
                });

            foreach (var pos in grouped)
            {
                await _positionRepo.UpsertAsync(pos);
            }
        }

        public async Task<List<Position>> GetPositionsAsync()
        {
            return await _positionRepo.GetAllAsync();
        }
    }
}
