using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AmecFWUPI.DataBaseModels;
using SQLite.Net.Async;
using System.IO;
using SQLite.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System;
using Windows.UI.Xaml.Media.Imaging;
using System.IO.Compression;
using Windows.Storage;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Media.MediaProperties;

namespace AmecFWUPI
{
    /// <summary>
    /// ImportServerData Page is used to connect to the Sever using API.
    /// </summary>
    public sealed partial class ImportServerData : Page
    {
        string path;
        public static SQLiteConnectionWithLock conn;
        public static SQLiteAsyncConnection dbAsyncConnection;
        //string URI = "http://desktop-9bmhfp2:88/api/poleinfo?UserName=";
        //string URIImage = "http://desktop-9bmhfp2:88/api/PoleImage/Get?ImagePath=";

        public ImportServerData()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            conn = new SQLiteConnectionWithLock(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), new SQLiteConnectionString(path, false));

            conn.TraceListener = new DebugTraceListener();
            dbAsyncConnection = new SQLiteAsyncConnection(() => conn);
        }
        async private void btnLoadTasks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int totalrecord = 0;
                string ImportStatus = "";
                ///all data show from api:
                progressRing1.IsActive = true;

                using (var client = new HttpClient())
                {
                    App.URILogin = App.URILogin + LoginInfo.LoginUserName;
                    
                    using (var response = await client.GetAsync(App.URILogin))
                    {
                        if (response.IsSuccessStatusCode)
                        {

                            

                            var productJsonString = await response.Content.ReadAsStringAsync();

                            var SqliteData = await dbAsyncConnection.QueryAsync<PoleInfo>("select *from PoleInfo");

                            var SqlServerData = JsonConvert.DeserializeObject<PoleInfoViewModel[]>(productJsonString).ToList();

                            //totalrecord = SqlServerData.Count();

                            foreach (var itm in SqlServerData)
                            {
                                
                                //progressBar1.Visibility = Visibility.Visible;
                                var childobj = SqliteData.SingleOrDefault(p => p.poleID == itm.PoleID);
                                if (childobj == null)
                                {

                                    var objpole = new PoleInfo();
                                    objpole.poleID = itm.PoleID;

                                    objpole.poleType = itm.TypeName;
                                    if (itm.TaskAddeddate != null)
                                    {

                                        objpole.dateTaskAdded = Convert.ToDateTime(itm.TaskAddeddate).ToString("MM-dd-yyyy");
                                    }
                                    if (itm.TaskPerformeddate != null)
                                    {
                                        objpole.dateTaskPerformed = Convert.ToDateTime(itm.TaskPerformeddate).ToString("MM-dd-yyyy");
                                    }
                                    objpole.mapImagePath = itm.ImageMapPath;
                                    objpole.adjacentPoleHeight = itm.AdjacentPoleHeight.ToString();
                                    objpole.transformerLoading = itm.TransFormerLoading;
                                    objpole.notes = itm.Notes;
                                    objpole.pathOfImagesTaken = itm.ImagesTakenpath;
                                    objpole.userid = LoginInfo.LoginUserID;
                                    objpole.TaskAssainUserID = (int)itm.TaskAssainUserID;
                                    objpole.TypeID = (int)itm.TypeID;
                                    
                                    using (var client2 = new HttpClient())
                                    {
                                        using (var response2 = await client2.GetAsync(App.URIImage + itm.ImageMapPath))
                                        {
                                            if (response2.IsSuccessStatusCode)
                                            {
                                                string ImageName = itm.PoleID+".jpg";
                                                                                            
                                                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(ImageName, CreationCollisionOption.ReplaceExisting);

                                                var byte2 = await response2.Content.ReadAsByteArrayAsync();
                                                                                               
                                                await FileIO.WriteBytesAsync(file, byte2);

                                            }
                                        }
                                    }
                                    //Save Date
                                     SaveData(objpole);
                                    totalrecord += 1;
                                }

                            }
                            progressRing1.IsActive = false;
                            //progressBar1.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                if(totalrecord>0)
                {
                    ImportStatus= "Total " + totalrecord + " Record Successfully Imported";
                }
                else
                {
                    ImportStatus = "No New Data Found";
                }

                txtmsg.Text = ImportStatus;
            }
            catch (Exception ex)
            {
               txtmsg.Text = ex.Message;
            }

        }
        private void SaveData(PoleInfo obj)
        {
            dbAsyncConnection.InsertAsync(obj);
        }
        
        

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            MetroEventSource.Log.Debug("Goback to MainPage");
        }
        
    }


 
}
