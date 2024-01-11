using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;
using POS.Api.Models.DTOs.Loyalty;
using POS.Api.Models;
using POS.Api.Models.DTOs.Payment;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace POS.Api.Controllers
{
    public class PaymentController : BaseController
    {
        public PaymentController(PosDbContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentDto request)
        {
            var order = await _context.Set<Order>().Where(c => c.Id == request.OrderId).FirstOrDefaultAsync();

            if (order is null)
            {
                return BadRequest();
            }

            var payment = new Payment()
            {
                Amount = request.Amount,
                Type = request.Type,
                TaxRate = request.TaxRate,
                TipAmount = request.TipAmount,
                Date = request.Date,
                CardNumber = request.CardNumber,
            };

            _context.Add(payment);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(payment);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePaymentDto request)
        {
            var payment = await _context.Set<Payment>().Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (payment is null)
            {
                return BadRequest();
            }

            payment.Amount = request.Amount;
            payment.Type = request.Type;
            payment.TaxRate = request.TaxRate;
            payment.TipAmount = request.TipAmount;
            payment.Date = request.Date;
            payment.CardNumber = request.CardNumber;

            _context.Update(payment);
            var result = await _context.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await _context.Set<Payment>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (payment == null)
            {
                return BadRequest();
            }

            var response = new PaymentDto()
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Type = payment.Type,
                TaxRate = payment.TaxRate,
                TipAmount = payment.TipAmount,
                Date = payment.Date,
                CardNumber = payment.CardNumber,
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _context.Set<Payment>().Select(e => new PaymentDto()
            {
                Id = e.Id,
                Amount = e.Amount,
                Type = e.Type,
                TaxRate = e.TaxRate,
                TipAmount = e.TipAmount,
                Date = e.Date,
                CardNumber = e.CardNumber,
            }).ToListAsync();

            return Ok(payments);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var payments = await _context.Set<Payment>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (payments == null)
            {
                return BadRequest();
            }

            _context.Remove(payments);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
