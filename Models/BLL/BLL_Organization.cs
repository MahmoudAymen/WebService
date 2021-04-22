using DSSGBOAdmin.Models.DAL;
using DSSGBOAdmin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSSGBOAdmin.Models.BLL
{
    public class BLL_Organization
    {
        public static long Add(Organization organization)
        {
            return DAL_Organization.Add(organization);
        }
        public static void Update(long Id,Organization organization)
        {
            DAL_Organization.Update(Id,organization);
        }
        public static void Delete(long id)
        {
            DAL_Organization.Delete(id);
        }
        public static Organization SelectById(long id)
        {
            return DAL_Organization.SelectById(id);
        }
        public static List<Organization> SelectAll()
        {
            return DAL_Organization.SelectAll();
        }
    }
}
