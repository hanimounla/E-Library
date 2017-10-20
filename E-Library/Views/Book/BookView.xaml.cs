using System.Diagnostics;
using System.IO;
using System.Windows.Controls;

namespace E_Library.Views
{
    public partial class BookView : UserControl
    {
        public BookView()
        {
            InitializeComponent();
        }

        private void wordCloudBTN_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string BookID = bookIdTB.Text;
            ProcessStartInfo procStartInfo =
                         new ProcessStartInfo("cmd", "/c " + "py -2 BookTextToCloud.py " + BookID);

            // The following commands are needed to redirect the standard output.
            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            procStartInfo.CreateNoWindow = true;
            // Now we create a process, assign its ProcessStartInfo and start it
            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
        }
    }
}
