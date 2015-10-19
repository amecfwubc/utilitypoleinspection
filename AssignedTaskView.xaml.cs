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
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Views;
using Windows.UI.Input;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AmecFWUPI
{
    /// <summary>
    /// This AssignedTaskView page can be used to view different Pole information from local database.
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

            var SqliteData = await dbAsyncConnection.QueryAsync<PoleInfo>("select poleID from PoleInfo where userid='"+LoginInfo.LoginUserID+"'");
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
                else if (Data.poleType == "Return")
                {
                    cbReturn.IsChecked = true;
                }
                else if (Data.poleType == "Vegetation")
                {
                    cbVegetation.IsChecked = true;
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

        private void Edit_data(object sender, RoutedEventArgs e)
        {
            LocalDataSave();
        }

        async private void DataUpdateToServer(PoleInfo obj)
        {
            string URI = "http://desktop-9bmhfp2:88/api/poleinfo";
            try
            {
                PoelInfo pole = new PoelInfo();
                pole.PoleID = obj.poleID;
                pole.Notes = obj.notes;
                pole.TransFormerLoading = obj.transformerLoading;
                pole.TaskAddeddate = Convert.ToDateTime(obj.dateTaskAdded);
                pole.TaskPerformeddate = Convert.ToDateTime(obj.dateTaskPerformed);
                pole.TypeID = obj.TypeID;
                pole.TaskAssainUserID = obj.TaskAssainUserID;
                pole.AdjacentPoleHeight = Convert.ToDouble(obj.adjacentPoleHeight);
                pole.UserID = obj.userid;
                using (var client = new HttpClient())
                {
                    var serializedProduct = JsonConvert.SerializeObject(pole);
                    var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(URI, content);
                }
            }
            catch (Exception ex)
            {
                txtmsg.Text = ex.Message;
            }
        }

        async private void LocalDataSave()
        {
            string SaveStatus = "";

            string poleId = drpPoleID.SelectedValue.ToString();
            
            var Data = await App._database.dbFetchTasksByPoleIdTablePoleInfoAsync(drpPoleID.SelectedValue.ToString());
            try
            {
                if (Data != null)
                {
                    //PoleInfo task = new PoleInfo();
                    //task.id = Data.id;

                    Data.poleID = poleId;
                    Data.notes = textBoxNotes.Text;
                    //if (file != null)
                    //{
                    //    task.pathOfImagesTaken = "\\poleimages\\" + file.Name;
                    //}
                    Data.poleType = GetCheckedPoleType();
                    Data.adjacentPoleHeight = txtPoleHeight.Text;
                    Data.transformerLoading = txbTransformerLoad.Text;
                    //Data.userid = LoginInfo.LoginUserID;
                    //Data.TaskAssainUserID = LoginInfo.LoginUserID;

                    Data.TypeID = GetPoleTypeId(Data.poleType);
                    await dbAsyncConnection.UpdateAsync(Data);
                    DataUpdateToServer(Data);
                    SaveStatus = "Successfully Updated";
                }
                else
                {
                    SaveStatus = "Pole ID is already Exist";
                }

                txtmsg.Text = SaveStatus;
            }
            catch (Exception ex)
            {

                txtmsg.Text = ex.Message;
            }

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

        private int  GetPoleTypeId(string PoleType)
        {
            int  PoleTypeID = 0;
            if (PoleType == "Additional Pole")
            {
                PoleTypeID=11;
            }
            else if (PoleType == "Non standard")
            {
                PoleTypeID = 2;
            }
            else if (PoleType == "Simple")
            {
                PoleTypeID = 10;
            }
            else if (PoleType == "Enviromental")
            {
                PoleTypeID = 12;
            }

            else if (PoleType == "Row")
            {
                PoleTypeID = 12;
            }
            else if (PoleType == "Vegetation")
            {
                PoleTypeID = 14;
            }
            else if (PoleType == "Return")
            {
                PoleTypeID = 15;
            }
            return PoleTypeID;
        }

    }
  

}

