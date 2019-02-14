using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;
using System.Reflection;

namespace HelpdeskViewModels
{
  public  class ProblemViewModel
    {
        private ProblemModel _model;
        public string Description { get; set; }

        public int Id { get; set; }

        public ProblemViewModel()
        {
            _model = new ProblemModel();

        }

        public void GetByDescription()
        {
            try
            {
                Problem pro = _model.GetByDescription(Description);
                Id = pro.Id;
                Description = pro.Description;
            }
         
              catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
        }

        public List<ProblemViewModel> GetAll()
        {
            List<ProblemViewModel> allpro = new List<ProblemViewModel>();
            try
            {
                List<Problem> pros =_model.GetAll();
                foreach(Problem pr in pros)
                {
                    ProblemViewModel pro = new ProblemViewModel();
                    pro.Id = pr.Id;
                    pro.Description = pr.Description;
                    allpro.Add(pro);
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allpro;
        }
    }

  
}
