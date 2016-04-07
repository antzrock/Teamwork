using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime getEndDate(DateTime endDate)
        {
            return new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
        }

        public DateTime getStartDate(DateTime startDate)
        {
            return new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
        }
    }
}
