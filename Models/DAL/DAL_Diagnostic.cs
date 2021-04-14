using MyUtilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AdminServicesGBO.Models.DAL
{
    public class DAL_Diagnostic
    {
        // Programme Diagonastic
        public static List<string> GetAllFilesDB()
        {
            List<string> Files = new List<string>();
            SqlConnection con = null;
            try
            {
                using (con = DBConnection.GetAuthConnection())
                {
                    con.Open();
                    string StrSQL = "SELECT DigitizedFile FROM Mail where DigitizedFile !=''";
                    SqlCommand cmd = new SqlCommand(StrSQL, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Files.Add(rdr["DigitizedFile"].ToString());
                    }
                }
            }
            catch (SqlException e)
            {
                throw new MyException(e, "Erreur de la base de données", e.Message, "DAL");
            }
            finally
            {
                con.Close();
            }

            return Files;
        }

        public static string Backup(string filePath)
        {
            SqlConnection con = null;
            string message = null;
            try
            {
                using (con = DBConnection.GetAuthConnection())
                {
                    con.Open();
                    string StrSQL = $"backup database [{con.Database}] to disk='" + filePath + "'";
                    SqlCommand cmd = new SqlCommand(StrSQL, con);
                    cmd.ExecuteNonQuery();
                    message = "Base de données sauvegardée avec succès";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return message;
        }

    }
}
