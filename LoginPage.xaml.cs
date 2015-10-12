using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;


namespace AmecFWUPI
{
    /// <summary>
    /// Login Page
    /// </summary>
    public sealed partial class LoginPage : Page

    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        //Method to check the username and password
        async private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            string pass = await App._database.dbFetchPasswordTableLoginInfoAsync(txtLoginID.Text);

            if (txtLoginPass.Password == pass)
            {
                this.Frame.Navigate(typeof(MainPage));
                MetroEventSource.Log.Debug("Successful Login");
            }
            else
            {
                txtError.Text = "Error in Login";
                MetroEventSource.Log.Error("Incorrect Password");
            }
        }
              
    }
}
