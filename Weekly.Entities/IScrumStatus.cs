using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public interface IScrumStatus
    {
        DateTime? ToDoDate { get; set; }

        DateTime? InProgressDate { get; set; }

        DateTime? DoneDate { get; set; }
    }
}
