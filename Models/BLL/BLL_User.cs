using DSSGBOAdmin.Models.DAL;
using DSSGBOAdmin.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSSGBOAdmin.Models.BLL
{
    public class BLL_User
    {
        public static bool CheckEmailUnicity(string Email, long IdOrganization)
        {
            return DAL_User.CheckEmailUnicity(Email, IdOrganization);
        }
        public static bool CheckNameUnicity(string Name, long IdOrganization)
        {
            return DAL_User.CheckNameUnicity(Name, IdOrganization);
        }
        public static long Add(User user)
        {
            return DAL_User.Add(user);
        }
        public static void Update(long id, User user)
        {
            DAL_User.Update(id, user);
        }
        public static void Delete(long id)
        {
            DAL_User.Delete(id);
        }
        public static User SelectById(long id)
        {
            return DAL_User.SelectById(id);
        }
        public static List<User> SelectAll(long IdOrganization)
        {
            return DAL_User.SelectAll(IdOrganization);
        }
        public static List<User> TestConnexion(string UserName, string Password,out string message)
        {
            return DAL_User.TestConnexion(UserName,Password, out message);
        }
        public static List<User> RechercherCompteUser(string Email, out string message)
        {
            return DAL_User.RechercherCompteUser(Email, out message);
        }
    }
}
