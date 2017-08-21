﻿using abkar_api.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using abkar_api.Contexts;
using System.Linq;
namespace abkar_api.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        static readonly Customers customerRepos = new Customers();

        DatabaseContext db = new DatabaseContext();

        //Get Customers
        [HttpGet]
        [Route("")] 
        public List<Customers> getCustomers()
        {
            return db.customers.OrderByDescending(c => c.id).ToList();
        }

        //Get Customer Detail
        [HttpGet]
        [Route("{id}")] 
        public IHttpActionResult getCustomer(int id)
        {
            Customers customer = db.customers.FirstOrDefault(p => p.id == id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        //Add Customer
        [HttpPost]
        [Route("")]
        public IHttpActionResult add([FromBody] Customers customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            db.customers.Add(customer);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ExceptionController.Handle(e);
            }
            return Ok(customer);
        }

  
        //Update Customer
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult update([FromBody] Customers customer, int id)
        {
            Customers customerDetail = db.customers.Find(id);
            if (customerDetail == null) return NotFound();
            customerDetail.company = customer.company;
            customerDetail.adress = customer.adress;
            customerDetail.city = customer.city;
            customerDetail.email = customer.email;
            customerDetail.lastname = customer.lastname;
            customerDetail.name = customer.name;
            customerDetail.password = customer.password;
            customerDetail.phone = customer.phone;
            customerDetail.updated_date = DateTime.Now;
            customerDetail.state = customer.state;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e) 
            {
                ExceptionController.Handle(e);
            }
          
            return Ok(customerDetail);
        }
        
        //Delete customer
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult delete(int id)
        {
            Customers customer = db.customers.Find(id);
            if(customer == null ) return NotFound();
            db.customers.Remove(customer);            
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ExceptionController.Handle(e);
            }
            return Ok();
        }


    }



    
}
