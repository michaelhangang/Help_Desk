using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpdeskViewModels;
using System.Collections.Generic;
using HelpdeskDAL;

namespace CaseStudyTests
{
    [TestClass]
    public class EmployeeViewModelTests
    {
        //    [TestMethod]
        //    public void EmployeeViewModelAddShouldReturnId()
        //    {
        //        EmployeeViewModel vm = new EmployeeViewModel();
        //        vm.Title = "Mr.";
        //        vm.Firstname = "Test";
        //        vm.Lastname = "Han";
        //        vm.Email = "ts@abc.com";
        //        vm.phoneno = "(555)555-5551";
        //        vm.DepartmentId = 100;
        //        vm.Add();
        //        Assert.IsTrue(vm.Id > 0);

        //    }

        //    [TestMethod]
        //    public void EmployeeViewModelGetbyNameShouldPopulatePropertyFirstname()
        //    {
        //        EmployeeViewModel vm = new EmployeeViewModel();
        //        vm.Lastname = "Han";
        //        vm.GetByLastname();
        //        Assert.IsNotNull(vm.Firstname);
        //    }

        //    [TestMethod]
        //    public void EmployeeViewModealGetAllShouldReturnAtLeastOneVm()
        //    {
        //        EmployeeViewModel vm = new EmployeeViewModel();
        //        List<EmployeeViewModel> allStudentVms = vm.GetAll();
        //        Assert.IsTrue(allStudentVms.Count > 0);
        //    }

        //    [TestMethod]
        //    public void EmployeeViewModelGetByIdShouldPopulatePropertyFirstname()
        //    {
        //        EmployeeViewModel vm = new EmployeeViewModel();
        //        vm.Lastname = "Han";
        //        vm.GetByLastname();
        //        vm.GetById();
        //        Assert.IsNotNull(vm.Firstname);

        //    }

        //    [TestMethod]
        //    public void EmployeeViewModelUpdateShouldReturnOkStatus()
        //    {
        //        EmployeeViewModel vm = new EmployeeViewModel();
        //        vm.Lastname = "Han";
        //        vm.GetByLastname();
        //        vm.Email = (vm.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
        //        int StudentsUpdated = vm.Update();
        //        Assert.IsTrue(StudentsUpdated > 0);

        //    }

        //    [TestMethod]
        //    public void EmployeeViewModelDeleteShouldReturnOne()
        //    {
        //        EmployeeViewModel vm = new EmployeeViewModel();
        //        vm.Lastname = "Han";
        //        vm.GetByLastname();
        //        int studentDeleted = vm.Delete();
        //        Assert.IsTrue(studentDeleted == 1);
        //    }

        //    [TestMethod]

        //    public void StudentViewModelUpdateTwiceShouldReturnNegativeTwo()
        //    {
        //        EmployeeViewModel vm1 = new EmployeeViewModel();
        //        EmployeeViewModel vm2 = new EmployeeViewModel();
        //        vm1.Lastname = "Han";
        //        vm2.Lastname = "Han";
        //        vm1.GetByLastname();
        //        vm2.GetByLastname();
        //        vm1.Email = (vm1.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
        //        if (vm1.Update() == 1)
        //        {
        //            vm2.Email = (vm2.Email.IndexOf(".ca") > 0) ? "ts@abc.com" : "ts@abc.ca";
        //            Assert.IsTrue(vm2.Update() == -2);
        //        }
        //        else
        //            Assert.Fail();
        //    }
        // [TestMethod]
        //public void ComprehensiveModelTestsShouldReturnTrue()
        //{
        //    CallModel cmodel = new CallModel();
        //    EmployeeModel emodel = new EmployeeModel();
        //    ProblemModel pmodel = new ProblemModel();
        //    Call call = new Call();
        //    call.DateOpened = DateTime.Now;
        //    call.DateClosed = null;
        //    call.OpenStatus = true;
        //    call.EmployeeId = emodel.GetByLastname("Han").Id;
        //    call.TechId = emodel.GetByLastname("Burner").Id;
        //    call.ProblemId = pmodel.GetByDescription("Hard Drive Failure").Id;
        //    call.Notes = "Gang's drive is shot, Burner to fix it";
        //    int newCallId = cmodel.Add(call);
        //    Console.WriteLine("New Call Generated - Id - " + newCallId);
        //    call = cmodel.GetById(newCallId);
        //    byte[] oldtimer = call.Timer;
        //    Console.WriteLine("New Call Retrieved");
        //    call.Notes += "\n Orderd new RAM!";

