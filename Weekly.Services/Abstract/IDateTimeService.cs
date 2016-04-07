using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Services.Abstract
{
    public interface IDateTimeService
    {
        DateTime getStartDate(DateTime startDate);
        DateTime getEndDate(DateTime endDate);
    }
}
