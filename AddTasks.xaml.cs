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
            

            if (file != null)
            {
                //CheckBox chk = (CheckBox)sender;
                //chk.IsChecked = true;


                PoleInfo task = new PoleInfo()
                {
                    poleID = txtPoleID.Text,
                    notes = textBoxNotes.Text,
                    pathOfImagesTaken = "\\poleimages\\" + file.Name,
                    adjacentPoleHeight = txtPoleHeight.Text,
                    transformerLoading = txtTransformerLoad.Text,
                    //poleType = chk.
                    };
                    await dbAsyncConnection.InsertAsync(task);
                    MetroEventSource.Log.Debug("Insert a Pole Info with Image");

            }
            else
            {
                try
                {
                    PoleInfo task = new PoleInfo()
                    {
                        poleID = txtPoleID.Text,
                        notes = textBoxNotes.Text,
                        adjacentPoleHeight = txtPoleHeight.Text,
                        transformerLoading = txtTransformerLoad.Text
                        
                    };
                    await dbAsyncConnection.InsertAsync(task);
                    MetroEventSource.Log.Debug("Insert a Pole Info without Image");
                }
                catch (Exception ex)
                {

                }
            }
         }
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            MetroEventSource.Log.Debug("Go Back to MainPage");
        }
    }

}

