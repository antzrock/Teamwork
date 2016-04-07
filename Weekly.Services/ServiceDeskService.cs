using SysAid.Data.Infrastructure;
using SysAid.Data.Model;
using SysAid.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class ServiceDeskService : IServiceDeskService
    {
        #region Variables
        private readonly IServiceDeskRepository _serviceDeskRepository;
        private const int SR_TYPE_INCIDENT = 1;
        private const int SR_TYPE_REQUEST = 10;
        private const int SR_TYPE_PROBLEM = 6;
        #endregion

        public ServiceDeskService(IServiceDeskRepository serviceDeskRepository)
        {
            _serviceDeskRepository = serviceDeskRepository;
        }

        public List<ProjectTask> GetProjectsTasksForUser(string userName, DateTime startDate, DateTime endDate)
        {
            return _serviceDeskRepository.GetProjectTasksForUser(userName, startDate, endDate);
        }

        public List<ServiceRequest> GetProblemsForUser(string userName, DateTime startDate, DateTime endDate)
        {
            return _serviceDeskRepository.GetServiceRequestsForUser(userName, startDate, endDate, SR_TYPE_PROBLEM);
        }

        public List<ServiceRequest> GetIncidentsForUser(string userName, DateTime startDate, DateTime endDate)
        {
            return _serviceDeskRepository.GetServiceRequestsForUser(userName, startDate, endDate, SR_TYPE_INCIDENT);
        }

        public List<ServiceRequest> GetRequestsForUser(string userName, DateTime startDate, DateTime endDate)
        {
            return _serviceDeskRepository.GetServiceRequestsForUser(userName, startDate, endDate, SR_TYPE_REQUEST);
        }

        public int getClosedIncidentsCount(DateTime startDate, DateTime endDate, string userName)
        {
            return _serviceDeskRepository.getClosedIncidentsCount(startDate, endDate, userName);
        }

        public int getClosedProblemsCount(DateTime startDate, DateTime endDate, string userName)
        {
            return _serviceDeskRepository.getClosedProblemsCount(startDate, endDate, userName);
        }

        public int getClosedRequestsCount(DateTime startDate, DateTime endDate, string userName)
        {
            return _serviceDeskRepository.getClosedRequestsCount(startDate, endDate, userName);
        }

        public string getServiceProviderName()
        {
            return _serviceDeskRepository.getServiceDeskProviderName();
        }

        public int getTasksCount(DateTime startDate, DateTime endDate, string userName, int statusCode)
        {
            return _serviceDeskRepository.getTasksCount(startDate, endDate, userName, statusCode);
        }
    }
}
