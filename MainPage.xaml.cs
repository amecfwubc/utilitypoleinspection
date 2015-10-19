using Windows.UI.Core;
using AmecFWUPI.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;

namespace AmecFWUPI
{
    public sealed partial class MainPage
    {
        const string apiUrl = @"http://localhost:90/api/values";
        public MainViewModel Vm => (MainViewModel)DataContext;

        public MainPage()
        {
            MetroEventSource.Log.Debug("Initializing the MainPage");
            InitializeComponent();
            
        }
        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            if (this.IsDataLoaded == false)
            {
                
            }
        }

        
        private void SystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

         
        //A method to navigate to the AddTasks page
        private void btnAddTasks_Click(object sender, RoutedEventArgs e)
        {
            MetroEventSource.Log.Debug("Initializing the AddTasks Page");
            this.Frame.Navigate(typeof(AddTasks));
        }

        //A method to navigate to the ViewPoleInfo page
        private void btnViewTasks_Click(object sender, RoutedEventArgs e)
        {
            MetroEventSource.Log.Debug("Initializing the ViewPoleInfo Page");
            this.Frame.Navigate(typeof(AssignedTaskView));
        }

        private void btnLoadTasks_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ImportServerData));
        }
    }
}
