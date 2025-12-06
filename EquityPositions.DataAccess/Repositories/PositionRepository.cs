using System;
using System.Collections.Generic;
using System.Text;
using EquityPositions.Core.Entities;
using EquityPositions.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace EquityPositions.DataAccess.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly AppDbContext _context;

        public PositionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task UpsertAsync(Position position)
        {
            var existing = await _context.Positions
                .FirstOrDefaultAsync(p => p.SecurityCode == position.SecurityCode);

            if (existing == null)
            {
                _context.Positions.Add(position);
            }
            else
            {
                existing.Quantity = position.Quantity;
            }

            await _context.SaveChangesAsync();
        }
    }
}
