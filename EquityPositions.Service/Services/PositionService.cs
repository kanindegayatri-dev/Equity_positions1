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
        public async Task<List<Position>> GetCurrentPositionsAsync()
        {
            var allTransactions = await _transactionRepo.GetAllAsync();

            var latestPerTrade = allTransactions
            .GroupBy(t => new { t.TradeId, t.SecurityCode })
            .Select(g => g
                .OrderByDescending(t => t.Version)
                .First())
                .Select(t => new
                {
                    t.SecurityCode,
                    Units =
                    t.Action == "CANCEL"
                    ? 0
                    : t.BuySell == "Buy"
                        ? t.Quantity
                        : -t.Quantity
                })
                ;
                
            //.Where(t => !string.Equals(t.Action, "Cancel",
              //                         StringComparison.OrdinalIgnoreCase));

            //var activeTransactions = latestPerTrade.Where(t => !t.Action.Equals("CANCEL", StringComparison.OrdinalIgnoreCase));

            var positions = latestPerTrade
            .GroupBy(t => t.SecurityCode)
            .Select(g => new Position
            {
                SecurityCode = g.Key,
                Quantity = g.Sum(x => x.Units)
            })
            .ToList();

            return positions;
        }
    }
}
