using DevExpress.Xpf.Core;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
namespace E_Library
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : DXWindow
    {
        public bool validlogin = false;
        private int userID;
        public byte securityLevel = 0;
        public string username = "";
        public string userpassword = "";
        public LogIn()
        {
            InitializeComponent();
            progressbarLBL.Visibility = Visibility.Hidden;
            progressPanel1.Visibility = Visibility.Hidden;
            
        }

        private void logInBTN_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTB.Text;
            string pass = passTB.Password;
            Thread t = new Thread(() => DoLogin(name,pass));
            progressPanel1.Visibility = Visibility.Visible;
            progressbarLBL.Visibility = Visibility.Visible;
            t.Start();
        }

        private void quitBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void DoLogin(string enterdName,string enteredPassword)
        {
            try
            {
                DataBaseTools.conn.Open();
            }
            catch
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    System.Windows.Forms.MessageBox.Show("Unable to connect to database !", "info",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressPanel1.Visibility = Visibility.Hidden;
                    progressbarLBL.Visibility = Visibility.Hidden;
                    return;
                }));
            }
            string cmdstr = "SELECT ID, password, SecurityLevel from Users where UserName = '" + enterdName + "'";
            SqlCommand cmd = new SqlCommand(cmdstr, DataBaseTools.conn);
            try
            {
                var row = cmd.ExecuteReader();
                row.Read();
                userpassword = row[1].ToString();
                userID = (int)row[0];
                username = enterdName;
                if (enteredPassword == userpassword)
                {
                    validlogin = true;
                    securityLevel = (byte)row[2];
                    Dispatcher.Invoke(new Action(() =>
                    {
                        DataBaseTools.conn.Close();
                        progressPanel1.Visibility = Visibility.Hidden;
                        progressbarLBL.Visibility = Visibility.Hidden;
                        CurrentUser c = new CurrentUser();
                        c.setID(userID);
                        c.setName(enterdName);
                        c.setPassword(enteredPassword);
                        c.setSecurityLevel(securityLevel);

                        MainWindow m = new MainWindow(c);
                        m.Show();
                        this.Close();
                    }));
                }
                else
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        DataBaseTools.conn.Close();
                        progressPanel1.Visibility = Visibility.Hidden;
                        progressbarLBL.Visibility = Visibility.Hidden;
                        System.Windows.Forms.MessageBox.Show("incorrect password", "info",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }));
                }
            }
            catch (Exception ex )
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    progressPanel1.Visibility = Visibility.Hidden;
                    progressbarLBL.Visibility = Visibility.Hidden;
                    System.Windows.Forms.MessageBox.Show("Wrong login \n"+ex.Message , "info",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }));
            }
        }
    }
}
