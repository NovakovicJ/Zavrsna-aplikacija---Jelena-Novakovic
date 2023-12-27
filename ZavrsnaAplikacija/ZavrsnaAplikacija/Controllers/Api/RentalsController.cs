using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZavrsnaAplikacija.Dtos;
using ZavrsnaAplikacija.Models;
using System.Data.Entity;

namespace ZavrsnaAplikacija.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private RentACarEntities1 _context;

        public RentalsController()
        {
            _context = new RentACarEntities1();
        }

        public IEnumerable<RentalDto> GetRentals()
        {
            var rentalQuery = _context.Rentals.Include(r => r.Car).Include(c => c.Customer);

            return _context.Rentals.ToList().Select(Mapper.Map<Rental, RentalDto>);
        }

        public IHttpActionResult GetRental(int id)
        {
            var rental = _context.Rentals.SingleOrDefault(g => g.RentalId == id);
            if (rental == null)
                return NotFound();
            return Ok(Mapper.Map<Rental, RentalDto>(rental));
        }

        [HttpPost]
        public IHttpActionResult CreateRental(RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var rental = Mapper.Map<RentalDto, Rental>(rentalDto);
            _context.Rentals.Add(rental);
            _context.SaveChanges();

            rentalDto.RentalId = rental.RentalId;
            return Created(new Uri(Request.RequestUri + "/" + rental.RentalId), rentalDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateRental(int id, RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var rentalInDb = _context.Rentals.SingleOrDefault(g => g.RentalId == id);
            if (rentalInDb == null)
                return NotFound();

            rentalDto.RentalId = rentalInDb.RentalId;
            Mapper.Map(rentalDto, rentalInDb);

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteRental(int id)
        {
            var rentalInDb = _context.Rentals.SingleOrDefault(g => g.RentalId == id);

            if (rentalInDb == null)
                return NotFound();

            _context.Rentals.Remove(rentalInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
