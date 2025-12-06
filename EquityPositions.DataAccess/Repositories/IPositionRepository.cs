using System;
using System.Collections.Generic;
using System.Text;
using EquityPositions.Core.Entities;

namespace EquityPositions.DataAccess.Repositories
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetAllAsync();
        Task UpsertAsync(Position position);
    }

}
