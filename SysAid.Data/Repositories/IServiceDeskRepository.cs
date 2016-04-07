using SysAid.Data.Infrastructure;
using SysAid.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAid.Data.Repositories
{
    public interface IServiceDeskRepository
    {
        string getServiceDeskProviderName();

        int getTasksCount(DateTime startDate, DateTime endDate, string userName, int statusCode);

        int getClosedRequestsCount(DateTime startDate, DateTime endDate, string userName);

        int getClosedIncidentsCount(DateTime startDate, DateTime endDate, string userName);

        int getClosedProblemsCount(DateTime startDate, DateTime endDate, string userName);

        List<ServiceRequest> GetServiceRequestsForUser(string userName, DateTime startDate, DateTime endDate, int sr_type);

        List<ProjectTask> GetProjectTasksForUser(string userName, DateTime startDate, DateTime endDate);
    }
}
