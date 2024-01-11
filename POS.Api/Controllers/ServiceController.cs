using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Api.Data;
using POS.Api.Models.DTOs.Reservation;
using POS.Api.Models;
using POS.Api.Models.DTOs.Service;
using POS.Api.Models.DTOs.TimeSlot;

namespace POS.Api.Controllers
{
    public class ServiceController : BaseController
    {
        public ServiceController(PosDbContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceRequest request)
        {
            var service = new Service()
            {
                Name = request.Name,
                EmployeeId = request.EmployeeId,
                Price = request.Price,
                Description = request.Description,
            };

            _context.Add(service);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(service.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ServiceRequest request)
        {
            var service = await _context.Set<Service>().Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (service is null)
            {
                return BadRequest();
            }

            service.Name = request.Name;
            service.EmployeeId = request.EmployeeId;
            service.Price = request.Price;
            service.Description = request.Description;

            _context.Update(service);
            var result = await _context.SaveChangesAsync();
            return Ok(result);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var service = await _context.Set<Service>().Where(c => c.Id == id).Include(s => s.TimeSlots).FirstOrDefaultAsync();

            if (service == null)
            {
                return BadRequest();
            }

            var response = new ServiceResponse()
            {
                Id = service.Id,
                Name = service.Name,
                Price = service.Price,
                EmployeeId = service.EmployeeId,
                Description = service.Description,
                TimeSlots = service.TimeSlots.Select(t => new ListItemTimeSlotResponse
                {
                    Id = t.Id,
                    Start = t.Start,
                    End = t.End,
                })
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _context.Set<Service>().Select(e => new ServiceResponse()
            {
                Id = e.Id,
                Name = e.Name,
                EmployeeId = e.EmployeeId,
                Description = e.Description,
                Price = e.Price,
            }).ToListAsync();
            return Ok(services);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var service = await _context.Set<Service>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (service == null)
            {
                return BadRequest();
            }

            _context.Remove(service);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("/api/[controller]/AddTimeSlot")]
        public async Task<IActionResult> AddTimeSlot(TimeSlotRequest request)
        {
            var timeSlot = new TimeSlot()
            {
                Start = request.Start,
                End = request.End,
                ServiceId = request.ServiceId,
            };

            _context.Add(timeSlot);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(timeSlot.Id);
        }

        [HttpGet("/api/[controller]/GetTimeSlots")]
        public async Task<IActionResult> GetAllTimeSlotsForService(Guid id)
        {
            var timeSlots = await _context.Set<TimeSlot>().Select(e => new ListItemTimeSlotResponse()
            {
                Id = e.Id,
                Start = e.Start,
                End = e.End,
            }).ToListAsync();
            return Ok(timeSlots);
        }
    }
}
