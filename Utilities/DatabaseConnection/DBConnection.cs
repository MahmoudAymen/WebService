using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Security.Principal;
using System.Security.AccessControl;

public static class DBConnection
{
    //static string DbCnnStrAuth = @"Persist Security Info=False;User ID=Aymen;Password=Aymen;Initial Catalog=AuthDB;Data Source=WIN-P0NOM2NRLEU\SQLEXPRESS";
    static string DbCnnStrAuth = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AuthDB;Data Source=DESKTOP-KSGLTG1";
    public static SqlConnection GetAuthConnection()
    {
        return new SqlConnection(DbCnnStrAuth);
    }
    // Dynamic data base GBO
    //static string DbCnnStr = @"Persist Security Info=False;User ID=Aymen;Password=Aymen;Data Source=WIN-P0NOM2NRLEU\SQLEXPRESS";
    static string DbCnnStr = "Integrated Security=SSPI;Data Source=DESKTOP-KSGLTG1";
    public static string CreateDatabase(string dbName)
    {
        string message = null;
        string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        string connectionString = DbCnnStr + ";";
        SqlConnection connection = null;
        try
        {
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                System.Diagnostics.Debug.WriteLine("connectionString=" + connectionString);
                GrantAccess(appPath); //Need to assign the permission for current application to allow create database on server (if you are in domain).
                bool IsExits = CheckDatabaseExists(connection, dbName); //Check database exists in sql server.
                System.Diagnostics.Debug.WriteLine("IsExits=" + IsExits);
                if (!IsExits)
                {
                    return CreateTableDB(connection, dbName);
                }
                else
                    message = "Base de données existe déjà";
            }

        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        finally
        {
            System.Diagnostics.Debug.WriteLine("create10");
            if (connection != null)
                connection.Close();
        }
        return message;
    }

    public static string CreateTableDB(SqlConnection connection, string DBName)
    {
        string message = null;
        //        string connectionString = DbCnnStr + $";Initial Catalog={DBName}";
        System.Diagnostics.Debug.WriteLine("create1");
        try
        {
            System.Diagnostics.Debug.WriteLine("create2");
            var scriptfile = Path.Combine(Directory.GetCurrentDirectory(), "Utilities", "DatabaseConnection", "script.sql");
            System.Diagnostics.Debug.WriteLine("create3=" + scriptfile);
            FileInfo file = new FileInfo(scriptfile);
            System.Diagnostics.Debug.WriteLine("create4");
            string script = File.ReadAllText(file.FullName);
            script = script.Replace("NameDB", DBName);
            System.Diagnostics.Debug.WriteLine("create5");
            //string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //GrantAccess(appPath);
            SqlCommand command = new SqlCommand(script, connection);
            System.Diagnostics.Debug.WriteLine("create6");
            System.Diagnostics.Debug.WriteLine("create7 = " + script);
            var tab = script.Split("GO");

            command.CommandText = tab[0];
            command.ExecuteNonQuery();
            try
            {
                command.CommandText = tab[1];
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                command.CommandText = "DROP DATABASE " + DBName;
                command.ExecuteNonQuery();
                throw ex;
            }
            System.Diagnostics.Debug.WriteLine("create8");

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("create9");
            message = ex.Message;
        }

        return message;
    }



    public static string DeleteDatabase(string dbName)
    {
        string DeleteDatabase;
        string message = null;
        string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        string connectionString = DbCnnStr + $";Initial Catalog={dbName}";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlConnection connection1 = new SqlConnection(DbCnnStr);
        System.Diagnostics.Debug.WriteLine("connectionString=" + connectionString);
        GrantAccess(appPath); //Need to assign the permission for current application to allow create database on server (if you are in domain).
        bool IsExits = CheckDatabaseExists(connection, dbName); //Check database exists in sql server.
        System.Diagnostics.Debug.WriteLine("IsExits=" + IsExits);
        if (IsExits)
        {
            System.Diagnostics.Debug.WriteLine("IsExits1");
            DeleteDatabase = $"DROP DATABASE {dbName}";
            SqlCommand command = new SqlCommand(DeleteDatabase, connection1);
            try
            {
                System.Diagnostics.Debug.WriteLine("IsExits3");
                connection1.Open();
                System.Diagnostics.Debug.WriteLine("IsExits4");
                command.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("IsExits5");

            }
            catch (Exception ex)
            {
                message = ex.Message;
                System.Diagnostics.Debug.WriteLine("IsExits7=" + message);
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("IsExits8");
                connection1.Close();
                System.Diagnostics.Debug.WriteLine("IsExits8");
            }
        }

        return message;
    }

    public static bool GrantAccess(string fullPath)
    {
        DirectoryInfo info = new DirectoryInfo(fullPath);
        WindowsIdentity self = WindowsIdentity.GetCurrent();
        DirectorySecurity ds = info.GetAccessControl();
        ds.AddAccessRule(new FileSystemAccessRule(self.Name,
        FileSystemRights.FullControl,
        InheritanceFlags.ObjectInherit |
        InheritanceFlags.ContainerInherit,
        PropagationFlags.None,
        AccessControlType.Allow));
        info.SetAccessControl(ds);
        return true;
    }


    public static bool CheckDatabaseExists(SqlConnection tmpConn, string databaseName)
    {
        string sqlCreateDBQuery;
        bool result = false;
        int resultObj = 0;
        try
        {
            sqlCreateDBQuery = $"SELECT database_id FROM sys.databases WHERE Name='{databaseName}'";
            System.Diagnostics.Debug.WriteLine("sqlCreateDBQuery=" + sqlCreateDBQuery);
            using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, tmpConn))
            {
                System.Diagnostics.Debug.WriteLine("d5el1c");
                //tmpConn.Open();
                System.Diagnostics.Debug.WriteLine("d5el2c");
                SqlDataReader dataReader = sqlCmd.ExecuteReader();
                while (dataReader.Read())
                {
                    resultObj = Convert.ToInt32(dataReader["database_id"].ToString());
                }
                dataReader.Close();
                System.Diagnostics.Debug.WriteLine("d5el3c");
                System.Diagnostics.Debug.WriteLine("resultObj=" + resultObj);
                if (resultObj != 0)
                {
                    result = true;
                }

            }
        }
        catch (Exception ex)
        {
            //result = false;
            System.Diagnostics.Debug.WriteLine("resultex=" + ex.Message);
        }
        //finally
        //{
        //    tmpConn.Close();
        //}
        System.Diagnostics.Debug.WriteLine("result=" + result);
        return result;
    }
}