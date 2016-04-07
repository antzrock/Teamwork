using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        public string GetTitleUsingEmail(string email)
        {
            string AdTitle = "";

            DirectoryEntry entry = new DirectoryEntry("LDAP://PASAR.local");
            DirectorySearcher Dsearch = new DirectorySearcher(entry);
            Dsearch.Filter = "(&(objectClass=user)(mail=" + email + "))";

            foreach (SearchResult sResultSet in Dsearch.FindAll())
            {
                AdTitle = GetProperty(sResultSet, "title");
            }

            return AdTitle;
        }

        public string GetFullnameUsingEmail(string email)
        {
            string AdFullname = "";
            DirectoryEntry entry = new DirectoryEntry("LDAP://PASAR.local");
            DirectorySearcher Dsearch = new DirectorySearcher(entry);
            Dsearch.Filter = "(&(objectClass=user)(mail=" + email + "))";

            foreach (SearchResult sResultSet in Dsearch.FindAll())
            {
                AdFullname = GetProperty(sResultSet, "displayName");
            }

            return AdFullname;
        }

        private string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
