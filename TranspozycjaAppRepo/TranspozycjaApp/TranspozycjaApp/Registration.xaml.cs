using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TranspozycjaApp.Klasy;
using System.Data;
using System.Data.SqlClient;


namespace TranspozycjaApp
{
    /// <summary>
    /// Logika interakcji dla klasy Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string UserID = "";
            string login = LoginText.Text;
            string password = PasswordBoxText.Password;

            AzureSQLConnection.openConnection();
            AzureSQLConnection.sql = "select top 1 * from Users where Login='" + LoginText.Text + "'";
            AzureSQLConnection.cmd.CommandType = CommandType.Text;
            AzureSQLConnection.cmd.CommandText = AzureSQLConnection.sql;
            AzureSQLConnection.da = new SqlDataAdapter(AzureSQLConnection.cmd);
            AzureSQLConnection.dt = new DataTable();
            AzureSQLConnection.da.Fill(AzureSQLConnection.dt);

            try//dodawanie uzytkownika do bazy
            {
                DateTime dateTime = DateTime.Now;
                string sqlFormattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                AzureSQLConnection.openConnection();
                AzureSQLConnection.sql = "INSERT INTO Users (Login, Password, Email) VALUES ('" + LoginText.Text + "', '" + PasswordBoxText.Password + "', '" + EmailText.Text + "')";
                AzureSQLConnection.cmd.CommandType = CommandType.Text;
                AzureSQLConnection.cmd.CommandText = AzureSQLConnection.sql;
                AzureSQLConnection.cmd.ExecuteNonQuery(); //to wykonuje inserta :P
                AzureSQLConnection.closeConnection();
                //Insert do wagi, która zawiera informację o aktualnej dacie

                //Pobranie ID dopiero co dodanego Usera
                //AzureSQLConnection.openConnection();
                //AzureSQLConnection.sql = "SELECT top 1 ID_User FROM Users WHERE Login='" + Login + "' AND Password='" + password + "'";
                //AzureSQLConnection.cmd.CommandText = AzureSQLConnection.sql;
                //AzureSQLConnection.rd = AzureSQLConnection.cmd.ExecuteReader();
                //if (AzureSQLConnection.rd.Read())
                //{
                //    UserID = AzureSQLConnection.rd["ID_User"].ToString();
                //    Console.WriteLine(UserID); //Do testowania
                //}
                //AzureSQLConnection.closeConnection(); 
                                          
         
                MessageBox.Show("Użytkownik " + LoginText.Text + " został poprawnie utworzony.", "Rejestracja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)//jesli baza nie dziala
            {
                MessageBox.Show("Błąd przy rejestracji"
                   + Environment.NewLine + "opis: " + ex.Message.ToString(), "Rejestracja"
                   , MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
