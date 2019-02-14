using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeWebsite.Reports;
using System.Diagnostics;
namespace EmployeeWebsite.Controllers
{
    public class ReportsController : ApiController
    {
        [Route("api/employeereport")]
        public IHttpActionResult GetEmloyeeReport()
        {
            try
            {
                EmployeeReport rep = new EmployeeReport();
                rep.doIt();
                return Ok("employee report generated");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("error" + ex.Message);
                return BadRequest("Retrived failed - Couldn't generate sample report");
            }

        }

        [Route("api/callreport")]

        public IHttpActionResult GetCallReport()
        {
            try
            {
                CallReport report = new CallReport();
                report.doIt();
                return Ok("call report generated");
            }
            catch (Exception ex)
            {
                return BadRequest("Report Generation failed - " + ex.Message);
            }
        }
    }
}
