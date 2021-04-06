using DSSGBOAdmin.Models.Entities;
using MyUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DSSGBOAdmin.Models.DAL
{
    public class DAL_Demande
    {
        public static Demande selectByField(string field, string valueSearch)
        {
            Demande demande = new Demande();
            long Id = 0;
            string StrSQL;
            SqlCommand command;
            if (field.Trim().ToLower().Equals("id"))
            {
                Id = long.Parse(valueSearch);
            }

            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    if (Id != 0)
                    {
                        StrSQL = "select * FROM RegistrationDemand where Id = @value";
                        command = new SqlCommand(StrSQL, connection);
                        command.Parameters.Add("@value", SqlDbType.BigInt).Value = Id;
                    }
                    else //search by antoher field
                    {
                        StrSQL = "select * FROM RegistrationDemand where " + field + " = @value";
                        command = new SqlCommand(StrSQL, connection);
                        command.Parameters.Add("@value", SqlDbType.VarChar).Value = valueSearch;
                    }
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        demande.ID = dataReader.GetInt64(0);
                        demande.Name = dataReader["Name"].ToString();
                        demande.Affiliation = dataReader["Affiliation"].ToString();
                        demande.FieldOfActivity = dataReader["FieldOfActivity"].ToString();
                        demande.Adress = dataReader["Adress"].ToString();
                        demande.PostalCode = dataReader["PostalCode"].ToString();
                        demande.City = dataReader["City"].ToString();
                        demande.Country = dataReader["Country"].ToString();
                        demande.Email = dataReader["Email"].ToString();
                        demande.Phone = dataReader["Phone"].ToString();
                        demande.PersonToContact = dataReader["PersonToContact"].ToString();
                        demande.ContactMail = dataReader["ContactMail"].ToString();
                        demande.ContactPhone = dataReader["ContactPhone"].ToString();
                        demande.ContactPosition = dataReader["ContactPosition"].ToString();
                        demande.RegDemandDate = dataReader["RegDemandDate"].ToString().Split(" ")[0];
                        demande.RegDemandDecision = dataReader["RegDemandDecision"].ToString();
                        demande.RegDemandDecisionDate = dataReader["RegDemandDecisionDate"].ToString().Split(" ")[0];
                        demande.RegDecisionComments = dataReader["RegDecisionComments"].ToString();
                    }
                    return demande;
                }
                catch (SqlException e)
                {
                    throw new MyException(e, "Database Error", e.Message, "DAL");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static void AddDemande(Demande newDemande)
        {
            if (selectByField("Name", newDemande.Name) != null || selectByField("Email", newDemande.Email) != null)
            {
                throw new MyException("Database Error", "Demande must be unique.", "DAL");
            }
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                string StrSQL = "insert into [RegistrationDemand]  " +
                    "([Name],[Affiliation],[FieldOfActivity],[Adress],[PostalCode],[City],[Country],[Email],[Phone],[PersonToContact],[ContactMail],[ContactPhone],[ContactPosition],[RegDemandDate],[RegDemandDecision],[RegDemandDecisionDate],[RegDecisionComments])" +
                                              "values  (" +
                    "@Name,@Affiliation,@FieldOfActivity,@Adress,@PostalCode,@City,@Country,@Email,@Phone,@PersonToContact,@ContactMail,@ContactPhone,@ContactPosition,@RegDemandDate,@RegDemandDecision,@RegDemandDecisionDate,@RegDecisionComments)";

                SqlCommand MySqlCommand = new SqlCommand(StrSQL, con);
                MySqlCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = newDemande.Name;
                MySqlCommand.Parameters.Add("@Affiliation", SqlDbType.VarChar).Value = newDemande.Affiliation ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@FieldOfActivity", SqlDbType.VarChar).Value = newDemande.FieldOfActivity ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@Adress", SqlDbType.VarChar).Value = newDemande.Adress ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@PostalCode", SqlDbType.VarChar).Value = newDemande.PostalCode ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@City", SqlDbType.VarChar).Value = newDemande.City ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@Country", SqlDbType.VarChar).Value = newDemande.Country ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = newDemande.Email ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@Phone", SqlDbType.VarChar).Value = newDemande.Phone ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@PersonToContact", SqlDbType.VarChar).Value = newDemande.PersonToContact ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@ContactMail", SqlDbType.VarChar).Value = newDemande.ContactMail ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@ContactPhone", SqlDbType.VarChar).Value = newDemande.ContactPhone ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@ContactPosition", SqlDbType.VarChar).Value = newDemande.ContactPosition ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@RegDemandDate", SqlDbType.Date).Value = newDemande.RegDemandDate ?? (object)DBNull.Value;
                MySqlCommand.Parameters.Add("@RegDemandDecision", SqlDbType.VarChar).Value = newDemande.RegDemandDecision ?? (object)DBNull.Value; ;
                MySqlCommand.Parameters.Add("@RegDemandDecisionDate", SqlDbType.Date).Value = newDemande.RegDemandDecisionDate == null ? (object)DBNull.Value : newDemande.RegDemandDecisionDate;
                MySqlCommand.Parameters.Add("@RegDecisionComments", SqlDbType.VarChar).Value = newDemande.RegDecisionComments ?? (object)DBNull.Value;
                DataBaseAccessUtilities.NonQueryRequest(MySqlCommand);

            }
        }

        public static void UpdateDemande(long Id, Demande newDemande)
        {
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {
                //string StrSQL = "update [RegistrationDemand] set " +
                //" Name = @Name , Affiliation = @Affiliation , FieldOfActivity = @FieldOfActivity , " +
                //" Adress = @Adress , PostalCode = @PostalCode , " +
                //" City = @City , Country = @Country , " +
                //" Email = @Email , Phone = @Phone , " +
                //" PersonToContact = @PersonToContact , " +
                //" ContactMail = @ContactMail , ContactPhone = @ContactPhone , ContactPosition = @ContactPosition , " +
                //" RegDemandDate = @RegDemandDate , RegDemandDecision = @RegDemandDecision , " +
                //" RegDemandDecisionDate = @RegDemandDecisionDate , RegDecisionComments = @RegDecisionComments " +
                //" where Id = @currId";
                string StrSQL = "update RegistrationDemand set" +
                " RegDemandDecision = @RegDemandDecision , " +
                " RegDemandDecisionDate = @RegDemandDecisionDate , RegDecisionComments = @RegDecisionComments " +
                " where Id = @currId";
                SqlCommand MySqlCommand = new SqlCommand(StrSQL, con);
                MySqlCommand.Parameters.Add("@currId", SqlDbType.BigInt).Value = Id;
                MySqlCommand.Parameters.Add("@RegDemandDecision", SqlDbType.VarChar).Value = newDemande.RegDemandDecision ?? (object)DBNull.Value; ;
                MySqlCommand.Parameters.Add("@RegDemandDecisionDate", SqlDbType.Date).Value = newDemande.RegDemandDecisionDate == null ? (object)DBNull.Value : newDemande.RegDemandDecisionDate;
                MySqlCommand.Parameters.Add("@RegDecisionComments", SqlDbType.VarChar).Value = newDemande.RegDecisionComments ?? (object)DBNull.Value;

                DataBaseAccessUtilities.NonQueryRequest(MySqlCommand);
            }

        }

        public static void DeleteDemande(long Id)
        {
            using (SqlConnection con = DBConnection.GetAuthConnection())
            {

                SqlCommand MySqlCommand = new SqlCommand("delete from [RegistrationDemand] where Id = @Id", con);
                MySqlCommand.Parameters.Add("@Id", SqlDbType.BigInt).Value = Id;
                DataBaseAccessUtilities.NonQueryRequest(MySqlCommand);

            }

        }

        //public static void DeleteAllData(out string messageError)
        //{
        //    try
        //    {
        //        messageError = null;
        //        using (SqlConnection con = DBConnection.GetAuthConnection())
        //        {

        //            MySqlCommand = new SqlCommand("delete from [RegistrationDemand]", con);

        //            DataBaseAccessUtilities.NonQueryRequest(MySqlCommand);
        //            if (messageError == null)
        //            {
        //                MySqlCommand = new SqlCommand("delete from [User]", con);

        //                DataBaseAccessUtilities.NonQueryRequest(MySqlCommand);

        //            }

        //            if (messageError == null)
        //            {
        //                MySqlCommand = new SqlCommand("delete from [Organization]", con);

        //                DataBaseAccessUtilities.NonQueryRequest(MySqlCommand);

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        messageError = ex.Message;
        //    }


        //}
        public static List<Demande> selectAll()
        {
            List<Demande> demandes = new List<Demande>();
            Demande demande;
            using (SqlConnection connection = DBConnection.GetAuthConnection())
            {
                try
                {
                    connection.Open();
                    string StrSQL = "SELECt * FROM RegistrationDemand";
                    SqlCommand command = new SqlCommand(StrSQL, connection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader != null)
                    {
                        while (dataReader.Read())
                        {
                            demande = new Demande();
                            demande.ID = dataReader.GetInt64(0);
                            demande.Name = dataReader["Name"].ToString();
                            demande.Affiliation = dataReader["Affiliation"].ToString();
                            demande.FieldOfActivity = dataReader["FieldOfActivity"].ToString();
                            demande.Adress = dataReader["Adress"].ToString();
                            demande.PostalCode = dataReader["PostalCode"].ToString();
                            demande.City = dataReader["City"].ToString();
                            demande.Country = dataReader["Country"].ToString();
                            demande.Email = dataReader["Email"].ToString();
                            demande.Phone = dataReader["Phone"].ToString();
                            demande.PersonToContact = dataReader["PersonToContact"].ToString();
                            demande.ContactMail = dataReader["ContactMail"].ToString();
                            demande.ContactPhone = dataReader["ContactPhone"].ToString();
                            demande.ContactPosition = dataReader["ContactPosition"].ToString();
                            demande.RegDemandDate = dataReader["RegDemandDate"].ToString().Split(" ")[0];
                            demande.RegDemandDecision = dataReader["RegDemandDecision"].ToString();
                            demande.RegDemandDecisionDate = dataReader["RegDemandDecisionDate"].ToString().Split(" ")[0];
                            demande.RegDecisionComments = dataReader["RegDecisionComments"].ToString();
                            demandes.Add(demande);
                        }
                    }
                    return demandes;
                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine("e.Message=" + e.Message);
                    throw new MyException(e, "Database Error", e.Message, "DAL");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
