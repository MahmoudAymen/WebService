using System;
using System.Collections.Generic;
using System.Text;
using AdminServiceGBO.Models.Entities;
using MyUtilities;
using System.Data;
using System.Data.SqlClient;

namespace AdminServiceGBO.Models.DAL
{
    public class DAL_User
    {
        // insert user
        public static long Add(User user)
        {
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                string StrSQL = "INSERT INTO [User] (Name,Email,PassWord,IdOrganization,Role,AccountCreationDate) " +
                    " output INSERTED.ID " +
                    " VALUES(@Name,@Email,@PassWord,@IdOrganization,@Role,@AccountCreationDate)";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@PassWord", SqlDbType.NVarChar).Value = user.PassWord;
                command.Parameters.Add("@IdOrganization", SqlDbType.BigInt).Value = user.IdOrganization;
                command.Parameters.Add("@Role", SqlDbType.NVarChar).Value = user.Role;
                command.Parameters.Add("@AccountCreationDate", SqlDbType.Date).Value = user.AccountCreationDate;
                return Convert.ToInt64(DataBaseAccessUtilities.ScalarRequest(command));
            }
        }
        // update user
        public static void Update(long id, User user)
        {
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                string StrSQL = "UPDATE [User] SET Name=@Name,Email=@Email,PassWord=@PassWord,Role=@Role WHERE Id = @CurId";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.Add("@CurId", SqlDbType.BigInt).Value = id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@PassWord", SqlDbType.NVarChar).Value = user.PassWord;
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
        public static User SelectByField(string Field, string Value)
        {
            User user = null;

            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    string StrSQL = "SELECT * FROM [User] WHERE "+Field+" = @Value";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    command.Parameters.Add("@Value", SqlDbType.VarChar).Value = Value;
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        user = new User();
                        user.Id = dataReader.GetInt64(0);
                        user.IdOrganization = long.Parse(dataReader["IdOrganization"].ToString());
                        user.Name = dataReader["Name"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.PassWord = dataReader["PassWord"].ToString();
                        user.Role = dataReader["Role"].ToString();
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
                        user.Id = dataReader.GetInt64(0);
                        user.IdOrganization = long.Parse(dataReader["IdOrganization"].ToString());
                        user.Name = dataReader["Name"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.PassWord = dataReader["PassWord"].ToString();
                        user.Role = dataReader["Role"].ToString();
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
                        user.Id = dataReader.GetInt64(0);
                        user.IdOrganization = long.Parse(dataReader["IdOrganization"].ToString());
                        user.Name = dataReader["Name"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.PassWord = dataReader["PassWord"].ToString();
                        user.Role = dataReader["Role"].ToString();
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
        public static List<User> TestConnexion(String UserName,string Password, out string message)
        {
            User user;
            List<User> users = new List<User>();
            bool testconnection = false;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    string StrSQL = "SELECT [User].Id,[User].Name,[User].Email,[User].Role,[User].IdOrganization,Organization.NameFr,Organization.OrganisationLogo,Organization.AffiliationLogo,Organization.AffiliationLogo,Organization.ParDiffusionEmail,Organization.ParDiffusionEmailPW FROM [User],Organization WHERE Organization.Id=[User].IdOrganization AND Name=@Name AND PassWord=@PassWord";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = UserName;
                    command.Parameters.Add("@PassWord", SqlDbType.NVarChar).Value = Password;
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        user = new User();
                        user.Id = dataReader.GetInt64(0);
                        user.IdOrganization = long.Parse(dataReader["IdOrganization"].ToString());
                        user.Name = dataReader["Name"].ToString();
                        user.Email = dataReader["Email"].ToString();
                        user.PassWord = dataReader["NameFr"].ToString();
                        user.Role = dataReader["Role"].ToString();
                        user.OrganisationLogoUser = dataReader["OrganisationLogo"].ToString();
                        user.AffiliationLogoUser = dataReader["AffiliationLogo"].ToString();
                        user.EmailOrganisation = dataReader["ParDiffusionEmail"].ToString();
                        user.PasswordOrganisation = dataReader["ParDiffusionEmailPW"].ToString();
                        System.Diagnostics.Debug.WriteLine("EmailOrganisation=" + user.EmailOrganisation);
                        System.Diagnostics.Debug.WriteLine("PasswordOrganisation=" + user.PasswordOrganisation);
                        users.Add(user);
                        testconnection = true;
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
                    new MyException(e, "Database Error", e.Message, "DAL");     
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
                        user.Id = dataReader.GetInt64(0);
                        user.IdOrganization = long.Parse(dataReader["IdOrganization"].ToString());
                        user.Name = dataReader["Name"].ToString();
                        user.Email = dataReader["NameFr"].ToString();
                        user.PassWord = dataReader["PassWord"].ToString();
                        user.Role = dataReader["Role"].ToString();
                        System.Diagnostics.Debug.WriteLine("IdOrganization=" + user.IdOrganization);
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
                    System.Diagnostics.Debug.WriteLine("message=" + message);
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
        public static bool CheckNameUnicityEmail(string Email,long IdOrganization)
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
        public static bool CheckNameUnicityName(string Name, long IdOrganization)
        {
            return CheckEntityUnicityName(Name, IdOrganization);
        }
    }
}
