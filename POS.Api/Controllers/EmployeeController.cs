using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Api.Data;
using POS.Api.Models;
using POS.Api.Models.DTOs;

namespace POS.Api.Controllers
{
    public class EmployeeController : BaseController
    {
        private UserManager<Employee> _userManager;

        public EmployeeController(PosDbContext context, UserManager<Employee> userManager) : base(context)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto request)
        {
            var employee = new Employee()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(employee);

            if (!result.Succeeded)
            {
                return BadRequest(employee);
            }

            result = await _userManager.AddPasswordAsync(employee, request.Password);

            if (result.Succeeded)
            {
                return Ok(employee);
            }

            return BadRequest(employee);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEmployeeDto request)
        {
            var employee = await _userManager.FindByIdAsync(request.Id.ToString());

            employee.PhoneNumber = request.PhoneNumber;
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Email = request.Email;
            employee.UserName = request.UserName;

            var result = await _userManager.UpdateAsync(employee);
            if (result.Succeeded)
            {
                return Ok(employee);
            }
            return BadRequest(employee);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employee = await _userManager.FindByIdAsync(id.ToString());

            if (employee is null)
            {
                return BadRequest();
            }

            var response = new EmployeeDto()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _context.Set<Employee>().Select(e => new EmployeeDto()
            {
                Id = Guid.Parse(e.Id),
                UserName = e.UserName,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber
            }).ToListAsync();

            return Ok(employees);
        }
    }
}
