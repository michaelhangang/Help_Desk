using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;
using System.Reflection;

namespace HelpdeskViewModels
{
public  class CallViewModel
    {
        private CallModel _model;

        public int Id { get; set; }
        public string Timer { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string ProblemDescription { get; set; }

        public int ProblemId { get; set; }

        public int TechId { get; set; }

        public DateTime DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public bool OpenStatus { get; set; }

        public string Notes { get; set; }

        public CallViewModel()
        {
            _model = new CallModel();
        }

       

        //Retrive all the calls
        public List<CallViewModel> GetAll()
        {
            List<CallViewModel> allCalls = new List<CallViewModel>();
            try
            {
                List<Call> allCal = _model.GetAll();
                foreach (Call ca in allCal)
                {
                    CallViewModel cal = new CallViewModel();
                    EmployeeModel emp = new EmployeeModel();
                    Employee em = emp.GetById(ca.EmployeeId);
                    ProblemModel pro = new ProblemModel();
                    Problem pr = pro.GetById(ca.ProblemId);
                    cal.Id = ca.Id;
                    cal.EmployeeId = ca.EmployeeId;
                    cal.EmployeeName = em.LastName;
                    cal.TechId = ca.TechId;
                    cal.ProblemId = ca.ProblemId;
                    cal.ProblemDescription = pr.Description;
                    cal.OpenStatus = ca.OpenStatus;
                    cal.Notes = ca.Notes;                    
                    cal.DateOpened = ca.DateOpened;
                    cal.DateClosed = ca.DateClosed;
                    cal.Timer = Convert.ToBase64String(ca.Timer);
                    allCalls.Add(cal);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allCalls;
        }

        //Add an call
        public void Add()
        {
            Id = -1;
            try
            {
                Call ca = new Call();
                ca.EmployeeId = EmployeeId;
                ca.TechId = TechId;
                ca.ProblemId = ProblemId;
                ca.OpenStatus = OpenStatus;
                ca.Notes = Notes;
                ca.DateOpened = DateOpened;
                ca.DateClosed = DateClosed;
                Id = _model.Add(ca);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        //Update an employ
        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                Call ca = new Call();
                ca.Id = Id;
                ca.EmployeeId = EmployeeId;
                ca.TechId = TechId;
                ca.ProblemId = ProblemId;
                ca.OpenStatus = OpenStatus;
                ca.Notes = Notes;
                ca.DateOpened = DateOpened;
                ca.DateClosed = DateClosed;
                ca.Timer = Convert.FromBase64String(Timer);
                opStatus = _model.Update(ca);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(opStatus);
        }

        //Delete a call
        public int Delete()
        {
            int callsDeleted = -1;

            try
            {
                callsDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return callsDeleted;
        }

        //Find call using is Id property
        public void GetById()
    {
        try
        {
                Call ca = _model.GetById(Id);
                Id = ca.Id;
                EmployeeId = ca.EmployeeId;
                TechId = ca.TechId;
                ProblemId = ca.ProblemId;
                OpenStatus = ca.OpenStatus;
                Notes = ca.Notes;
                DateOpened = ca.DateOpened;
                DateClosed = ca.DateClosed;
                Timer = Convert.ToBase64String(ca.Timer);             
        }
     
        catch (Exception ex)
        {
            Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
            throw ex;
        }
    }
}
}
