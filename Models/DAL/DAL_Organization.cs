using DSSGBOAdmin.Models.Entities;
using MyUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DSSGBOAdmin.Models.DAL
{
    public class DAL_Organization
    {
        // test sur l'uncite 
        private static bool CheckEntityUnicityName(string Name)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                string query = "SELECT COUNT(*) FROM Organization WHERE NameFr = @NameFr";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@NameFr", SqlDbType.NVarChar).Value = Name;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }
        // test sur l'uncite 
        private static bool CheckEntityUnicityEmail(string Email)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                string query = "SELECT COUNT(*) FROM Organization WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }
        // test sur l'uncite 
        private static bool CheckEntityUnicityAcronym(string Acronym)
        {
            int ocurrencesNumber = 0;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                string query = "SELECT COUNT(*) FROM Organization WHERE Acronym = @Acronym";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@Acronym", SqlDbType.NVarChar).Value = Acronym;
                ocurrencesNumber = (int)DataBaseAccessUtilities.ScalarRequest(command);
            }

            if (ocurrencesNumber == 0)
                return true;
            else
                return false;
        }
        // Test sur l'uncite Name
        public static bool CheckNameUnicity(string name)
        {
            return CheckEntityUnicityName(name);
        }
        // Test sur l'uncite Email
        public static bool CheckEmailUnicity(string email)
        {
            return CheckEntityUnicityEmail(email);
        }
        // Test sur l'uncite Acronym
        public static bool CheckAcronymUnicity(string acronym)
        {
            return CheckEntityUnicityAcronym(acronym);
        }
        // insert Organization
        public static long Add(Organization organization)
        {
            if (!CheckEntityUnicityName(organization.NameFr))
            {
                throw new MyException("Erreur de la base de données", "Le nom d'organisation doit être unique.", "DAL");
            }
            if (!CheckEntityUnicityEmail(organization.Email))
            {
                throw new MyException("Erreur de la base de données", "L'e-mail d'organisation doit être unique.", "DAL");
            }
            if (!CheckEntityUnicityAcronym(organization.Acronym))
            {
                throw new MyException("Erreur de la base de données", "L'acronyme d'organisation doit être unique.", "DAL");
            }
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                string StrSQL = "INSERT INTO Organization (NameFr,NameAr,Acronym,OrganisationLogo,Affiliation,AffiliationLogo,FieldOfActivity,Adress," +
                    "PostalCode,City,Country,Email,Phone,PersonToContact,ContactMail,ContactPhone,ContactPosition," +
                    "ParDiffusionEmail,ParDiffusionEmailPW,ParOutgoingMailChar,ParIngoingMailChar," +
                    "OrganizationSystemPrefix)" +
                    " output INSERTED.ID " +
                    " VALUES(@NameFr,@NameAr,@Acronym,@OrganisationLogo,@Affiliation,@AffiliationLogo," +
                    "@FieldOfActivity,@Adress,@PostalCode,@City,@Country,@Email,@Phone," +
                    "@PersonToContact,@ContactMail,@ContactPhone,@ContactPosition," +
                    "@ParDiffusionEmail,@ParDiffusionEmailPW,@ParOutgoingMailChar,@ParIngoingMailChar," +
                    "@OrganizationSystemPrefix)";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.Add("@NameFr", SqlDbType.NVarChar).Value = organization.NameFr ?? (object)DBNull.Value;
                command.Parameters.Add("@NameAr", SqlDbType.NVarChar).Value = organization.NameAr ?? (object)DBNull.Value;
                command.Parameters.Add("@Acronym", SqlDbType.NVarChar).Value = organization.Acronym ?? (object)DBNull.Value;
                command.Parameters.Add("@OrganisationLogo", SqlDbType.NVarChar).Value = organization.OrganisationLogo ?? (object)DBNull.Value;
                command.Parameters.Add("@Affiliation", SqlDbType.NVarChar).Value = organization.Affiliation ?? (object)DBNull.Value;
                command.Parameters.Add("@AffiliationLogo", SqlDbType.NVarChar).Value = organization.AffiliationLogo ?? (object)DBNull.Value;
                command.Parameters.Add("@FieldOfActivity", SqlDbType.NVarChar).Value = organization.FieldOfActivity ?? (object)DBNull.Value;
                command.Parameters.Add("@Adress", SqlDbType.NVarChar).Value = organization.Adress ?? (object)DBNull.Value;
                command.Parameters.Add("@PostalCode", SqlDbType.NVarChar).Value = organization.PostalCode ?? (object)DBNull.Value;
                command.Parameters.Add("@City", SqlDbType.NVarChar).Value = organization.City ?? (object)DBNull.Value; ;
                command.Parameters.Add("@Country", SqlDbType.NVarChar).Value = organization.Country ?? (object)DBNull.Value;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = organization.Email ?? (object)DBNull.Value;
                command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = organization.Phone ?? (object)DBNull.Value;
                command.Parameters.Add("@PersonToContact", SqlDbType.NVarChar).Value = organization.PersonToContact ?? (object)DBNull.Value;
                command.Parameters.Add("@ContactMail", SqlDbType.NVarChar).Value = organization.ContactMail ?? (object)DBNull.Value;
                command.Parameters.Add("@ContactPhone", SqlDbType.NVarChar).Value = organization.ContactPhone ?? (object)DBNull.Value;
                command.Parameters.Add("@ContactPosition", SqlDbType.NVarChar).Value = organization.ContactPosition ?? (object)DBNull.Value;
                command.Parameters.Add("@ParDiffusionEmail", SqlDbType.NVarChar).Value = organization.ParDiffusionEmail ?? (object)DBNull.Value;
                command.Parameters.Add("@ParDiffusionEmailPW", SqlDbType.NVarChar).Value = organization.ParDiffusionEmailPW ?? (object)DBNull.Value;
                command.Parameters.Add("@ParOutgoingMailChar", SqlDbType.NVarChar).Value = organization.ParOutgoingMailChar ?? (object)DBNull.Value;
                command.Parameters.Add("@ParIngoingMailChar", SqlDbType.NVarChar).Value = organization.ParIngoingMailChar ?? (object)DBNull.Value;
                command.Parameters.Add("@OrganizationSystemPrefix", SqlDbType.NVarChar).Value = organization.OrganizationSystemPrefix ?? (object)DBNull.Value;
                return Convert.ToInt64(DataBaseAccessUtilities.ScalarRequest(command));
            }
        }
        // update organisation
        public static void Update(long id, Organization organization)
        {
            var oldOrganization = SelectById(id);
            if (oldOrganization.NameFr != organization.NameFr)
            {
                if (!CheckEntityUnicityName(organization.NameFr))
                {
                    throw new MyException("Erreur de la base de données", "Le nom d'organisation doit être unique.", "DAL");
                }
            }
            if (oldOrganization.Email != organization.Email)
            {
                if (!CheckEntityUnicityEmail(organization.Email))
                {
                    throw new MyException("Erreur de la base de données", "L'e-mail d'organisation doit être unique.", "DAL");
                }
            }
            if (oldOrganization.Acronym != organization.Acronym)
            {
                if (!CheckEntityUnicityAcronym(organization.Acronym))
                {
                    throw new MyException("Erreur de la base de données", "L'acronyme d'organisation doit être unique.", "DAL");
                }
            }
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                string StrSQL = "UPDATE Organization SET NameFr=@NameFr,NameAr=@NameAr,Acronym=@Acronym,OrganisationLogo=@OrganisationLogo,Affiliation=@Affiliation,AffiliationLogo=@AffiliationLogo,FieldOfActivity=@FieldOfActivity,Adress=@Adress,PostalCode=@PostalCode,City=@City,Country=@Country,Email=@Email,Phone=@Phone,PersonToContact=@PersonToContact,ContactMail=@ContactMail,ContactPhone=@ContactPhone,ContactPosition=@ContactPosition,ParDiffusionEmail=@ParDiffusionEmail,ParDiffusionEmailPW=@ParDiffusionEmailPW,ParOutgoingMailChar=@ParOutgoingMailChar,ParIngoingMailChar=@ParIngoingMailChar WHERE Id = @CurId";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.Add("@CurId", SqlDbType.BigInt).Value = id;
                command.Parameters.Add("@NameFr", SqlDbType.NVarChar).Value = organization.NameFr ?? (object)DBNull.Value;
                command.Parameters.Add("@NameAr", SqlDbType.NVarChar).Value = organization.NameAr ?? (object)DBNull.Value;
                command.Parameters.Add("@Acronym", SqlDbType.NVarChar).Value = organization.Acronym ?? (object)DBNull.Value;
                command.Parameters.Add("@OrganisationLogo", SqlDbType.NVarChar).Value = organization.OrganisationLogo ?? (object)DBNull.Value;
                command.Parameters.Add("@Affiliation", SqlDbType.NVarChar).Value = organization.Affiliation ?? (object)DBNull.Value;
                command.Parameters.Add("@AffiliationLogo", SqlDbType.NVarChar).Value = organization.AffiliationLogo ?? (object)DBNull.Value;
                command.Parameters.Add("@FieldOfActivity", SqlDbType.NVarChar).Value = organization.FieldOfActivity ?? (object)DBNull.Value;
                command.Parameters.Add("@Adress", SqlDbType.NVarChar).Value = organization.Adress ?? (object)DBNull.Value;
                command.Parameters.Add("@PostalCode", SqlDbType.NVarChar).Value = organization.PostalCode ?? (object)DBNull.Value;
                command.Parameters.Add("@City", SqlDbType.NVarChar).Value = organization.City ?? (object)DBNull.Value; ;
                command.Parameters.Add("@Country", SqlDbType.NVarChar).Value = organization.Country ?? (object)DBNull.Value;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = organization.Email ?? (object)DBNull.Value;
                command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = organization.Phone ?? (object)DBNull.Value;
                command.Parameters.Add("@PersonToContact", SqlDbType.NVarChar).Value = organization.PersonToContact ?? (object)DBNull.Value;
                command.Parameters.Add("@ContactMail", SqlDbType.NVarChar).Value = organization.ContactMail ?? (object)DBNull.Value;
                command.Parameters.Add("@ContactPhone", SqlDbType.NVarChar).Value = organization.ContactPhone ?? (object)DBNull.Value;
                command.Parameters.Add("@ContactPosition", SqlDbType.NVarChar).Value = organization.ContactPosition ?? (object)DBNull.Value;
                command.Parameters.Add("@ParDiffusionEmail", SqlDbType.NVarChar).Value = organization.ParDiffusionEmail ?? (object)DBNull.Value;
                command.Parameters.Add("@ParDiffusionEmailPW", SqlDbType.NVarChar).Value = organization.ParDiffusionEmailPW ?? (object)DBNull.Value;
                command.Parameters.Add("@ParOutgoingMailChar", SqlDbType.NVarChar).Value = organization.ParOutgoingMailChar ?? (object)DBNull.Value;
                command.Parameters.Add("@ParIngoingMailChar", SqlDbType.NVarChar).Value = organization.ParIngoingMailChar ?? (object)DBNull.Value;
                //command.Parameters.Add("@AccountStatus", SqlDbType.NVarChar).Value = organization.AccountStatus ?? (object)DBNull.Value;
                //command.Parameters.Add("@AccountType", SqlDbType.NVarChar).Value = organization.AccountType ?? (object)DBNull.Value;
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }
        // delete Organization
        public static void Delete(long id)
        {
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                string StrSQL = "DELETE FROM Organization WHERE Id=" + id;
                SqlCommand command = new SqlCommand(StrSQL, con);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }
        // select one record of table user
        public static Organization SelectById(long id)
        {
            Organization organization = new Organization();

            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    string StrSQL = "SELECT * FROM Organization WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        organization.Id = dataReader.GetInt64(0);
                        organization.NameFr = dataReader["NameFr"].ToString();
                        organization.NameAr = dataReader["NameAr"].ToString();
                        organization.Acronym = dataReader["Acronym"].ToString();
                        organization.OrganisationLogo = dataReader["OrganisationLogo"].ToString();
                        organization.Affiliation = dataReader["Affiliation"].ToString();
                        organization.AffiliationLogo = dataReader["AffiliationLogo"].ToString();
                        organization.FieldOfActivity = dataReader["FieldOfActivity"].ToString();
                        organization.Adress = dataReader["Adress"].ToString();
                        organization.PostalCode = dataReader["PostalCode"].ToString();
                        organization.City = dataReader["City"].ToString();
                        organization.Country = dataReader["Country"].ToString();
                        organization.Email = dataReader["Email"].ToString();
                        organization.Phone = dataReader["Phone"].ToString();
                        organization.PersonToContact = dataReader["PersonToContact"].ToString();
                        organization.ContactMail = dataReader["ContactMail"].ToString();
                        organization.ContactPhone = dataReader["ContactPhone"].ToString();
                        organization.ContactPosition = dataReader["ContactPosition"].ToString();
                        organization.ParDiffusionEmail = dataReader["ParDiffusionEmail"].ToString();
                        organization.ParDiffusionEmailPW = dataReader["ParDiffusionEmailPW"].ToString();
                        organization.ParOutgoingMailChar = dataReader["ParOutgoingMailChar"].ToString();
                        organization.ParIngoingMailChar = dataReader["ParIngoingMailChar"].ToString();
                        organization.AccountStatus = dataReader["AccountStatus"].ToString();
                        organization.AccountType = dataReader["AccountType"].ToString();
                        organization.OrganizationSystemPrefix = dataReader["OrganizationSystemPrefix"].ToString();
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
                return organization;
            }
        }
        // select all record of table Organization
        public static List<Organization> SelectAll()
        {
            List<Organization> Organizations = new List<Organization>();
            Organization organization;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    string StrSQL = "SELECT * FROM Organization ORDER BY Id DESC";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        organization = new Organization();
                        organization.Id = dataReader.GetInt64(0);
                        organization.NameFr = dataReader["NameFr"].ToString();
                        organization.NameAr = dataReader["NameAr"].ToString();
                        organization.Acronym = dataReader["Acronym"].ToString();
                        organization.OrganisationLogo = dataReader["OrganisationLogo"].ToString();
                        organization.Affiliation = dataReader["Affiliation"].ToString();
                        organization.AffiliationLogo = dataReader["AffiliationLogo"].ToString();
                        organization.FieldOfActivity = dataReader["FieldOfActivity"].ToString();
                        organization.Adress = dataReader["Adress"].ToString();
                        organization.PostalCode = dataReader["PostalCode"].ToString();
                        organization.City = dataReader["City"].ToString();
                        organization.Country = dataReader["Country"].ToString();
                        organization.Email = dataReader["Email"].ToString();
                        organization.Phone = dataReader["Phone"].ToString();
                        organization.PersonToContact = dataReader["PersonToContact"].ToString();
                        organization.ContactMail = dataReader["ContactMail"].ToString();
                        organization.ContactPhone = dataReader["ContactPhone"].ToString();
                        organization.ContactPosition = dataReader["ContactPosition"].ToString();
                        organization.ParDiffusionEmail = dataReader["ParDiffusionEmail"].ToString();
                        organization.ParDiffusionEmailPW = dataReader["ParDiffusionEmailPW"].ToString();
                        organization.ParOutgoingMailChar = dataReader["ParOutgoingMailChar"].ToString();
                        organization.ParIngoingMailChar = dataReader["ParIngoingMailChar"].ToString();
                        organization.AccountStatus = dataReader["AccountStatus"].ToString();
                        organization.AccountType = dataReader["AccountType"].ToString();
                        organization.OrganizationSystemPrefix = dataReader["OrganizationSystemPrefix"].ToString();
                        Organizations.Add(organization);
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
                return Organizations;
            }
        }
    }
}
