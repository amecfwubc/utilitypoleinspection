using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AmecFWUPI.DataBaseModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AmecFWUPI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPoleInfoTasks : Page
    {
        public AddPoleInfoTasks()
        {
            this.InitializeComponent();
        }

        async private void btnLoadTasks_Click(object sender, RoutedEventArgs e)
        {

            await App._database.dbCreateTablePoleInfoAsync();

            await App._database.dbCreateTablePoleInfoAsync();
            List<PoleInfo> tasks = await App._database.dbFetchTasksByDateTablePoleInfoAsync(dtPickerLoadTask.Date.ToString("MM-dd-yyyy"));
            MetroEventSource.Log.Debug("Loading task according to date");
            lvTasks.ItemsSource = tasks;
            

        }

        private void lvTasks_ItemClick(object sender, ItemClickEventArgs e)
        {
            var poleId = ((PoleInfo)e.ClickedItem).id;
            this.Frame.Navigate(typeof(AddPoleInfo), poleId);
            MetroEventSource.Log.Debug("Initializing the AddPoleInfo Page");
        }
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            MetroEventSource.Log.Debug("Go Back to AddPoleInfo Page");
        }
    }
}
