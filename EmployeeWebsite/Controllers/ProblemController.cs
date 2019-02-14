using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace EmployeeWebsite.Controllers
{
    public class ProblemController : ApiController
    {
        [Route("api/problems")]
        public IHttpActionResult GetAll()
        {
            try
            {
                 ProblemViewModel pro = new ProblemViewModel();
                List<ProblemViewModel> allProblems = pro.GetAll();
                return Ok(allProblems);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
    }
}
