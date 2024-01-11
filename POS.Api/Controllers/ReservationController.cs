﻿using Microsoft.AspNetCore.Mvc;
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
                return Ok(reservation);
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
    }
}