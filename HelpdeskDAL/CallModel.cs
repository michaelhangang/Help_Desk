using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace HelpdeskDAL
{
  public  class CallModel
    {
        //Class member 
        IRepository<Call> repo;

        //Default constructor
        public CallModel()
        {
            repo = new HelpRepository<Call>();
        }

        //Get an call information  by Id
        public Call GetById(int id)
        {
            List<Call> selectedCall = null;

            try
            {
                selectedCall = repo.GetByExpression(call => call.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedCall.FirstOrDefault();
        }

        //get all calls
        public List<Call> GetAll()
        {
            List<Call> allCalls = new List<Call>();

            try
            {
                allCalls = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allCalls;
        }

        //Add a new call 
        public int Add(Call newCall)
        {
            try
            {

                newCall = repo.Add(newCall);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return newCall.Id;
        }

        //Update an call
        public UpdateStatus Update(Call updateCall)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                opStatus = repo.Update(updateCall);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return opStatus;
        }

        //Delete an call by Id
        public int Delete(int id)
        {
            int CallDeleted = -1;
            try
            {
                CallDeleted = repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return CallDeleted;
        }

        //Get an call by note
        public Call GetByNote(string note)
        {
            List<Call> selectedCall= null;

            try
            {
                selectedCall = repo.GetByExpression(call => call.Notes == note);//using Lambda expressions 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedCall.FirstOrDefault();
        }




    }
}
