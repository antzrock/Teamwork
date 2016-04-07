using SysAid.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using SysAid.Data.Infrastructure;

namespace SysAid.Data.Repositories
{
    public class ServiceDeskRepository : IServiceDeskRepository
    {
        private SysAidContext dataContext;
        private const string PROVIDER_NAME = "SysAid";
        private const int SR_TYPE_INCIDENT = 1;
        private const int SR_TYPE_REQUEST = 10;
        private const int SR_TYPE_PROBLEM = 6;

        public ServiceDeskRepository()
        {
            dataContext = new SysAidContext();
        }

        public string getServiceDeskProviderName()
        {
            return PROVIDER_NAME;
        }

        public List<ProjectTask> GetProjectTasksForUser(string userName, DateTime startDate, DateTime endDate)
        {
            List<ProjectTask> allTasks = (from pt in dataContext.tasks
                                          join proj in dataContext.projects on pt.project_id equals proj.id
                                          join taskCat in dataContext.cust_values on pt.category equals taskCat.value_key
                                          join taskStatus in dataContext.cust_values on pt.status equals taskStatus.value_key
                                          where taskCat.list_name == "taskCats" &&
                                                taskStatus.list_name == "taskStatuses" &&
                                                proj.manager == userName &&
                                                (pt.start_time >= startDate && pt.start_time <= endDate)
                                          select new ProjectTask()
                                          {
                                              id = pt.id,
                                              project_id = proj.id,
                                              project_title = proj.title,
                                              project_description = proj.description,
                                              project_manager = proj.manager,
                                              title = pt.title,
                                              description = pt.description,
                                              category = taskCat.value_key,
                                              category_str = taskCat.value_caption,
                                              status = taskStatus.value_key,
                                              status_str = taskStatus.value_caption,
                                              progress = pt.progress,
                                              notes = pt.notes,
                                              start_time = pt.start_time,
                                              end_time = pt.end_time,
                                              estimation = pt.estimation
                                          }).ToList();

            return allTasks;                                               
        }

        public List<ServiceRequest> GetServiceRequestsForUser(string userName, DateTime startDate, DateTime endDate, int sr_type)
        {
            List<ServiceRequest> completedRequests = (from sr in dataContext.service_req
                                                      join cust_value in dataContext.cust_values on sr.status equals cust_value.value_key
                                                      where cust_value.list_name == "status" &&
                                                            sr.sr_type == sr_type &&
                                                            sr.responsibility == userName &&
                                                            (sr.close_time >= startDate && sr.close_time <= endDate)
                                                      select new ServiceRequest()
                                                      {
                                                          id = sr.id,
                                                          title = sr.title,
                                                          description = sr.description,
                                                          status = sr.status,
                                                          status_str = cust_value.value_caption,
                                                          insert_time = sr.insert_time,
                                                          update_time = sr.update_time,
                                                          close_time = sr.close_time,
                                                          request_user = sr.request_user,
                                                          due_date = sr.due_date
                                                      }).ToList();


            List<ServiceRequest> createdRequests = (from sr in dataContext.service_req
                                                    join cust_value in dataContext.cust_values on sr.status equals cust_value.value_key
                                                    where cust_value.list_name == "status" &&
                                                          sr.sr_type == sr_type &&
                                                          sr.responsibility == userName &&
                                                          (sr.insert_time >= startDate && sr.insert_time <= endDate)
                                                    select new ServiceRequest()
                                                    {
                                                        id = sr.id,
                                                        title = sr.title,
                                                        description = sr.description,
                                                        status = sr.status,
                                                        status_str = cust_value.value_caption,
                                                        insert_time = sr.insert_time,
                                                        update_time = sr.update_time,
                                                        close_time = sr.close_time,
                                                        request_user = sr.request_user,
                                                        due_date = sr.due_date
                                                    }).ToList();

            List<ServiceRequest> allRequests = completedRequests.Union<ServiceRequest>(createdRequests, new ServiceRequestEqualityComparer()).ToList();


            return allRequests;
        }

        public int getTasksCount(DateTime startDate, DateTime endDate, string userName, int statusCode)
        {
            int count = 0;
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append("COUNT(*) AS TASKS_COUNT ");
            query.Append("FROM task tsk ");
            query.Append("JOIN project prj ON tsk.project_id = prj.id ");
            query.Append("WHERE ");
            query.Append("prj.manager = '{0}' ");
            query.Append("AND tsk.status = {1} ");
            query.Append("AND CAST(tsk.end_time AS DATE) >= CAST(CONVERT(datetime, '{2}', 101) AS DATE)");
            query.Append("AND CAST(tsk.end_time AS DATE) <= CAST(CONVERT(datetime, '{3}', 101) AS DATE) ");

            String dbQueryStr = String.Format(query.ToString(), userName, statusCode, startDate.ToString("MM/dd/yyy"), endDate.ToString("MM/dd/yyy"));
            count = dataContext.Database.SqlQuery<Int32>(dbQueryStr).SingleOrDefault<Int32>();
                                        
            return count;
        }

        private int getClosedServiceRequestCount(DateTime startDate, DateTime endDate, string userName, int sr_type)
        {
            int count = 0;
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append("COUNT(*) AS REQUESTS_COUNT ");
            query.Append("FROM service_req ");
            query.Append("WHERE ");
            query.Append("responsibility = '{0}' ");
            query.Append("AND sr_type = {1} ");
            query.Append("AND CAST(close_time AS DATE) >= CAST(CONVERT(datetime, '{2}', 101) AS DATE)");
            query.Append("AND CAST(close_time AS DATE) <= CAST(CONVERT(datetime, '{3}', 101) AS DATE) ");

            String dbQueryStr = String.Format(query.ToString(), userName, sr_type, startDate.ToString("MM/dd/yyy"), endDate.ToString("MM/dd/yyy"));
            count = dataContext.Database.SqlQuery<Int32>(dbQueryStr).SingleOrDefault<Int32>();

            return count;
        }

        public int getClosedRequestsCount(DateTime startDate, DateTime endDate, string userName)
        {
            return getClosedServiceRequestCount(startDate, endDate, userName, SR_TYPE_REQUEST);
        }

        public int getClosedIncidentsCount(DateTime startDate, DateTime endDate, string userName)
        {
            return getClosedServiceRequestCount(startDate, endDate, userName, SR_TYPE_INCIDENT);
        }

        public int getClosedProblemsCount(DateTime startDate, DateTime endDate, string userName)
        {
            return getClosedServiceRequestCount(startDate, endDate, userName, SR_TYPE_PROBLEM);
        }
    }
}