        //    if (cmodel.Update(call) == UpdateStatus.Ok)
        //    {
        //        Console.WriteLine("Call was updated " + call.Notes);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Call was not updated!");
        //    }

        //    call.Timer = oldtimer;
        //    if (cmodel.Update(call) == UpdateStatus.Stale)
        //    {
        //        Console.WriteLine("Call was not updated due to stale data");
        //    }
        //    cmodel = new CallModel();
        //    call = cmodel.GetById(newCallId);

        //    if (cmodel.Delete(newCallId) == 1)
        //    {
        //        Console.WriteLine("Call was deleted!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Call was not deleted");
        //    }
        //    Assert.IsNull(cmodel.GetById(newCallId));
        //}

        //[TestClass]
        //public class CallViewModelTests
        //{
        //    [TestMethod]
        //    [ExpectedException(typeof(NullReferenceException))]
        //    public void ComprehensiveVMTests()
        //    {
        //        CallViewModel cvm = new CallViewModel();
        //        EmployeeViewModel evm = new EmployeeViewModel();
        //        ProblemViewModel pvm = new ProblemViewModel();
        //        cvm.DateOpened = DateTime.Now;
        //        cvm.DateClosed = null;
        //        cvm.OpenStatus = true;
        //        evm.Lastname = "Han";
        //        evm.GetByLastname();
        //        cvm.EmployeeId = evm.Id;
        //        evm.Lastname = "Burner";
        //        evm.GetByLastname();
        //        cvm.TechId = evm.Id;
        //        pvm.Description = "Memory Upgrade";
        //        pvm.GetByDescription();
        //        cvm.ProblemId = pvm.Id;
        //        cvm.Notes = "Gang has bad Ram, Burner to fix it";
        //        cvm.Add();
        //        Console.WriteLine("New Call Generated - Id = " + cvm.Id);
        //        int id = cvm.Id;
        //        cvm.GetById();
        //        cvm.Notes += "\n Ordered new Ram!";

        //        if (cvm.Update() == 1)
        //        {
        //            Console.WriteLine("Call was updated " + cvm.Notes);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Call was not updated!");
        //        }

        //        cvm.Notes = "Another change to comments that should not work";
        //        if (cvm.Update() == -2)
        //        {
        //            Console.WriteLine("Call was not updated data was stale");
        //        }

        //        cvm = new CallViewModel();
        //        cvm.Id = id;
        //        cvm.GetById();

        //        if (cvm.Delete() == 1)
        //        {
        //            Console.WriteLine("Call was deleted!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Call was not deleted");
        //        }

        //        cvm.GetById();
        //    }

        //    [TestMethod]
        //    public void callviewmodeltest()
        //    {
        //        CallViewModel cal = new CallViewModel();
        //        List<CallViewModel> calls = cal.GetAll();
        //        Assert.IsTrue(calls.Count > 0);


        //    }


        // }

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void ComprehensiveVMTests()
        //{
        //    CallViewModel cvm = new CallViewModel();
        //    EmployeeViewModel evm = new EmployeeViewModel();
        //    ProblemViewModel pvm = new ProblemViewModel();
        //    cvm.DateOpened = DateTime.Now;
        //    cvm.DateClosed = null;
        //    cvm.OpenStatus = true;
        //    evm.Lastname = "Han";
        //    evm.GetByLastname();
        //    cvm.EmployeeId = evm.Id;
        //    evm.Lastname = "Burner";
        //    evm.GetByLastname();
        //    cvm.TechId = evm.Id;
        //    pvm.Description = "Memory Upgrade";
        //    pvm.GetByDescription();
        //    cvm.ProblemId = pvm.Id;
        //    cvm.Notes = "Gang has bad Ram, Burner to fix it";
        //    cvm.Add();
        //    Console.WriteLine("New Call Generated - Id = " + cvm.Id);
        //}
        [TestMethod]
        public void callviewmodeltest()
        {
            CallViewModel cal = new CallViewModel();
            List<CallViewModel> calls = cal.GetAll();
            //Assert.IsTrue(calls.Count > 0);
            foreach (CallViewModel ca in calls)
            {
                EmployeeModel emp = new EmployeeModel();
                Employee em = emp.GetById(ca.EmployeeId);
                ProblemModel pro = new ProblemModel();
                Problem pr = pro.GetById(ca.ProblemId);
            }
          
        }


    }
}
