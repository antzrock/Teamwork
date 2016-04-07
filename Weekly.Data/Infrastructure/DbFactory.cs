using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        TeamworkContext dbContext;

        public TeamworkContext Init()
        {
            return dbContext ?? (dbContext = new TeamworkContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
