using SysAid.Data.Infrastructure;
using SysAid.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Services.Abstract
{
    public interface IServiceDeskService
    {
        string getServiceProviderName();

        int getTasksCount(DateTime startDate, DateTime endDate, string userName, int statusCode);

        int getClosedRequestsCount(DateTime startDate, DateTime endDate, string userName);

        int getClosedIncidentsCount(DateTime startDate, DateTime endDate, string userName);

        int getClosedProblemsCount(DateTime startDate, DateTime endDate, string userName);

        List<ServiceRequest> GetRequestsForUser(string userName, DateTime startDate, DateTime endDate);

        List<ServiceRequest> GetIncidentsForUser(string userName, DateTime startDate, DateTime endDate);

        List<ServiceRequest> GetProblemsForUser(string userName, DateTime startDate, DateTime endDate);

        List<ProjectTask> GetProjectsTasksForUser(string userName, DateTime startDate, DateTime endDate);
    }
}
