using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;
using POS.Api.Models.DTOs.Loyalty;
using POS.Api.Models;
using POS.Api.Models.DTOs.Order;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using POS.Api.Models.DTOs.OrderItem;

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

            var response = new OrderResponse
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
            order.Amount = order.Amount;
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

            var response = new OrderResponse()
            {
                Id = order.Id,
                Amount = order.Amount,
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
            var orders = await _context.Set<Order>().Select(e => new OrderResponse()
            {
                Id = e.Id,
                Amount = e.Amount,
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

        [HttpPost("/api/[controller]/AddItem")]
        public async Task<IActionResult> AddItem(CreateOrderItemDto request)
        {
            var existingOrderItem = await _context.Set<OrderItem>().Where(o => o.OrderId == request.OrderId && o.ItemId == request.ItemId).FirstOrDefaultAsync();

            if (existingOrderItem is not null)
            {
                existingOrderItem.Amount += request.Amount;
                _context.Update(existingOrderItem);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            var orderItem = new OrderItem()
            {
                OrderId = request.OrderId,
                ItemId = request.ItemId,
                Amount = request.Amount,
            };

            _context.Add(orderItem);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(orderItem);
        }

        [HttpPost("/api/[controller]/RemoveItem")]
        public async Task<IActionResult> RemoveItem(Guid orderId, Guid itemId)
        {
            var existingOrderItem = await _context.Set<OrderItem>().Where(o => o.OrderId == orderId && o.ItemId == itemId).FirstOrDefaultAsync();

            if (existingOrderItem is null)
            {
                return BadRequest();
            }

            _context.Set<OrderItem>().Remove(existingOrderItem);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("/api/[controller]/AddTip")]
        public async Task<IActionResult> AddTip(AddTipRequest request)
        {
            var existingOrder = await _context.Set<Order>().Where(o => o.Id == request.OrderId).FirstOrDefaultAsync();

            if (existingOrder is null || existingOrder.TipAmount != 0)
            {
                return BadRequest();
            }

            existingOrder.TipAmount = request.Amount;

            _context.Set<Order>().Update(existingOrder);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("/api/[controller]/ApplyDiscount")]
        public async Task<IActionResult> ApplyDiscount(ApplyDiscountRequest request)
        {
            var existingOrder = await _context.Set<Order>().Where(o => o.Id == request.OrderId).FirstOrDefaultAsync();

            if (existingOrder is null || existingOrder.TipAmount != 0)
            {
                return BadRequest();
            }

            existingOrder.DiscountId = request.DiscountId;

            _context.Set<Order>().Update(existingOrder);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
