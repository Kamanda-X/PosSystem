using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Api.Data;
using POS.Api.Models.DTOs.Order;
using POS.Api.Models;
using POS.Api.Models.DTOs.Reservation;

namespace POS.Api.Controllers
{
    public class ReservationController : BaseController
    {
        public ReservationController(PosDbContext context) : base(context)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationRequest request)
        {
            if (request.TableId is not null)
            {
                var reservation = new Reservation()
                {
                    TableId = request.TableId,
                    Start = request.Start,
                    End = request.End,
                };

                _context.Add(reservation);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok(reservation);
            } else
            {
                var existingReservation = await _context.Set<Reservation>().Where(r => r.TimeSlotId == request.TimeSlotId
                && r.Start.Year == request.Start.Year
                && r.Start.Month == request.Start.Month
                && r.Start.Day == request.Start.Day).FirstOrDefaultAsync();

                if (existingReservation is not null)
                {
                    return BadRequest("Time slot is already occupied");
                }
                var reservation = new Reservation()
                {
                    TimeSlotId = request.TimeSlotId,
                    Start = request.Start,
                    End = request.End,
                };

                _context.Add(reservation);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    return BadRequest();
                }

                var timeSlot = await _context.Set<TimeSlot>().Where(r => r.Id == request.TimeSlotId).FirstOrDefaultAsync();
                var response = new ReservationResponse()
                {
                    Id = reservation.Id,
                    TimeSlot = new Models.DTOs.TimeSlot.ListItemTimeSlotResponse
                    {
                        Id = timeSlot.Id,
                        Start = timeSlot.Start,
                        End = timeSlot.End,
                    },
                    TableId = reservation.TableId,
                    Start = reservation.Start,
                    End = reservation.End,
                };

                return Ok(response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ReservationRequest request)
        {
            var reservation = await _context.Set<Reservation>().Where(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (reservation is null)
            {
                return BadRequest();
            }

            reservation.TableId = request.TableId;
            reservation.TableId = request.TableId;
            reservation.Start = request.Start;
            reservation.End = request.End;

            _context.Update(reservation);
            var result = await _context.SaveChangesAsync();
            return Ok(result);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var reservation = await _context.Set<Reservation>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (reservation == null)
            {
                return BadRequest();
            }

            var response = new ReservationResponse()
            {
                Id = reservation.Id,
                TableId = reservation.TableId,
                Start = reservation.Start,
                End = reservation.End,
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await _context.Set<Reservation>().Select(e => new ReservationResponse()
            {
                Id = e.Id,
                TableId = e.TableId,
                Start = e.Start,
                End = e.End,
            }).ToListAsync();
            return Ok(reservations);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reservation = await _context.Set<Reservation>().Where(c => c.Id == id).FirstOrDefaultAsync();

            if (reservation == null)
            {
                return BadRequest();
            }

            _context.Remove(reservation);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("/api/[controller]/FufillOrder")]
        public async Task<IActionResult> FufillReservation(Guid id)
        {
            var reservation = await _context.Set<Reservation>().Where(c => c.Id == id).Include(r => r.TimeSlot).ThenInclude(t => t.Service).FirstOrDefaultAsync();

            if (reservation == null || reservation.TimeSlotId == null)
            {
                return BadRequest();
            }

            var order = new Order
            {
                EmployeeId = reservation.TimeSlot.Service.EmployeeId,
                ReservationId = reservation.Id,
                Amount = reservation.TimeSlot.Service.Price,
                Date = DateTime.Now,
                Status = OrderStatus.Ongoing,
            };

            _context.Add(order);
            await _context.SaveChangesAsync();

            var response = new OrderResponse
            {
                Id = order.Id,
                Amount = order.Amount,
                ReservationId= order.ReservationId,
                EmployeeId = order.EmployeeId,
                DiscountId = order.DiscountId,
                PaymentId = order.PaymentId,
                CustomerId = order.CustomerId,
                Status = order.Status,
                Date = order.Date,
            };

            return Ok(response);
        }
    }
}
