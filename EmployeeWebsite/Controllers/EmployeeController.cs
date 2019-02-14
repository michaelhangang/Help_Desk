/*
Class name:EmployeeController
Purpose:   handles incoming HTTP requests and send response back to the caller
Author:    Gang Han
*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;
namespace EmployeeWebsite.Controllers
{
    public class EmployeeController : ApiController
    {
        [Route("api/employees/{name}")]
        //accept a name to return employee information
        public IHttpActionResult Get(string name)
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                emp.Lastname = name;
                emp.GetByLastname();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        //accept an object and return a result to represent if the update is successful
        public IHttpActionResult Put(EmployeeViewModel emp)
        {
            try
            {
                int retVal = emp.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Employee " + emp.Lastname + " updated!");
                    case -1:
                        return Ok("Employee " + emp.Lastname + " not updated");
                    case -2:
                        return Ok("Data is stale for " + emp.Lastname + ", Employee not updated");
                    default:
                        return Ok("Employee " + emp.Lastname + " not updated!");


                }

            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        //Get all the employees
        public IHttpActionResult GetAll()
        {
            try
            {
                EmployeeViewModel emp = new EmployeeViewModel();
                List<EmployeeViewModel> allEmployees = emp.GetAll();
                return Ok(allEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/employees")]
        //Add an employee information to the database. return a result to represent if the addition is successful
        public IHttpActionResult Post(EmployeeViewModel emp)
        {
            try
            {
                emp.Add();
                if (emp.Id > 0)
                {
                    return Ok("Employee " + emp.Lastname + " added!");
                }
                else
                {
                    return Ok("Employee " + emp.Lastname + " not added");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/employees/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                EmployeeViewModel stu = new EmployeeViewModel();
                stu.Id = id;
                if (stu.Delete() == 1)
                {
                    return Ok("Employee deleted!");
                }
                else
                {
                    return Ok("Employee not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Deleted failed - Contact Tech Support");
            }
        }

    }
}
