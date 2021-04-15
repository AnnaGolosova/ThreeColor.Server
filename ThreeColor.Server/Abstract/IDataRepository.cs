using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using ThreeColor.Data.Models;
using ThreeColor.Server.Helpers;

namespace ThreeColor.Server.Abstract
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataRepository
    {
        ReturnDataModel<List<Tests>> GetTests(bool includeDeleted);

        ReturnDataModel<Tests> GetTest(int testId);

        ReturnDataModel UpdateTest(Tests test);

        ReturnDataModel<int> AddTest(Tests result);

        ReturnDataModel<int> GetNewTestingNumber();

        ReturnDataModel<List<Points>> GetPoints(Guid testId, bool includeDeleted);

        ReturnDataModel<List<Points>> GetPoints();

        ReturnDataModel UpdatePoints(List<Points> points);

        ReturnDataModel AddPoints(List<Points> points);

        ReturnDataModel AddResult(Results result);

        ReturnDataModel<List<Results>> GetResultsByTest(int testId);

        ReturnDataModel<List<Results>> GetLastResults();

        ReturnDataModel<List<Results>> GetResults();

        ReturnDataModel<Users> GetExistingUser(Users userModel);

        ReturnDataModel<Users> AddUser(Users userModel);

        ReturnDataModel<Users> GetUser(string newId);

        ReturnDataModel<double> GetAverageByLastTest();
    }
}