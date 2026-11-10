using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Web.Controllers
{
    [ApiController]
    [Route("api/payments")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> CreatePayment(int orderId, [FromBody] CreatePaymentDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _paymentService.CreatePaymentAsync(
                userId,
                orderId,
                dto.PaymentMethod);

            return Ok(result);
        }

        [HttpPost("{paymentId}/process")]
        public async Task<IActionResult> ProcessPayment(int paymentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _paymentService.ProcessPaymentAsync(userId, paymentId);

            return Ok(result);
        }


        [HttpPost("{paymentId}/fail")]
        public async Task<IActionResult> FailPayment(int paymentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _paymentService.FailPaymentAsync(userId, paymentId);

            return Ok(result);
        }
    }
}
