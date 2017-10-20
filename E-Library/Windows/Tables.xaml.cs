using DevExpress.Xpf.Core;


namespace E_Library
{
    /// <summary>
    /// Interaction logic for Tables.xaml
    /// </summary>
    public partial class Tables : DXWindow
    {
        CurrentUser currentUser;
        public Tables(object c)
        {
            InitializeComponent();
            currentUser = (CurrentUser)c;
            UserStatusLable.Content = "User: " + currentUser.getName();

            string connectionString = DataBaseTools.connstr;
            string[] values = connectionString.Split(';');
            connectionStatusLable.Content = "Connection : " + values[0].TrimStart("data source=".ToCharArray());

        }
    }
}
