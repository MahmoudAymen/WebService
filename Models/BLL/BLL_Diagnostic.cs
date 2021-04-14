using AdminServicesGBO.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminServicesGBO.Models.BLL
{
    public class BLL_Diagnostic
    {
        public static List<string> GetAllFilesDB()
        {
            return DAL_Diagnostic.GetAllFilesDB();
        }
        public static string Backup(string filePath)
        {
            return DAL_Diagnostic.Backup(filePath);
        }
    }
}
