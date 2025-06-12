using Microsoft.AspNetCore.Mvc;
using TravelBackend.Models.DTOs;
using TravelBackend.Services;

namespace TravelBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetByBookingId(int bookingId)
        {
            var list = await _transactionService.GetTransactionsByBookingIdAsync(bookingId);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _transactionService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)
        {
            var result = await _transactionService.AddTransactionAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.TransactionId }, result);
        }
    }

}
