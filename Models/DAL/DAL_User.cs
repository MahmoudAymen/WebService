using System;
using System.Collections.Generic;
using System.Text;
using DSSGBOAdmin.Models.Entities;
using MyUtilities;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace DSSGBOAdmin.Models.DAL
{
    public class DAL_User
    {
        // insert user
        public static long Add(User user)
        {
            if (!CheckEntityUnicityName(user.Name,user.IdOrganization))
            {
                throw new MyException("Erreur de la base de données", "Le nom d'utilisateur doit être unique.", "DAL");
            }
            if(!CheckEntityUnicityEmail(user.Email, user.IdOrganization))
            {
                throw new MyException("Erreur de la base de données", "L'e-mail d'utilisateur doit être unique.", "DAL");
            }
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                string StrSQL = "INSERT INTO [User] (Name,Email,PassWord,IdOrganization,Role,AccountCreationDate) VALUES(@Name,@Email,@PassWord,@IdOrganization,@Role,@AccountCreationDate)";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@PassWord", SqlDbType.NVarChar).Value = ProtectPassword(user.PassWord);
                command.Parameters.Add("@IdOrganization", SqlDbType.BigInt).Value = user.IdOrganization;
                command.Parameters.Add("@Role", SqlDbType.NVarChar).Value = user.Role;
                command.Parameters.Add("@AccountCreationDate", SqlDbType.Date).Value = user.AccountCreationDate;
                return Convert.ToInt64(DataBaseAccessUtilities.ScalarRequest(command));
            }
        }
        // cryptage Password By RSA triple DES ALG
        static string ProtectPassword(string clearPassword)
        {
            //clearPassword = "GBOAdmin";
            byte[] bytes = Encoding.UTF8.GetBytes(clearPassword);
            byte[] protectedBytes = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            //string test = Convert.ToBase64String(protectedBytes);
            //System.Diagnostics.Debug.WriteLine("adduserhash=" + test);
            return Convert.ToBase64String(protectedBytes);
        }
        // Decryptage Password By RSA triple DES ALG
        static string UnprotectPassword(string protectedPassword)
        {
            //protectedPassword = "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAKyXeKmq/d0uEQU/YuOTLSQAAAAACAAAAAAAQZgAAAAEAACAAAAAcIVvrAum2N+96RlSy6uye6PykhxM/27vh8iVZO/fYpwAAAAAOgAAAAAIAACAAAAAkdmeBqT/dQeGqsoqD269WCY3b+fyS18s1KnW59j2VABAAAABHFhhwskaKkfwUrUeS9J3NQAAAAL+dlQJquC6k8eoO22sr+Whz56e7L8PHv3KRKUmM+MdImyQpF8R7WzPAIV5Ll7zrOlHw0gyIV10ix3mxXxeVgOg=";
            byte[] protectedBytes = Convert.FromBase64String(protectedPassword);
            byte[] bytes = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
            System.Diagnostics.Debug.WriteLine("adduserdecryptage=" + Encoding.UTF8.GetString(bytes));
            return Encoding.UTF8.GetString(bytes);
        }
        // update user
        public static void Update(long id, User user)
        {
            var olduser = SelectById(id);
            if (olduser.IdOrganization == user.IdOrganization && olduser.Name != user.Name)
            {
                if (!CheckEntityUnicityName(user.Name, user.IdOrganization))
                {
                    throw new MyException("Erreur de la base de données", "Le nom d'utilisateur doit être unique.", "DAL");
                }
            }
            if (olduser.IdOrganization == user.IdOrganization && olduser.Email != user.Email)
            {
                if (!CheckEntityUnicityEmail(user.Email, user.IdOrganization))
                {
                    throw new MyException("Erreur de la base de données", "L'e-mail d'utilisateur doit être unique.", "DAL");
                }
            }
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                string StrSQL = "UPDATE [User] SET Name=@Name,Email=@Email,PassWord=@PassWord,Role=@Role WHERE Id = @CurId";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.Add("@CurId", SqlDbType.BigInt).Value = id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@PassWord", SqlDbType.NVarChar).Value = ProtectPassword(user.PassWord);
                command.Parameters.Add("@Role", SqlDbType.NVarChar).Value = user.Role;
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }
        // delete user
        public static void Delete(long id)
        {
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                string StrSQL = "DELETE FROM [User] WHERE Role !='Administrateur' AND Id=" + id;
                SqlCommand command = new SqlCommand(StrSQL, con);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }
        // select one record of table user
        public static User SelectById(long id)
        {
            User user = new User();

            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    string StrSQL = "SELECT * FROM [User] WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        user.Id = Convert.ToInt64(dataReader["Id"]);
                        user.Name = dataReader["Name"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.PassWord = UnprotectPassword(dataReader["PassWord"].ToString());
                        user.Role = dataReader["Role"].ToString();
                        user.IdOrganization = Convert.ToInt64(dataReader["IdOrganization"]);
                    }
                }
                catch (SqlException e)
                {
                    throw new MyException(e, "Database Error", e.Message, "DAL");
                }
                finally
                {
                    connection.Close();
                }
                return user;
            }
        }
        // select all record of table user
        public static List<User> SelectAll(long IdOrganization)
        {
            List<User> Users = new List<User>();
            User user;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    string StrSQL = "SELECT * FROM [User] WHERE IdOrganization=@IdOrganization ORDER BY Id DESC";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    command.Parameters.Add("@IdOrganization", SqlDbType.BigInt).Value = IdOrganization;
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user = new User();
                        user.Id = Convert.ToInt64(dataReader["Id"]);
                        user.Name = dataReader["Name"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.PassWord = UnprotectPassword(dataReader["PassWord"].ToString());
                        user.Role = dataReader["Role"].ToString();
                        user.IdOrganization = Convert.ToInt64(dataReader["IdOrganization"]);
                        Users.Add(user);
                    }
                }
                catch (SqlException e)
                {
                    throw new MyException(e, "Database Error", e.Message, "DAL");
                }
                finally
                {
                    connection.Close();
                }
                return Users;
            }
        }
        public static List<User> TestConnexion(string UserName,string Password, out string message)
        {
            User user;
            List<User> users = new List<User>();
            bool testconnection = false;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    string StrSQL = "SELECT [User].Id,[User].Name,[User].Email,[User].Role,[User].IdOrganization,Organization.NameFr,Organization.OrganisationLogo,Organization.AffiliationLogo,Organization.AffiliationLogo,Organization.ParDiffusionEmail,Organization.ParDiffusionEmailPW,PassWord,OrganizationSystemPrefix FROM [User],Organization WHERE Organization.Id=[User].IdOrganization AND Name=@Name";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = UserName;
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string pass = UnprotectPassword(dataReader["PassWord"].ToString());
                        if (pass == Password)
                        {
                            user = new User();
                            user.Id = Convert.ToInt64(dataReader["Id"]);
                            user.Name = dataReader["Name"].ToString();
                            user.Email = dataReader["Email"].ToString();
                            user.PassWord = dataReader["NameFr"].ToString();
                            user.Role = dataReader["Role"].ToString();
                            user.IdOrganization = Convert.ToInt64(dataReader["IdOrganization"]);
                            user.OrganisationLogoUser = dataReader["OrganisationLogo"].ToString();
                            user.AffiliationLogoUser = dataReader["AffiliationLogo"].ToString();
                            user.EmailOrganisation = dataReader["ParDiffusionEmail"].ToString();
                            user.PasswordOrganisation = dataReader["ParDiffusionEmailPW"].ToString();
                            user.OrganizationSystemPrefix = dataReader["OrganizationSystemPrefix"].ToString();
                            users.Add(user);
                            testconnection = true;
                            //System.Diagnostics.Debug.WriteLine("user.OrganizationSystemPrefix="+ user.OrganizationSystemPrefix);
                        }
                        
                    }
                    if (testconnection)
                    {
                        message = "Connexion réussie";
                    }
                    else
                    {
                        message = "Echec de la connexion! Veuillez vérifier vos détails.";
                    }
                }
                catch (SqlException e)
                {
                    message = e.Message;
                    System.Diagnostics.Debug.WriteLine("message10=" + message);
                    new MyException(e, "Database Error", message, "DAL");     
                }
                finally
                {
                    connection.Close();
                }
                return users;
            }

        }

        // Reset Password and Test Compte User
        public static List<User> RechercherCompteUser(string Email, out string message)
        {
            User user;
            List<User> users = new List<User>();
            bool testconnection = false;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    string StrSQL = "SELECT [User].Id,[User].Name,[User].PassWord,[User].Role,[User].IdOrganization,Organization.NameFr FROM [User],Organization WHERE Organization.Id=[User].IdOrganization AND [User].Email=@Email";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user = new User();
                        user.Id = Convert.ToInt64(dataReader["Id"]);
                        user.Name = dataReader["Name"].ToString();
                        user.Email = dataReader["NameFr"].ToString();
                        user.PassWord = UnprotectPassword(dataReader["PassWord"].ToString());
                        user.Role = dataReader["Role"].ToString();
                        user.IdOrganization = Convert.ToInt64(dataReader["IdOrganization"]);
                        users.Add(user);
                        testconnection = true;
                    }
                    if (testconnection)
                    {
                        message = "Vérifier votre boîte mail pour récupérer le mot de passe et nom d'utilisateur.";
                    }
                    else
                    {
                        message = "Aucun résultat de recherche.<br/>Votre recherche ne donne aucun résultat. Veuillez réessayer avec d’autres informations.";
                    }
                }
                catch (SqlException e)
                {
                    message = e.Message;
                    new MyException(e, "Database Error", e.Message, "DAL");
                }
                finally
                {
                    connection.Close();
                }
                return users;
            }
        }
        // Test unicity Email
        private static bool CheckEntityUnicityEmail(string Email,long IdOrganization)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE Email = @Email AND IdOrganization = @IdOrganization";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
                command.Parameters.Add("@IdOrganization", SqlDbType.BigInt).Value = IdOrganization;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }
        public static bool CheckEmailUnicity(string Email,long IdOrganization)
        {
            return CheckEntityUnicityEmail(Email, IdOrganization);
        }
        // Test unicity Name
        private static bool CheckEntityUnicityName(string Name, long IdOrganization)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE Name = @Name AND IdOrganization = @IdOrganization";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
                command.Parameters.Add("@IdOrganization", SqlDbType.BigInt).Value = IdOrganization;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }
        public static bool CheckNameUnicity(string Name, long IdOrganization)
        {
            return CheckEntityUnicityName(Name, IdOrganization);
        }
    }
}
