/*
Class name:EmployeeModel
Purpose:   facilitate database access against the employees table
Author:    Gang Han
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace HelpdeskDAL
{
    public class EmployeeModel
    {
        //Class member 
        IRepository<Employee> repo;
        
        //Default constructor
        public EmployeeModel()
        {
            repo = new HelpRepository<Employee>();
        }

        //Get an emploee information  by email
        public Employee GetByEmail(string email)
        {
           List<Employee> selectedEmployee = null;

            try
            {
              
                selectedEmployee = repo.GetByExpression(emp => emp.Email == email);

            }
            catch(Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
            }


        //Get an emploee information  by Id
        public Employee GetById(int id)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(stu => stu.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }

        //get all employees
        public List<Employee> GetAll()
        {
            List<Employee> allEmployees = new List<Employee>();

            try
            {
                allEmployees = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allEmployees;
        }

        //Add a new employee 
        public int Add(Employee newEmployee)
        {
            try
            {

                newEmployee= repo.Add(newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return newEmployee.Id;
        }

        //Update an employee
        public UpdateStatus Update(Employee updateEmployee)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                opStatus= repo.Update(updateEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return opStatus;
        }

        //Delete an employee by Id
        public int Delete(int id)
        {
            int EmployeeDeleted = -1;
            try
            {
                EmployeeDeleted = repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return EmployeeDeleted;
        }

        //Get an employee by lastname
        public Employee GetByLastname(string name)
        {
           List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(stu => stu.LastName == name);//using Lambda expressions 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedEmployee.FirstOrDefault();
        }


    }
}
