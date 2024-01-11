using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;
using POS.Api.Models;
using POS.Api.Models.DTOs.Loyalty;
using Microsoft.EntityFrameworkCore;

namespace POS.Api.Controllers
{
    public class LoyaltyController : BaseController
    {
        public LoyaltyController(PosDbContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLoyaltyDto request)
        {
            var loyalty = new Loyalty()
            {
                Discount = request.Discount,
                Type = request.Type,
            };

            _context.Add(loyalty);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(loyalty);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLoyaltyDto request)
        {
            var loyalty = await _context.Set<Loyalty>().Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            loyalty.Discount = request.Discount;
            loyalty.Type = request.Type;

            _context.Update(loyalty);
            var result = await _context.SaveChangesAsync();
            return Ok(loyalty);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var loyalty = await _context.Set<Loyalty>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (loyalty == null)
            {
                return BadRequest();
            }

            var response = new LoyaltyDto()
            {
                Id = loyalty.Id,
                Discount = loyalty.Discount,
                Type = loyalty.Type,
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loyalties = await _context.Set<Loyalty>().Select(e => new LoyaltyDto()
            {
                Id = e.Id,
                Discount = e.Discount,
                Type = e.Type,
            }).ToListAsync();

            return Ok(loyalties);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var loyalty = await _context.Set<Loyalty>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (loyalty == null)
            {
                return BadRequest();
            }

            _context.Remove(loyalty);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
