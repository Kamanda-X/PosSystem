using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Api.Data;
using POS.Api.Models.DTOs;
using POS.Api.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace POS.Api.Controllers
{
    public class CustomerCOntroller : BaseController
    {
        public CustomerCOntroller(PosDbContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto request)
        {
            var customer = new Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            _context.Add(customer);

            return Ok(customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerDto request)
        {
            var customer = await _context.Set<Customer>().Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (customer is null)
            {
                return BadRequest();
            }

            customer.PhoneNumber = request.PhoneNumber;
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;

            _context.Update(customer);
            var result = await _context.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _context.Set<Customer>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (customer is null)
            {
                return BadRequest();
            }

            var response = new CustomerDto()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Set<Customer>().Select(e => new CustomerDto()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber
            }).ToListAsync();

            return Ok(customers);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _context.Set<Customer>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (customer is null)
            {
                return BadRequest();
            }

            _context.Set<Customer>().Remove(customer);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
