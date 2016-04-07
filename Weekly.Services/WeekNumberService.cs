using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class WeekNumberService : IWeekNumberService
    {
        public const int REPORT_DAYS_OFFSET = 6, ONE_DAY = 1, DAYS_IN_A_WEEK = 7, THREE_DAYS = 3;

        public int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            //return new GregorianCalendar(GregorianCalendarTypes.Localized).GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public List<Week> GetWeekNumbersOfYear(string year, int reportDayOffset)
        {
            List<Week> WeekNumbers = new List<Week>();
            string date = "01/01/" + year;

            DateTime jan1 = Convert.ToDateTime(date);
            DateTime startOfFirstWeek = jan1.AddDays(ONE_DAY - (int)(jan1.DayOfWeek));

            var weeks =
              Enumerable
                .Range(0, 54)
                .Select(i => new
                {
                    weekStart = startOfFirstWeek.AddDays(i * DAYS_IN_A_WEEK)
                })
                .TakeWhile(x => x.weekStart.Year <= jan1.Year)
                .Select(x => new
                {
                    x.weekStart,
                    weekFinish = x.weekStart.AddDays(REPORT_DAYS_OFFSET)
                })
                .SkipWhile(x => x.weekFinish < jan1.AddDays(ONE_DAY))
                .Select((x, i) => new
                {
                    x.weekStart,
                    x.weekFinish,
                    weekNum = i + 1
                });

            foreach (var week in weeks.ToList())
            {
                Week newWeekNumber = new Week();
                newWeekNumber.WeekNo = (int)week.weekNum;
                newWeekNumber.StartDate = (DateTime)week.weekStart;
                newWeekNumber.EndDate = (DateTime)week.weekFinish;
                newWeekNumber.ReportDate = newWeekNumber.StartDate.AddDays(reportDayOffset);
                newWeekNumber.Year = Int32.Parse(year);
                newWeekNumber.CreatedDate = System.DateTime.Now;
                newWeekNumber.CreatedBy = 1;

                WeekNumbers.Add(newWeekNumber);
            }

            return WeekNumbers;
        }
    }
}
