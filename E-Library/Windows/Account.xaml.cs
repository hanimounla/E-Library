using System.Windows;
using DevExpress.Xpf.Core;

namespace E_Library.Windows
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : DXWindow
    {
        CurrentUser CurrentUser;
        public  Account()
        {
            InitializeComponent();
        }

        public Account(object currentUser)
        {
            this.CurrentUser = (CurrentUser)currentUser;
            InitializeComponent();
            nameTB.Text = CurrentUser.getName();
        }

        private void changePassBTN_Click(object sender, RoutedEventArgs e)
        {
            if (oldPassTB.Text == CurrentUser.getPassword())
            {
                if (confirmPassTB.Text == NewpassTB.Text)
                {
                    confirmLBL.Content = "";
                    DataBaseTools.UpdateUserPassword(CurrentUser.getID(), confirmPassTB.Text);
                    System.Windows.MessageBox.Show("Password Changed", "Info");
                    Close();
                }
                else
                    confirmLBL.Content = "Confirmation password dosen't match the password";
            }
            else
                System.Windows.MessageBox.Show("Worng password", "Info");
        }

        private void cnacelBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NewpassTB_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (NewpassTB.Text.Length < 8)
            {
                passwordLBL.Content = "Must be longer than 7 charecters";
            }
            else
                passwordLBL.Content = "";
        }
    }
}
