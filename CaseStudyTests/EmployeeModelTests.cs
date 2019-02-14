using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpdeskDAL;
using System.Collections.Generic;

namespace CaseStudyTests
{
    [TestClass]
    public class EmployeeModelTests
    {
        //[TestMethod]
        //public void EmployeeModelGetbyEmailShouldReturnEmployee()
        //{
        //    EmployeeModel model = new EmployeeModel();
        //    Employee someEmployee = model.GetByEmail("bs@abc.com");
        //    Assert.IsNotNull(someEmployee);
        //}
        //[TestMethod]
        //public void EmployeeModelGetbyEmailShouldNotReturnEmployee()
        //{
        //    EmployeeModel model = new EmployeeModel();
        //    Employee someEmployee = model.GetByEmail("ddd@abc.com");
        //    Assert.IsNull(someEmployee);
        //}

        [TestMethod]
        public void EmployeeModelGetAllShouldReturnList()
        {
            EmployeeModel model = new EmployeeModel();
            List<Employee> allEmployees = model.GetAll();
            Assert.IsTrue(allEmployees.Count > 0);
        }

        [TestMethod]

        public void EmployeeModelAddShouldReturnNewId()
        {
            EmployeeModel model = new EmployeeModel();
            Employee newEmployee = new Employee();
            newEmployee.Title = "Mr.";
            newEmployee.FirstName = "Gang";
            newEmployee.LastName = "Han";
            newEmployee.Email = "gh@abc.com";
            newEmployee.PhoneNo = "(416)455-5751";
            newEmployee.DepartmentId= 100;
            int newId = model.Add(newEmployee);
            Assert.IsTrue(newId > 0);
        }

        [TestMethod]

        public void EmployeeModelGetbyIdShouldReturnEmployee()
        {
            EmployeeModel model = new EmployeeModel();
            Employee someEmployee = model.GetByEmail("gh@abc.com");
            Employee anotherEmployee = model.GetById(someEmployee.Id);
            Assert.IsNotNull(anotherEmployee);
        }

        [TestMethod]

        public void EmployeeModelUpdateShouldReturnOkStatus()
        {
            EmployeeModel model = new EmployeeModel();
            Employee updateEmployee = model.GetByEmail("gh@abc.com");
            updateEmployee.Email = (updateEmployee.Email.IndexOf(".com") > 0) ? "ts@abcd.com" : "ts@abcd.ca";
            UpdateStatus employeeUpdate = model.Update(updateEmployee);
            Assert.IsTrue(employeeUpdate == UpdateStatus.Ok);

        }

        [TestMethod]

        public void EmployeeModelDeleteShouldReturnOne()
        {
            EmployeeModel model = new EmployeeModel();
            Employee deleteEmployee = model.GetByEmail("ts@abcd.com");
            int EmployeesDeleted = model.Delete(deleteEmployee.Id);
            Assert.IsTrue(EmployeesDeleted == 1);
        }

        [TestMethod]
        public void EmployeeModelUpdateTwiceShouldReturnStaleStatus()
        {
            EmployeeModel model1 = new EmployeeModel();
            EmployeeModel model2 = new EmployeeModel();
            Employee updateStudent1 = model1.GetByLastname("Pin");
            Employee updateStudent2 = model2.GetByLastname("Pin");
            updateStudent1.Email = (updateStudent1.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
            if (model1.Update(updateStudent1) == UpdateStatus.Ok)
            {
                updateStudent2.Email = (updateStudent2.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
                Assert.IsTrue(model2.Update(updateStudent2) == UpdateStatus.Stale);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LoadPicsShouldReturnTrue()
        {
            DALUtil util = new DALUtil();
            Assert.IsTrue(util.AddEmployeePicsToDb());
        }
    }
}
