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


namespace AmecFWUPI
{
    /// <summary>
    /// ViewPoleInfo Page is created to view the data that are stored in PoleInfo table.
    /// </summary>
    public sealed partial class ViewPoleInfo : Page
    {
        string path;
        public static SQLiteConnectionWithLock conn;
        public static SQLiteAsyncConnection dbAsyncConnection;
        string URI = "http://desktop-9bmhfp2:88/api/poleinfo";

        public ViewPoleInfo()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            conn = new SQLiteConnectionWithLock(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), new SQLiteConnectionString(path, false));

            conn.TraceListener = new DebugTraceListener();
            dbAsyncConnection = new SQLiteAsyncConnection(() => conn);

        }

        //A method to view the data from PoleInfo table.
        async private void btnLoadTasks_Click(object sender, RoutedEventArgs e)
        {
            var query = await dbAsyncConnection.QueryAsync<PoleInfo>("select *from PoleInfo");
            //ServiceReference1.Service1Client obj = new ServiceReference1.Service1Client();

            //var data = obj.GetPoleDataAsync();
            //var data1 = await obj.GetDataAsync(1);
            //GetAllPoleInfo();
            var list = dbAsyncConnection.Table<PoleInfo>();
            // assign to listview
            foreach (var item in query)
            {
                lvTasks.ItemsSource = query;
            }
            MetroEventSource.Log.Debug("View Pole Info from PoleInfo Table");
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

    }


 
}
