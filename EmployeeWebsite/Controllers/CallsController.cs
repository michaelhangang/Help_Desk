using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace EmployeeWebsite.Controllers
{
    public class CallsController : ApiController
    {
        [Route("api/calls")]
        public IHttpActionResult GetAll()
        {
            try
            {
                CallViewModel cal = new CallViewModel();
                List<CallViewModel> allcalls = cal.GetAll();
                return Ok(allcalls);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/calls")]
        //Add an employee information to the database. return a result to represent if the addition is successful
        public IHttpActionResult Post(CallViewModel cal)
        {
            try
            {
                cal.Add();
                if (cal.Id > 0)
                {
                    return Ok("Call added!");
                }
                else
                {
                    return Ok("Call not added");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }

        [Route("api/calls")]
        //accept an object and return a result to represent if the update is successful
        public IHttpActionResult Put(CallViewModel cal)
        {
            try
            {
                int retVal = cal.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Call updated!");
                    case -1:
                        return Ok("Call not updated");
                    case -2:
                        return Ok("Call is stale! Call not updated");
                    default:
                        return Ok("Call not updated!");


                }

            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }


        [Route("api/calls/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                CallViewModel cal = new CallViewModel();
                cal.Id = id;
                if (cal.Delete() == 1)
                {
                    return Ok("Call deleted!");
                }
                else
                {
                    return Ok("Call not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Deleted failed - Contact Tech Support");
            }
        }


    }
}

