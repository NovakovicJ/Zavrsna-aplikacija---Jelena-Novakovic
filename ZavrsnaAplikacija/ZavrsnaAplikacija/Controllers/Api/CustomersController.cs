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
    public class CustomersController : ApiController
    {
        private RentACarEntities1 _context;
        
        public CustomersController()
        {
            _context = new RentACarEntities1();
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        public IHttpActionResult GetCustomer(int id)
        {
            var customer= _context.Customers.SingleOrDefault(g=>g.CustomerId == id);
            if(customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.CustomerId = customer.CustomerId;
            return Created(new Uri(Request.RequestUri + "/" + customer.CustomerId), customerDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb= _context.Customers.SingleOrDefault(g=>g.CustomerId==id);
            if (customerInDb == null)
                return NotFound();

            customerDto.CustomerId = customerInDb.CustomerId;
            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb=_context.Customers.SingleOrDefault(g=> g.CustomerId==id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
