using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZavrsnaAplikacija.Dtos;
using ZavrsnaAplikacija.Models;

namespace ZavrsnaAplikacija.Controllers.Api
{
    public class CarsController : ApiController
    {
        private RentACarEntities1 _context;

        public CarsController()
        {
            _context = new RentACarEntities1();
        }

        public IEnumerable<CarDto> GetCars()
        {
            return _context.Cars.ToList().Select(Mapper.Map<Car, CarDto>);
        }

        public IHttpActionResult GetCar(int id)
        {
            var car = _context.Cars.SingleOrDefault(g => g.CarId == id);
            if (car == null)
                return NotFound();
            return Ok(Mapper.Map<Car, CarDto>(car));
        }

        [HttpPost]
        public IHttpActionResult CreateCar(CarDto carDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var car = Mapper.Map<CarDto, Car>(carDto);
            _context.Cars.Add(car);
            _context.SaveChanges();

            carDto.CarId = car.CarId;
            return Created(new Uri(Request.RequestUri + "/" + car.CarId), carDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateCar(int id, CarDto carDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var carInDb = _context.Cars.SingleOrDefault(g => g.CarId == id);
            if (carInDb == null)
                return NotFound();

            carDto.CarId = carInDb.CarId;
            Mapper.Map(carDto, carInDb);

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCar(int id)
        {
            var carInDb = _context.Cars.SingleOrDefault(g => g.CarId == id);

            if (carInDb == null)
                return NotFound();

            _context.Cars.Remove(carInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
