using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using context = System.Web.HttpContext;

namespace ExceptionLogToDatabase
{
    public class ExceptionLogging
    {
        private static String exepurl;
        static SqlConnection con;
        private static void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["connection"].ToString();
            con = new SqlConnection(constr);
            con.Open();
        }
        public static void SendExcepToDB(Exception exdb)
        {
            connection();
            exepurl = context.Current.Request.Url.ToString();
            SqlCommand cmd = new SqlCommand("ExceptionLoggingToDataBase", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExceptionMsg", exdb.Message.ToString());
            cmd.Parameters.AddWithValue("@ExceptionType", exdb.GetType().Name.ToString());
            cmd.Parameters.AddWithValue("@ExceptionURL", exepurl);
            cmd.Parameters.AddWithValue("@ExceptionSource", exdb.StackTrace.ToString());
            cmd.ExecuteNonQuery();
        }
    }
}