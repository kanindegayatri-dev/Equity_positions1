using EquityPositions.Core.Entities;
using EquityPositions.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquityPositions.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPositions()
        {
            var positions = await _positionService.GetPositionsAsync();
            return Ok(positions);
        }

        [HttpPost("recalculate")]
        public async Task<IActionResult> Recalculate()
        {
            await _positionService.RecalculatePositionsAsync();
            return Ok("Positions recalculated successfully");
        }

        [HttpGet("current")]
        public async Task<ActionResult<List<Position>>> GetCurrentPositions()
        {
            var positions = await _positionService.GetCurrentPositionsAsync();
            return Ok(positions);
        }
    }
}
