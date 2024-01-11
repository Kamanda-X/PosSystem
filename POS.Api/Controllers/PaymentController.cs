using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;
using POS.Api.Models.DTOs.Loyalty;
using POS.Api.Models;
using POS.Api.Models.DTOs.Payment;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace POS.Api.Controllers
{
    public class PaymentController : BaseController
    {
        public PaymentController(PosDbContext context) : base(context)
        {
        }


        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await _context.Set<Payment>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (payment == null)
            {
                return BadRequest();
            }

            var response = new ProcessPaymentResponse()
            {
                Id = payment.Id,
                Sum = payment.Sum,
                AmountPaid = payment.AmountPaid,
                Change = payment.Change,
                TaxRate = payment.TaxRate,
                Date = payment.Date,
                Discount = payment.Discount,
                Total = payment.Sum * (1 + payment.TaxRate / 100 - payment.Discount / 100)
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _context.Set<Payment>().Select(e => new ProcessPaymentResponse()
            {
                Id = e.Id,
                Sum = e.Sum,
                AmountPaid = e.AmountPaid,
                Change = e.Change,
                TaxRate = e.TaxRate,
                Date = e.Date,
                Discount = e.Discount,
                Total = e.Sum * (1 + e.TaxRate / 100 - e.Discount / 100)
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

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(ProcessPaymentRequest request)
        {
            var order = await _context.Set<Order>().Where(c => c.Id == request.OrderId).Include(o => o.OrderItems).ThenInclude(o => o.Item).Include(o => o.Discount).FirstOrDefaultAsync();

            if (order is null)
            {
                return BadRequest();
            }

            var sum = order.Amount is null or 0 ? order.OrderItems.Sum(oi => oi.Amount * oi.Item.Price) : order.Amount ?? 0;

            var total = sum * (1 + request.TaxRate / 100 - (order.Discount is null ? 0 : order.Discount.Amount / 100));

            if (sum > request.Amount)
            {
                return BadRequest("Not enough funds");
            }
            
            

            var payment = new Payment()
            {
                Total = total,
                Sum = sum,
                AmountPaid = request.Amount,
                Change = request.Type == PaymentType.Cash ? request.Amount - total : null,
                Type = request.Type,
                TaxRate = request.TaxRate,
                Date = request.Date,
                CardNumber = request.CardNumber,
                Discount = order.Discount != null ? order.Discount.Amount : 0
            };

            _context.Add(payment);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(new ProcessPaymentResponse()
            {
                Id = payment.Id,
                Sum = payment.Sum,
                AmountPaid = payment.AmountPaid,
                Change = payment.Change,
                TaxRate = payment.TaxRate,
                Date = payment.Date,
                Discount = payment.Discount,
                Total = payment.Sum * (1 + payment.TaxRate / 100 - payment.Discount / 100)
            });
        }
    }
}
