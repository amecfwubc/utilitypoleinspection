using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AmecFWUPI.DataBaseModels;
using Windows.Storage.Pickers;
using Windows.Storage;
using SQLite.Net.Async;
using SQLite.Net;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AmecFWUPI
{
    /// <summary>
    /// This AddTaks page can be used to enter different Pole information and insert it into the sqlite database.
    /// </summary>
    public sealed partial class AddTasks : Page
    {

        string path;
        public static SQLiteConnectionWithLock conn;
        public static SQLiteAsyncConnection dbAsyncConnection;
        FileOpenPicker openPicker = new FileOpenPicker();
        StorageFile file;
        public AddTasks()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            conn = new SQLiteConnectionWithLock(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), new SQLiteConnectionString(path, false));

            conn.TraceListener = new DebugTraceListener();
            dbAsyncConnection = new SQLiteAsyncConnection(() => conn);
            
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

        async private void btnAddPoleInfo_Click(object sender, RoutedEventArgs e)
        {
            string SaveStatus = "";
            
            var SqliteData = await dbAsyncConnection.QueryAsync<PoleInfo>("select poleID from PoleInfo where poleID='" + txtPoleID.Text + "'");

            if (SqliteData.Count == 0)
            {
                PoleInfo task = new PoleInfo();


                task.poleID = txtPoleID.Text;
                task.notes = textBoxNotes.Text;
                if (file != null)
                {
                    task.pathOfImagesTaken = "\\poleimages\\" + file.Name;
                }
                task.poleType = GetCheckedPoleType();
                task.adjacentPoleHeight = txtPoleHeight.Text;
                task.transformerLoading = txtTransformerLoad.Text;
                task.userid = LoginInfo.LoginUserID;

                await dbAsyncConnection.InsertAsync(task);
                SaveStatus = "Saved";
            }
            else
            {
                SaveStatus = "Pole ID is already Exist";
            }

            txtmsg.Text = SaveStatus;



        }
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            MetroEventSource.Log.Debug("Go Back to MainPage");
        }
        private void ControlDefaultSetting()
        {
            cbEnvironmental.IsChecked = false;
            cbNonStandard.IsChecked = false;
            cbReturn.IsChecked = false;
            cbROW.IsChecked = false;
            cbSimple.IsChecked = false;
            cbVegetation.IsChecked = false;
            cbAdditionalPole.IsChecked = false;
            txtPoleID.Text = "";
            txtPoleHeight.Text = "";
            txtTransformerLoad.Text = "";
            textBoxNotes.Text = "";
            txtmsg.Text = "";
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            ControlDefaultSetting();
        }

        private string GetCheckedPoleType()
        {
            string PoleType = "";
            if (cbAdditionalPole.IsChecked == true)
            {
                PoleType = "Additional Pole";
            }
            else if (cbNonStandard.IsChecked == true)
            {
                PoleType = "Non standard";
            }
            else if (cbSimple.IsChecked == true)
            {
                PoleType = "Simple";
            }
            else if (cbEnvironmental.IsChecked == true)
            {
                PoleType = "Enviromental";
            }

            else if (cbROW.IsChecked == true)
            {
                PoleType = "Row";
            }
            else if (cbVegetation.IsChecked == true)
            {
                PoleType = "Vegetation";
            }
            else if (cbReturn.IsChecked == true)
            {
                PoleType = "Return";
            }
            return PoleType;
        }
    }
}

