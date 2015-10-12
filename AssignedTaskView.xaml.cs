using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AmecFWUPI.DataBaseModels;
using Windows.Storage.Pickers;
using Windows.Storage;
using SQLite.Net.Async;
using SQLite.Net;
using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AmecFWUPI
{
    /// <summary>
    /// This AddTaks page can be used to enter different Pole information and insert it into the sqlite database.
    /// </summary>
    public sealed partial class AssignedTaskView : Page
    {

        string path;
        public static SQLiteConnectionWithLock conn;
        public static SQLiteAsyncConnection dbAsyncConnection;
        FileOpenPicker openPicker = new FileOpenPicker();
        StorageFile file;
        public AssignedTaskView()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            conn = new SQLiteConnectionWithLock(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), new SQLiteConnectionString(path, false));

            conn.TraceListener = new DebugTraceListener();
            dbAsyncConnection = new SQLiteAsyncConnection(() => conn);

            LoadPoleList();
        }

        async private void LoadPoleList()
        {
            //var Data = App._database.LoadPoleIDList();

            var SqliteData = await dbAsyncConnection.QueryAsync<PoleInfo>("select poleID from PoleInfo");
            //drpPoleID.DataContext = SqliteData;
            //drpPoleID.SelectedValue = "poleID";
            //drpPoleID.SelectedItem = "poleID";


            foreach (var itm in SqliteData)
            {
                drpPoleID.Items.Add(itm.poleID);
            }


        }

        async public void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/ write access to the picked file
                txtBlkPhotos.Text = "Picked photo: " + file.Name;
                MetroEventSource.Log.Debug("Picked an image file");
            }
            else
            {
                txtBlkPhotos.Text = "Operation cancelled.";
            }
        }

        
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            MetroEventSource.Log.Debug("Go Back to MainPage");
        }

        async private void View_data(object sender, RoutedEventArgs e)
        {
            try
            {
                ControlDefaultSetting();
                var Data = await App._database.dbFetchTasksByPoleIdTablePoleInfoAsync(drpPoleID.SelectedValue.ToString());
                if (Data == null)
                {
                    //
                    return;
                }
                //Data
                if (Data.poleType == "Simple")
                {
                    cbSimple.IsChecked = true;
                }
                else if (Data.poleType == "Non Standard")
                {
                    cbNonStandard.IsChecked = true;
                }

                txtPoleHeight.Text = Data.adjacentPoleHeight;
                txbTransformerLoad.Text = Data.transformerLoading;
                if (Data.dateTaskAdded != null)
                {
                    dtPickerLoadTask.Date = Convert.ToDateTime(Data.dateTaskAdded);
                }

                if (Data.dateTaskPerformed != null)
                {
                   
                    dtPickerPerformTask.Date = Convert.ToDateTime(Data.dateTaskPerformed);
                }
                textBoxNotes.Text = Data.notes;
                Uri imageUri2 = new Uri(@"C:\Users\salva\AppData\Local\Packages\f867452b-a946-4cec-bc0d-a7793b639c29_we21w4g8rdebr\LocalState\"+Data.poleID+".jpg");
                imagePreivew.Source = new BitmapImage(imageUri2);
                


            }
            catch (Exception ex)
            {

            }

       }

        private void ControlDefaultSetting()
        {
            cbEnvironmental.IsChecked = false;
            cbNonStandard.IsChecked = false;
            cbReturn.IsChecked = false;
            cbROW.IsChecked = false;
            cbSimple.IsChecked = false;
            cbVegetation.IsChecked = false;

            txtPoleHeight.Text = "";
            txbTransformerLoad.Text = "";

            textBoxNotes.Text = "";
        }

        private void drpPoleID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}

