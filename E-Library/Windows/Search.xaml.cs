using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Speech.Recognition;
using System.Threading;
using System.Windows.Controls;

namespace E_Library
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : DevExpress.Xpf.Core.DXWindow
    {
        static SpeechRecognitionEngine speechRecognitionEngine = null;
        static ManualResetEvent manualResetEvent = null;
        public Search()
        {
            InitializeComponent();
            
            
            progressbar1.Visibility = System.Windows.Visibility.Hidden;
            searchBTN.Visibility = System.Windows.Visibility.Hidden;

            speechRecognitionEngine = new SpeechRecognitionEngine();
            

            string query = "select title from books";
            SqlDataAdapter booksTitle = new SqlDataAdapter(query, DataBaseTools.conn);
            DataSet booksTitleTable = new DataSet();
            booksTitle.Fill(booksTitleTable);
            foreach (DataRow bookTitle in booksTitleTable.Tables[0].Rows)
            {
                speechRecognitionEngine.LoadGrammar(new Grammar(new GrammarBuilder(bookTitle.Field<string>(0))));
            }
        }

        private void speechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            
        }

        private void speechRecognized(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            searchTB.Text = e.Result.Text;
        }

        private void fillBooks()
        {
            fieldCB.Items.Add("Id");
            fieldCB.Items.Add("Title");
            //fieldCB.Items.Add("Category");
            fieldCB.Items.Add("PublishDate");
            fieldCB.Items.Add("ISBN");
            fieldCB.Items.Add("Publisher");
            show();
        }

        private void fillAuthors()
        {
            fieldCB.Items.Add("ID");
            fieldCB.Items.Add("Name");
            fieldCB.Items.Add("Nationality");
            show();
        }

        private void fillPublishers()
        {
            fieldCB.Items.Add("ID");
            fieldCB.Items.Add("Name");
            fieldCB.Items.Add("City");
            fieldCB.Items.Add("Phone");
            show();
        }
        private void show()
        {
            byLBL.Visibility = System.Windows.Visibility.Visible;
            fieldCB.Visibility = System.Windows.Visibility.Visible;
            searchBTN.Visibility = System.Windows.Visibility.Visible;
            searchBTN.Visibility = System.Windows.Visibility.Hidden;
        }

        private void fillSearch()
        {
            byLBL.Visibility = System.Windows.Visibility.Hidden;
            fieldCB.Visibility = System.Windows.Visibility.Hidden;
            searchBTN.Visibility = System.Windows.Visibility.Visible;
            column = "Text";
            searchTB.EditValueChanged -= new DevExpress.Xpf.Editors.EditValueChangedEventHandler(this.searchTB_EditValueChanged);
        }
        string table, column;
        private void searchBTN_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string searchFor = searchTB.Text;
            string query = @"select b.Title ,d.pageid [Page Number] , d.text [Text] 
                                from books b inner join Data d 
                                on b.ID = d.BookID
                                where d.Text like N'%" + searchFor + @"%'
                                union 
                                select b.Title ,pd.pageid [Page Number] , 'Image : ' + pd.text [Text] 
                                from books b inner join PicturesData pd
                                on b.ID = pd.BookID
                                where pd.text like N'%" + searchFor + "%' order by b.Title";
            DataBaseTools.open();
            ExecuteAndFill(query);
        }
        Thread Execute;
        private void ExecuteAndFill(string query)
        {
            SqlCommand cmd = new SqlCommand(query, DataBaseTools.conn);
            progressbar1.Visibility = System.Windows.Visibility.Visible;
            Execute = new Thread(() => ExecuteCommand(cmd));
            Execute.Start();
        }
        private void ExecuteCommand(SqlCommand cmd)
        {
            cmd.ExecuteNonQuery();
            Dispatcher.Invoke(new Action(() =>
            {
                SqlDataAdapter a = new SqlDataAdapter(cmd);
                DataTable t = new DataTable();
                a.Fill(t);
                searchGrid.ItemsSource = t;
                searchGrid.GroupBy("Title");
                searchGrid.GroupBy("Page Number");
                Dispatcher.Invoke(new Action(() => {
                    searchGrid.FindRowByValue("Text", searchTB.Text);
                }));
                    DataBaseTools.close();
                    matchesTB.Text = searchGrid.VisibleRowCount + "";
                    progressbar1.Visibility = System.Windows.Visibility.Hidden;
            }));
        }

        private void tableCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchTB.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(searchTB_EditValueChanged);
            fieldCB.Items.Clear();
            int selected = tableCB.SelectedIndex;

            switch (selected)
            {
                case 0:
                    fillBooks(); table = "Books";
                    break;
                case 1:
                     fillSearch(); table = "Data";
                    break;
                case 2:
                    fillAuthors(); table = "Authors";
                    break;
                case 3:
                    fillPublishers(); table = "Publishers";
                    break;
                default:
                    break;
            }
        }
        private void fieldCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                column = fieldCB.SelectedItem.ToString();
            }
            catch (Exception)
            {

            }
        }
        
        private void searchGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void speechBTN_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            speechRecognitionEngine.SpeechRecognitionRejected += speechRecognized;
            speechRecognitionEngine.SpeechRecognitionRejected += speechRejected;
            speechRecognitionEngine.SetInputToDefaultAudioDevice();
            speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }
        #region #ShowGridMenu
        private void TableView_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            // Check whether this event was raised for a column's context menu.
            if (e.MenuType != GridMenuType.RowCell) return;

            // Remove the Column Chooser menu item.
            e.Customizations.Add(new RemoveBarItemAndLinkAction()
            {
                ItemName = DefaultColumnMenuItemNames.ColumnChooser
            });

            // Create a custom menu item and add it to the context menu.
            BarButtonItem bi = new BarButtonItem();
            bi.Name = "customItem";
            bi.Content = "Open Page";
            bi.ItemClick += new ItemClickEventHandler(customItem_ItemClick);
            e.Customizations.Add(bi);
        }

        private void customItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            int pageId = (int)searchGrid.GetFocusedRowCellValue("Page Number");
            string title = (string)searchGrid.GetFocusedRowCellValue("Title");
            string location = DataBaseTools.GetPdfLocation(title);
            PDFViewer p = new PDFViewer(location, pageId,searchTB.Text);
            p.Show();
            //System.Windows.Forms.MessageBox.Show("Menu Clicked !");
        }

        #endregion #ShowGridMenu

        private void searchTB_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                searchBTN_Click(sender, e);
            }
        }

        private void searchTB_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchTB.Text) && table != "Data" && column != null)
            {
                string query;
                if (column != "ID")
                {
                    query = "Select * from " + table + " Where " + column + " like N'%" + searchTB.Text + "%'";
                }
                else
                    query = "Select * from " + table + " Where " + column + " = " + searchTB.Text + " ";
                DataBaseTools.open();
                SqlCommand cmd = new SqlCommand(query, DataBaseTools.conn);
                cmd.ExecuteNonQuery();
                 SqlDataAdapter a = new SqlDataAdapter(cmd);
                DataTable t = new DataTable();
                a.Fill(t);
                searchGrid.ItemsSource = t;
                DataBaseTools.close();
                matchesTB.Text = searchGrid.VisibleRowCount + "";
            }
            else
            {
                matchesTB.Text = "";
                searchGrid.ItemsSource = null;
                searchGrid.Columns.Clear();
            }
        }
    }
}
