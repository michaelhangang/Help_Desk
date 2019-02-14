using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
public  class ProblemModel
    {
        IRepository<Problem> repoProblem;

        public ProblemModel()
        {
            repoProblem = new HelpRepository<Problem>();
        }

        public Problem GetByDescription(string desription)
        {
           List< Problem> pro = new List<Problem>();
            pro = repoProblem.GetByExpression(pr => pr.Description == desription);

            return pro.FirstOrDefault();
        }



       public List<Problem> GetAll()
        {
            List<Problem> pro = new List<Problem>();
            pro = repoProblem.GetAll();

            return pro;
        }

       public Problem GetById(int id)
        {
            List<Problem> pro = new List<Problem>();
            pro =repoProblem.GetByExpression(pr => pr.Id == id);

            return pro.FirstOrDefault();
        }
    }
}
