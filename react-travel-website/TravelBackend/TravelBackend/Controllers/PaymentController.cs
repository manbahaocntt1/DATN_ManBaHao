using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelBackend.Models.DTOs;
using TravelBackend.Services;

namespace TravelBackend.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("momo-create")]
        public async Task<IActionResult> CreateMomoPayment([FromBody] CreateMomoPaymentDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Invalid model", errors });
            }
            var momoRes = await _paymentService.CreateMomoPaymentAsync(dto);
            return Ok(momoRes);
        }
    }

}
