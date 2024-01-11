using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;
using POS.Api.Models.DTOs.Loyalty;
using POS.Api.Models;
using POS.Api.Models.DTOs.Order;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace POS.Api.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(PosDbContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto request)
        {
            var order = new Order()
            {
                EmployeeId = request.EmployeeId,
                DiscountId = request.DiscountId,
                PaymentId = request.PaymentId,
                CustomerId = request.CustomerId,
                Status = request.Status,
                Date = request.Date,
            };

            _context.Add(order);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrderDto request)
        {
            var order = await _context.Set<Order>().Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (order is null)
            {
                return BadRequest();
            }

            order.Status = request.Status;
            order.Date = request.Date;
            order.CustomerId = request.CustomerId;
            order.DiscountId = request.DiscountId;
            order.PaymentId = request.PaymentId;
            order.EmployeeId = request.EmployeeId;
            

            _context.Update(order);
            var result = await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _context.Set<Order>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return BadRequest();
            }

            var response = new OrderDto()
            {
                Id = order.Id,
                EmployeeId = order.EmployeeId,
                DiscountId = order.DiscountId,
                PaymentId = order.PaymentId,
                CustomerId = order.CustomerId,
                Status = order.Status,
                Date = order.Date,
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Set<Order>().Select(e => new OrderDto()
            {
                Id = e.Id,
                EmployeeId = e.EmployeeId,
                DiscountId = e.DiscountId,
                PaymentId = e.PaymentId,
                CustomerId = e.CustomerId,
                Status = e.Status,
                Date = e.Date,
            }).ToListAsync();

            return Ok(orders);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _context.Set<Order>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return BadRequest();
            }

            _context.Remove(order);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
