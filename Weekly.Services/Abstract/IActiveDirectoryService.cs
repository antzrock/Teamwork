using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly.Services.Abstract
{
    public interface IActiveDirectoryService
    {
        string GetFullnameUsingEmail(string email);
        string GetTitleUsingEmail(string email);
    }
}
