﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using ThreeColor.Data.Models;
using ThreeColor.Server.Abstract;
using ThreeColor.Server.Helpers;

namespace ThreeColor.Server.Data.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly TestContext _dataContext;

        /// <inheritdoc/>
        public DataRepository(TestContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ReturnDataModel AddPoints(List<Points> points)
        {
            ReturnDataModel returnModel = new ReturnDataModel();
            try
            {
                _dataContext.Points.AddRange(points);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel AddResult(Results result)
        {
            ReturnDataModel returnModel = new ReturnDataModel();
            try
            {
                _dataContext.Results.Add(result);
                _dataContext.SaveChanges();
            } catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<Guid> AddTest(Tests test)
        {
            ReturnDataModel<Guid> returnModel = new ReturnDataModel<Guid>();
            try
            {
                _dataContext.Tests.Add(test);
                _dataContext.SaveChanges();

                returnModel.Data = test.Id;
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<List<Points>> GetPoints(Guid testId, bool includeDeleted)
        {
            ReturnDataModel<List<Points>> returnModel = new ReturnDataModel<List<Points>>();
            try
            {
                if(includeDeleted)
                    returnModel.Data = _dataContext.Points.Where(p => p.TestId == testId).ToList();
                else
                    returnModel.Data = _dataContext.Points.Where(p => p.TestId == testId && p.IsDeleted == 0).ToList();
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<Tests> GetTest(int testId)
        {
            ReturnDataModel<Tests> returnModel = new ReturnDataModel<Tests>();
            try
            {
                returnModel.Data = _dataContext.Tests.Find(testId);
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<List<Tests>> GetTests(bool includeDeleted)
        {
            ReturnDataModel<List<Tests>> returnModel = new ReturnDataModel<List<Tests>>();
            try
            {
                if(includeDeleted)
                    returnModel.Data = _dataContext.Tests.ToList();
                else
                    returnModel.Data = _dataContext.Tests.Where(t => t.IsDeleted == 0).ToList();
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel UpdatePoints(List<Points> points)
        {
            ReturnDataModel returnModel = new ReturnDataModel();
            try
            {
                if(points != null && points.Count > 0)
                {
                    var testId = points.First().TestId;

                    _dataContext.Points.Where(p => p.TestId == testId).ToList().ForEach(p =>
                    {
                        p.IsDeleted = 1;
                    });

                    points.ForEach(p =>
                    {
                        p.TestId = testId;
                        _dataContext.Points.Add(p);
                    });

                    _dataContext.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel UpdateTest(Tests test)
        {
            ReturnDataModel returnModel = new ReturnDataModel();
            try
            {
                var oldTest = _dataContext.Tests.Find(test.Id);
                oldTest.FieldColor = test.FieldColor;
                oldTest.MaxInterval = test.MaxInterval;
                oldTest.MinInterval = test.MinInterval;
                oldTest.Name = test.Name;
                oldTest.PointSize = test.PointSize;
                oldTest.Speed = test.Speed;
                oldTest.IsDeleted = test.IsDeleted;
                oldTest.FieldHeight = test.FieldHeight;
                oldTest.FieldWidth = test.FieldWidth;

                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<List<Results>> GetLastResults()
        {
            ReturnDataModel<List<Results>> returnModel = new ReturnDataModel<List<Results>>();
            try
            {
                if (_dataContext.Results.Count() > 0)
                {
                    int lastTestingNumber = _dataContext.Results.Max(r => r.TestingNumber);
                    returnModel.Data = _dataContext.Results.Where(r => r.TestingNumber == lastTestingNumber).ToList();
                }
                else
                {
                    returnModel.Data = new List<Results>();
                }
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<double> GetAverageByLastTest()
        {
            ReturnDataModel<double> returnModel = new ReturnDataModel<double>();
            try
            {
                var pointId = GetLastResults().Data.Max(d => d.PointId);
                var testId = _dataContext.Points.Find(pointId).TestId;
                var pointsByTest = _dataContext.Points.Where(p => p.TestId == testId).Select(p => p.Id);
                returnModel.Data = _dataContext.Results.Where(r => pointsByTest.Contains(r.PointId)).Average(r => r.Time);
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<List<Results>> GetResults()
        {
            ReturnDataModel<List<Results>> returnModel = new ReturnDataModel<List<Results>>();
            try
            {
                returnModel.Data = _dataContext.Results.ToList();
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<List<Results>> GetResultsByTest(Guid testId)
        {
            ReturnDataModel<List<Results>> returnModel = new ReturnDataModel<List<Results>>();
            try
            {
                var points = _dataContext.Points.Where(r => r.TestId == testId);
                returnModel.Data = _dataContext.Results.Where(r => points.Any(p => r.PointId == p.Id)).ToList();
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<int> GetNewTestingNumber()
        {
            ReturnDataModel<int> returnModel = new ReturnDataModel<int>();
            try
            {
                returnModel.Data = _dataContext.Results.Count() > 0 ? _dataContext.Results.Max(r => r.TestingNumber) + 1 : 1;
            }
            catch (Exception ex)
            {
                returnModel.LoadException(ex);
            }

            return returnModel;
        }

        public ReturnDataModel<Users> GetExistingUser(Users userModel)
        {
            var result = new ReturnDataModel<Users>();
            try
            {
                result.Data = _dataContext.Users.First(u =>
                    string.Equals(u.Activity, userModel.Activity) &&
                    string.Equals(u.Gender, userModel.Gender) &&
                    u.Age == userModel.Age);
            }
            catch(InvalidOperationException)
            {
                result.ErrorMessage = "User cannot be found";
            }

            return result;
        }

        public ReturnDataModel<Users> AddUser(Users userModel)
        {
            var result = new ReturnDataModel<Users>();
            try
            {
                _dataContext.Users.Add(userModel);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.LoadException(ex);
            }
            return result;
        }

        public ReturnDataModel<List<Points>> GetPoints()
        {
            var result = new ReturnDataModel<List<Points>>();
            try
            {
                result.Data = _dataContext.Points.ToList();
            }
            catch (Exception ex)
            {
                result.LoadException(ex);
            }

            return result;
        }
    }
}