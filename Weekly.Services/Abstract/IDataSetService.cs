using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Services.Abstract
{
    public interface IDataSetService
    {
        void EntityToDataSet<T>(DataSet ds, T entity, string tableName);

        void EntityToDataSet<T>(DataSet ds, List<T> entities, string tableName);
    }
}
