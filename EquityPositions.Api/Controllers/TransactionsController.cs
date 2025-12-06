using EquityPositions.Core.Entities;
using EquityPositions.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquityPositions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IPositionService _positionService;

        public TransactionsController(
            ITransactionService transactionService,
            IPositionService positionService)
        {
            _transactionService = transactionService;
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            var created = await _transactionService.CreateAsync(transaction);

            await _positionService.RecalculatePositionsAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = created.TransactionId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Transaction transaction)
        {
            var updated = await _transactionService.UpdateAsync(id, transaction);
            if (updated == null)
                return NotFound();

            await _positionService.RecalculatePositionsAsync();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _transactionService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            await _positionService.RecalculatePositionsAsync();

            return NoContent();
        }

    }
}
