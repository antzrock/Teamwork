using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Services.Abstract
{
    public interface IWeekNumberService
    {
        List<Week> GetWeekNumbersOfYear(string year, int reportDayOffset);

        int GetIso8601WeekOfYear(DateTime time);
        
    }
}
