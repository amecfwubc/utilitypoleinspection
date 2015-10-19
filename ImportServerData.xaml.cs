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
        string URI = "http://desktop-9bmhfp2:88/api/poleinfo?UserName=";
        string URIImage = "http://desktop-9bmhfp2:88/api/PoleImage/Get?ImagePath=";

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

                using (var client = new HttpClient())
                {
                    URI = URI + LoginInfo.LoginUserName;
                    
                    using (var response = await client.GetAsync(URI))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var productJsonString = await response.Content.ReadAsStringAsync();

                            var SqliteData = await dbAsyncConnection.QueryAsync<PoleInfo>("select *from PoleInfo");

                            var SqlServerData = JsonConvert.DeserializeObject<PoleInfoViewModel[]>(productJsonString).ToList();

                            //totalrecord = SqlServerData.Count();

                            foreach (var itm in SqlServerData)
                            {
                                var childobj = SqliteData.SingleOrDefault(p => p.poleID == itm.PoleID);
                                if (childobj == null)
                                {

                                    var objpole = new PoleInfo();
                                    objpole.poleID = itm.PoleID;


                                    //string POleTypeName = "";
                                    //switch (itm.TypeID)
                                    //{
                                    //    case 4:
                                    //        POleTypeName = "Simple";
                                    //        break;

                                    //    case 5:
                                    //        POleTypeName = "Non Standard";
                                    //        break;
                                    //}

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
                                    //GetImageByte(itm.ImageMapPath);
                                    //Save Image

                                    using (var client2 = new HttpClient())
                                    {
                                        using (var response2 = await client2.GetAsync(URIImage + itm.ImageMapPath))
                                        {
                                            if (response2.IsSuccessStatusCode)
                                            {
                                                string ImageName = itm.PoleID+".jpg";
                                                                                            
                                                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(ImageName, CreationCollisionOption.GenerateUniqueName);

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
        async private void GetImageByte(string imagePath)
        {
            byte[] bytes;
            using (var client2 = new HttpClient())
            {
                using (var response2 = await client2.GetAsync(URIImage + imagePath))
                {
                    if (response2.IsSuccessStatusCode)
                    {

                        var data = await response2.Content.ReadAsStreamAsync();
                        //Image image = Image.FromStream(data);
                        //byte[] byte = Encoding.Unicode.GetBytes(data);
                        using (var br = new BinaryReader(data))
                        {
                            using (var ms = new MemoryStream())
                            {
                                var lineBuffer = br.ReadBytes(1024);

                                while (lineBuffer.Length > 0)
                                {
                                    ms.Write(lineBuffer, 0, lineBuffer.Length);
                                    lineBuffer = br.ReadBytes(1024);
                                }

                                bytes = new byte[(int)ms.Length];
                                ms.Position = 0;
                                ms.Read(bytes, 0, bytes.Length);
                            }
                        }

                        
                        //StorageFile sampleFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("userImage.jpg", CreationCollisionOption.ReplaceExisting);
                        //await FileIO.WriteBytesAsync(sampleFile, bytes);
                        //var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.Read);

                    }
                }
            }
            //get response
            //var response = await request.GetResponseAsync();
            

        }
        async Task<BitmapImage> convertBytesToBitmapAsync(byte[] bytes)
        {
            //convert to bitmap
            var bitmapImage = new BitmapImage();
            var stream = new InMemoryRandomAccessStream();
            stream.WriteAsync(bytes.AsBuffer());
            stream.Seek(0);

            //display
            bitmapImage.SetSource(stream);

            return bitmapImage;
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            MetroEventSource.Log.Debug("Goback to MainPage");
        }


        private  async  void GetAllPoleInfo()
        {
            ///all data show from api:

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();
                       var data = JsonConvert.DeserializeObject<PoelInfo[]>(productJsonString).ToList();


                    }
                }
            }


            ////Single data show from Api
            string id = "21";
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI+"/"+id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<PoelInfo>(productJsonString);


                    }
                }
            }

        }

        private int GetPoleTypeId(string PoleType)
        {
            int PoleTypeID = 0;
            if (PoleType == "Additional Pole")
            {
                PoleTypeID = 11;
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
