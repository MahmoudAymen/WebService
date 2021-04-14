using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;

public static class DBConnection
{ 
    static string DbCnnStrAuth = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=GBOAuthDB;Data Source=DESKTOP-KSGLTG1";
    public static SqlConnection GetAuthConnection()
    {
        return new SqlConnection(DbCnnStrAuth);
    }
}