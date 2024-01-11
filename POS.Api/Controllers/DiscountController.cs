using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;
using POS.Api.Models.DTOs.Loyalty;
using POS.Api.Models;
using POS.Api.Models.DTOs.Discount;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace POS.Api.Controllers
{
    public class DiscountController : BaseController
    {
        public DiscountController(PosDbContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscountDto request)
        {
            var discount = new Discount()
            {
                Amount = request.Amount,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Description = request.Description,
            };

            _context.Add(discount);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(discount);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDiscountDto request)
        {
            var discount = await _context.Set<Discount>().Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            discount.StartDate = request.StartDate;
            discount.EndDate = request.EndDate;
            discount.Description = request.Description;
            discount.Amount = request.Amount;


            _context.Update(discount);
            var result = await _context.SaveChangesAsync();
            return Ok(discount);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var disocunt = await _context.Set<Discount>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (disocunt == null)
            {
                return BadRequest();
            }

            var response = new DiscountDto()
            {
                Id = disocunt.Id,
                Amount = disocunt.Amount,
                StartDate = disocunt.StartDate,
                EndDate = disocunt.EndDate,
                Description = disocunt.Description,
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _context.Set<Discount>().Select(e => new DiscountDto()
            {
                Id = e.Id,
                Amount = e.Amount,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Description = e.Description,
            }).ToListAsync();

            return Ok(discounts);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var discount = await _context.Set<Discount>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (discount == null)
            {
                return BadRequest();
            }

            _context.Remove(discount);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
