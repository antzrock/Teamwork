using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Entities
{
    public interface IEntityBase
    {
        int ID { get; set; }

        byte[] RowVersion { get; set; }
    }
}
