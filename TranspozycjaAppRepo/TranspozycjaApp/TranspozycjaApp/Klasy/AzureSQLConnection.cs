using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace TranspozycjaApp.Klasy
{
    class AzureSQLConnection
    {
        public static string GetConnectionStrings()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conString"].ToString();
            return connectionString;
        }
        public static string sql;
        public static SqlConnection con = new SqlConnection();
        public static SqlCommand cmd = new SqlCommand("", con);
        public static SqlDataReader rd;
        public static DataTable dt;
        public static SqlDataAdapter da;
        public static void openConnection()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.ConnectionString = GetConnectionStrings();
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("wystąpił błąd podczas próby połączeniem do bazy azure sql."
                    + Environment.NewLine + "opis: " + ex.Message.ToString(), "Connect to SQL Server"
                    , MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void closeConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("wystapił błąd podczas próby rozłączenia bazy."
                    + Environment.NewLine + "opis: " + ex.Message.ToString(), "Connect to SQL Server"
                    , MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
