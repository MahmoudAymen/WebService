using AdminServiceGBO.Models.DAL;
using AdminServiceGBO.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServiceGBO.Models.BLL
{
    public class BLL_User
    {
        
        public static User loggingUser(string Email, string Password)
        {
            
            User userLogging = SelectByField("Email", Email);
            if(userLogging != null && userLogging.Id > 0 && userLogging.PassWord.Trim().Equals(Password.Trim()))
            {
                return userLogging;
            }
            return null;

        }
        public static bool CheckNameUnicityEmail(string Email, long IdOrganization)
        {
            return DAL_User.CheckNameUnicityEmail(Email, IdOrganization);
        }
        public static bool CheckNameUnicityName(string Name, long IdOrganization)
        {
            return DAL_User.CheckNameUnicityName(Name, IdOrganization);
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
        public static User SelectByField(string field, string value)
        {
            return DAL_User.SelectByField(field, value);
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
