using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class DataSetService : IDataSetService
    {
        public void EntityToDataSet<T>(DataSet ds, List<T> entities, string tableName)
        {
            foreach (var entity in entities)
            {
                var entityFields = entity.GetType().GetProperties().Where(a => a.CanRead);

                if (tableName == null)
                    tableName = entity.GetType().FullName;

                var drRow = ds.Tables[tableName].NewRow();

                foreach (var field in entityFields)
                {
                    if (drRow.Table.Columns.Contains(field.Name))
                        drRow[field.Name] = field.GetValue(entity, null);
                }

                ds.Tables[tableName].Rows.Add(drRow);
            }

        }

        public void EntityToDataSet<T>(DataSet ds, T entity, string tableName)
        {
            var entityFields = typeof(T).GetProperties().Where(a => a.CanRead);

            if (tableName == null)
                tableName = entity.GetType().FullName;

            var drRow = ds.Tables[tableName].NewRow();

            foreach (var field in entityFields)
            {
                if (drRow.Table.Columns.Contains(field.Name))
                    drRow[field.Name] = field.GetValue(entity, null);
            }

            ds.Tables[tableName].Rows.Add(drRow);
        }
    }
}
