using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;
using POS.Api.Models.DTOs.Item;
using POS.Api.Models;
using POS.Api.Models.DTOs.OrderItem;
using Microsoft.EntityFrameworkCore;

namespace POS.Api.Controllers
{
    public class OrderItemController : BaseController
    {
        public OrderItemController(PosDbContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderItemDto request)
        {
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

        [HttpPut]
        public async Task<IActionResult> Update(UpdateOrderItemDto request)
        {
            var orderItem = await _context.Set<OrderItem>().Where(c => c.OrderId == request.OrderId && c.ItemId == request.ItemId).FirstOrDefaultAsync();

            if (orderItem == null)
            {
                return BadRequest();
            }

            orderItem.Amount = request.Amount;

            _context.Update(orderItem);
            var result = await _context.SaveChangesAsync();
            return Ok(orderItem);
        }

        [HttpGet("/api/[controller]/{orderId}&{itemId}")]
        public async Task<IActionResult> GetById(Guid orderId, Guid itemId)
        {
            var orderItem = await _context.Set<OrderItem>().Where(c => c.OrderId == orderId && c.ItemId == itemId).FirstOrDefaultAsync();

            if (orderItem == null)
            {
                return BadRequest();
            }

            var response = new OrderItemDto()
            {
                OrderId = orderItem.OrderId,
                ItemId = orderItem.ItemId,
                Amount = orderItem.Amount
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.Set<OrderItem>().Select(e => new OrderItemDto()
            {
                OrderId = e.OrderId,
                ItemId = e.ItemId,
                Amount = e.Amount
            }).ToListAsync();

            return Ok(items);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid orderId, Guid itemId)
        {
            var orderItem = await _context.Set<OrderItem>().Where(c => c.OrderId == orderId && c.ItemId == itemId).FirstOrDefaultAsync();

            if (orderItem == null)
            {
                return BadRequest();
            }

            _context.Set<OrderItem>().Remove(orderItem);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
