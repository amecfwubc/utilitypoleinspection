using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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
            //await App._database.dbCreateTablePoleInfoAsync();
         
            List<PoleInfo> tasks = await App._database.dbFetchTasksByDateTablePoleInfoAsync(dtPickerLoadTask.Date.ToString("MM-dd-yyyy"));

            lvTasks.ItemsSource = tasks;

        }

        private void lvTasks_ItemClick(object sender, ItemClickEventArgs e)
        {
            var poleId = ((PoleInfo)e.ClickedItem).id;
            this.Frame.Navigate(typeof(AddPoleInfo),poleId);
        }
    }
}
