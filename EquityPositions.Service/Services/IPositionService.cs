using EquityPositions.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquityPositions.Service.Services
{
    public interface IPositionService
    {
        Task RecalculatePositionsAsync();
        Task<List<Position>> GetPositionsAsync();
    }
}
