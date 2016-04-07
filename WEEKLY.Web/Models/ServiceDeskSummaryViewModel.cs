using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEEKLY.Web.Models
{
    public class ServiceDeskSummaryViewModel
    {
        // MONTH
        public int MonthTotalCount { get; set; }

        public decimal MonthPercentageChange { get; set; }


        // THIS WEEK
        public int WeekTotalCount { get; set; }

        public decimal WeekPercentageChange { get; set; }

        // TODAY
        public int TodayTotalCount { get; set; }

        public decimal TodayPercentageChange { get; set; }

        // TASKS
        public int TaskCount { get; set; }

        public decimal TaskPercentChange { get; set; }

        // REQUESTS
        public int RequestCount { get; set; }

        public decimal RequestPercentChange { get; set; }


        // INCIDENTS
        public int IncidentCount { get; set; }

        public decimal IncidentPercentChange { get; set; }

        // PROBLEMS
        public int ProblemCount { get; set; }

        public decimal ProblemPercentChange { get; set; }

        public decimal CalculatePercentageChange(int firstVal, int secondVal)
        {
            decimal percentageChanged = 0;
            percentageChanged = firstVal - secondVal;

            //if (percentageChanged != 0)
            //{
            //    percentageChanged = secondVal == 0 ? 100 : (percentageChanged / secondVal) * 100;
            //}
                        
            return percentageChanged;
        }

        public int CalculateTotalsCount(int taskCnt, int requestCnt, int incidentCnt, int problemCnt)
        {
            int totalCnt = taskCnt + requestCnt + incidentCnt + problemCnt;
            return totalCnt;
        }
        
    }
}