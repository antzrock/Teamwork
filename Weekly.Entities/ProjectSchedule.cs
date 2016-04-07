using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public class ProjectSchedule : IEntityBase, IAuditable
    {
        public int ID { get; set; }

        public string Activity { get; set; }

        public DateTime PlannedStartDate { get; set; }

        public DateTime PlannedEndDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        //PROJECT
        public int ProjectID { get; set; }

        public virtual Project Project { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        // STATUS
        public int ProjectScheduleStatusID { get; set; }

        public ProjectScheduleStatus Status { get; set; }

        // ACTIVITY STATUS
        public int ProjectScheduleActivityStatusID { get; set; }
        public ProjectScheduleActivityStatus ActivityStatus { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
