using SysAid.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Services;

namespace WEEKLY.Console
{
    class Program
    {
        static void Main(string[] args)
        {


            ////ActiveDirectoryService adService = new ActiveDirectoryService();
            ////string email = "";
            ////System.Console.WriteLine("Enter Email: ");
            ////email = System.Console.ReadLine();
            ////string fullname = "";
            ////fullname = adService.GetFullnameUsingEmail(email);
            ////System.Console.WriteLine("Fullname: " + fullname);
            ////System.Console.ReadLine();

            //ServiceDeskRepository sdRepo = new ServiceDeskRepository();
            //ServiceDeskService sdService = new ServiceDeskService(sdRepo);

            //DateTime startDate = System.DateTime.Now;
            //DateTime endDate = System.DateTime.Now;

            //int TasksCount = sdService.getTasksCount(startDate, endDate, "PASARCORP\\ayap", 3);
            //int IncidentsCount = sdService.getClosedIncidentsCount(startDate, endDate, "PASARCORP\\ayap");
            //int RequestsCount = sdService.getClosedRequestsCount(startDate, endDate, "PASARCORP\\ayap");
            //int ProblemsCount = sdService.getClosedProblemsCount(startDate, endDate, "PASARCORP\\ayap");

            //DateTime startDateYesterday = System.DateTime.Now.AddDays(-1);
            //DateTime endDateYesterday = System.DateTime.Now.AddDays(-1);

            //int TasksYesterday = sdService.getTasksCount(startDateYesterday, endDateYesterday, "PASARCORP\\ayap", 3);
            //int IncidentsYesterday = sdService.getClosedIncidentsCount(startDateYesterday, endDateYesterday, "PASARCORP\\ayap");
            //int RequestsYesterday = sdService.getClosedRequestsCount(startDateYesterday, endDateYesterday, "PASARCORP\\ayap");
            //int ProblemsYesterday = sdService.getClosedProblemsCount(startDateYesterday, endDateYesterday, "PASARCORP\\ayap");

            ////((V2 - V1) / |V1|) * 100
            //decimal TasksPercentChange = TasksCount - TasksYesterday;
            //TasksPercentChange = TasksYesterday == 0 ? 100 : (TasksPercentChange / TasksYesterday) * 100;
            ////decimal IncidentsPercentChange = IncidentsYesterday == 0 ? 100 : ((IncidentsCount - IncidentsYesterday) / IncidentsYesterday) * 100;
            ////decimal RequestsPercentChange =  RequestsYesterday == 0 ? 100 :((RequestsCount - RequestsYesterday) / RequestsYesterday) * 100;
            ////decimal ProblemsPercentChange = ProblemsYesterday == 0 ? 100 :((ProblemsCount - ProblemsYesterday) / ProblemsYesterday) * 100;

            //System.Console.WriteLine("TASKS");
            //System.Console.WriteLine("===========================================");
            //System.Console.WriteLine("Today Tasks: " + TasksCount);
            //System.Console.WriteLine("Yesterday Tasks: " + TasksYesterday);
            //System.Console.WriteLine(TasksPercentChange <= 0 ? "Tasks down " + Math.Abs(TasksPercentChange).ToString("0.000") + "% from yesterday" : "Tasks up " + Math.Abs(TasksPercentChange).ToString("0.000") + "% from yesterday");

            ////System.Console.WriteLine("REQUESTS");
            ////System.Console.WriteLine("===========================================");
            ////System.Console.WriteLine("Today Requests: " + RequestsCount);


            ////System.Console.WriteLine("INCIDENTS");
            ////System.Console.WriteLine("===========================================");
            ////System.Console.WriteLine("Today Incidents: " + IncidentsCount);


            ////System.Console.WriteLine("PROBLEMS");
            ////System.Console.WriteLine("===========================================");
            ////System.Console.WriteLine("Today Problems: " + ProblemsCount);

            //System.Console.ReadLine();

        }
    }
}
