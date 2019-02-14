using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;
using System.Reflection;
namespace HelpdeskViewModels
{
    public class DepartmentViewModel
    {
        private DepartmentModel _model;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Timer { get; set; }

        //Constructor
        public DepartmentViewModel()
        {
            _model = new DepartmentModel();
        }

        //Retrive all the employees
        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allVms = new List<DepartmentViewModel>();
            try
            {
                List<Department> allEmpdents = _model.GetAll();
                foreach (Department Emp in allEmpdents)
                {
                    DepartmentViewModel EmpVm = new DepartmentViewModel();
                   
                    EmpVm.Name = Emp.DepartmentName;
                    EmpVm.Id = Emp.Id;

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
    }
}
