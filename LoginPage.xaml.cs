using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using AmecFWUPI.DataBaseModels;
using SQLite.Net;
using System.IO;
using SQLite.Net.Async;

namespace AmecFWUPI
{
    /// <summary>
    /// Login Page
    /// </summary>
    public sealed partial class LoginPage : Page

    {
        string path;
        static SQLiteConnectionWithLock conn;
        static SQLiteAsyncConnection dbAsyncConnection;
        public LoginPage()
        {
            this.InitializeComponent();
            
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLiteConnectionWithLock(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), new SQLiteConnectionString(path, false));
            conn.TraceListener = new DebugTraceListener();
            dbAsyncConnection = new SQLiteAsyncConnection(() => conn);
        }

        //Method to check the username and password
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var UserName = txtLoginID.Text;
            var PassWord = txtLoginPass.Password;
            
            LogIn(UserName,PassWord);
            
        }

        async private void LogIn(string UserName, string PassWord)
        {
            progressRing1.IsActive = true;
            var logIn_V = await App._database.CheckLogin(UserName, PassWord);
            //string pass = await App._database.dbFetchPasswordTableLoginInfoAsync(txtLoginID.Text);

            if (logIn_V != null)
            {
                
                LoginInfo.LoginUserName = UserName;
                LoginInfo.LoginUserID = logIn_V.id;
                this.Frame.Navigate(typeof(MainPage));
                MetroEventSource.Log.Debug("Successful Login");
            }
            else
            {
                var IsValid =await userAuthorizedByServer(UserName, PassWord);
                if (IsValid)
                {
                    LogIn(UserName, PassWord);
                    progressRing1.IsActive = false;
                }
                else
                {
                    txtError.Text = "Error in Login";
                    MetroEventSource.Log.Error("Incorrect Password");
                    progressRing1.IsActive = false;
                }
            }
        }
        async private Task<bool> userAuthorizedByServer(string uname, string pass)
        {
            bool returnvalue = false;
            try
            {

                var URL = "http://desktop-9bmhfp2:88/api/user?UserName="+uname+"&Password="+pass+"";
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(URL))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var productJsonString = await response.Content.ReadAsStringAsync();
                                                       
                            var user = JsonConvert.DeserializeObject<UserViewModel[]>(productJsonString).FirstOrDefault();
                            if (user != null)
                            {
                                var obj = new LoginInfo();

                                obj.userName = user.UserName;
                                obj.UserFullName = user.UserFullName;
                                obj.password = user.UPassword;
                                LoginInfo.LoginUserID = user.Id;
                                await dbAsyncConnection.InsertAsync(obj);
                                returnvalue = true;
                            }
                            
                        }
                    }
                }
                return returnvalue;
            }
            catch (Exception ex)
            {
                return returnvalue;
            }
        }

        private void txtLoginID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
