using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Entities;

namespace Weekly.Services.Abstract
{
    public interface IProductBacklogService
    {
        ProductBacklog GetProductBacklogForProject(int projectId, int userId);

        ProductBacklog InitializeProductBacklogForProject(int projectId, int userId);
    }
}
