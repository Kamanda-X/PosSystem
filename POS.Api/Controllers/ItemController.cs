using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;
using POS.Api.Models.DTOs;
using POS.Api.Models;
using POS.Api.Models.DTOs.Item;
using Microsoft.EntityFrameworkCore;

namespace POS.Api.Controllers
{
    public class ItemController : BaseController
    {
        public ItemController(PosDbContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDto request)
        {
            var item = new Item()
            {
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
            };

            _context.Add(item);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateItemDto request)
        {
            var item = await _context.Set<Item>().Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            item.Name = request.Name;
            item.Category = request.Category;
            item.Price = request.Price;

            _context.Update(item);
            var result = await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _context.Set<Item>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return BadRequest();
            }

            var response = new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Price = item.Price,
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.Set<Item>().Select(e => new ItemDto()
            {
                Id = e.Id,
                Name = e.Name,
                Category = e.Category,
                Price = e.Price,
            }).ToListAsync();

            return Ok(items);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Set<Item>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return BadRequest();
            }

            _context.Set<Item>().Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
