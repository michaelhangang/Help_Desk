/*
Class name:EmployeeViewModel
Purpose:   Provide data and functioanality to be used by html
Author:    Gang Han
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;
using System.Reflection;


namespace HelpdeskViewModels
{
   public  class EmployeeViewModel
    {
        //Class members
        private EmployeeModel _model;

        public string Title { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string phoneno { get; set; }

        public string Timer { get; set; }

        public int DepartmentId { get; set; }

        public int DepartmentName { get; set; }

        public int Id { get; set; }

        public bool? IsTech { get; set; }

        public string StaffPicture64 { get; set; }

        public EmployeeViewModel()
        {
            _model = new EmployeeModel();
        }

        //Find an employee using lastname property
        public void GetByLastname()
        {
            try
            {
                Employee Emp = _model.GetByLastname(Lastname);
                Title = Emp.Title;
                Firstname = Emp.FirstName;
                Lastname = Emp.LastName;
                phoneno = Emp.PhoneNo;
                Email = Emp.Email;
                Id = Emp.Id;
                DepartmentId = Emp.DepartmentId;
                if (Emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(Emp.StaffPicture);
                }
                Timer = Convert.ToBase64String(Emp.Timer);
            }
            catch (NullReferenceException ex)
            {
                Lastname = "not found";
            }

            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in" + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                    " " + ex.Message);
                throw ex;
            }


        }

        //Retrive all the employees
        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> allVms = new List<EmployeeViewModel>();
            try
            {
                List<Employee> allEmpdents = _model.GetAll();
                foreach (Employee Emp in allEmpdents)
                {
                    EmployeeViewModel EmpVm = new EmployeeViewModel();
                    EmpVm.Title = Emp.Title;
                    EmpVm.Firstname = Emp.FirstName;
                    EmpVm.Lastname = Emp.LastName;
                    EmpVm.phoneno = Emp.PhoneNo;
                    EmpVm.Email = Emp.Email;
                    EmpVm.Id = Emp.Id;
                    EmpVm.DepartmentId = Emp.DepartmentId;
                    EmpVm.IsTech = Emp.IsTech;
                    if (Emp.StaffPicture != null)
                    {
                        EmpVm.StaffPicture64 = Convert.ToBase64String(Emp.StaffPicture);
                    }
                    EmpVm.Timer = Convert.ToBase64String(Emp.Timer);
                    allVms.Add(EmpVm);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allVms;
        }

        //Add an employee
        public void Add()
        {
            Id = -1;
            try
            {
                Employee Emp = new Employee();
                Emp.Title = Title;
                Emp.FirstName = Firstname;
                Emp.LastName = Lastname;
                Emp.PhoneNo = phoneno;
                Emp.Email = Email;
                Emp.DepartmentId = DepartmentId;
                if (StaffPicture64 != null)
                {
                    Emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                Id = _model.Add(Emp);
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
                Employee Emp = new Employee();
                Emp.Title = Title;
                Emp.FirstName = Firstname;
                Emp.LastName = Lastname;
                Emp.PhoneNo = phoneno;
                Emp.Email = Email;
                Emp.Id = Id;
                Emp.DepartmentId = DepartmentId;
                if (StaffPicture64 != null)
                {
                    Emp.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }
                Emp.Timer = Convert.FromBase64String(Timer);
                opStatus = _model.Update(Emp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(opStatus); 
        }

        //Delete a employee
        public int Delete()
        {
            int employeesDeleted = -1;

            try
            {
                employeesDeleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return employeesDeleted;
        }

        //Find employee using is Id property
        public void GetById()
        {
            try
            {
                Employee Emp = _model.GetById(Id);
                Title = Emp.Title;
                Firstname = Emp.FirstName;
                Lastname = Emp.LastName;
                phoneno = Emp.PhoneNo;
                Email = Emp.Email;
                Id = Emp.Id;
                DepartmentId = Emp.DepartmentId;
                if (Emp.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(Emp.StaffPicture);

                }
                Timer = Convert.ToBase64String(Emp.Timer);
            }
            catch (NullReferenceException ex)
            {
                Lastname = "not found";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }


        }
    }
}