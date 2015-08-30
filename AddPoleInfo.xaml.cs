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
using Windows.Storage.Pickers;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AmecFWUPI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPoleInfo : Page
    {
        
            PoleInfo currentPole;
        string id;
        
        public AddPoleInfo()
        {
            this.InitializeComponent();

           

            
            
        }

      async public void refreshScreen()
        {
            currentPole = await App._database.dbFetchTasksByIdTablePoleInfoAsync(id);

            textPoleID.Text = "Pole ID: " + currentPole.poleID;
            currentPole.dateTaskPerformed = DateTime.Now.ToString("MM-dd-yyyy");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // code here
            id = e.Parameter.ToString();

            try
            {
                refreshScreen();
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }


        }

        async private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                txtBlkPhotos.Text = "Picked photo: " + file.Name;
                currentPole.pathOfImagesTaken = "\\poleimages\\"+file.Name;
            }
            else
            {
                txtBlkPhotos.Text = "Operation cancelled.";
            }
        }

        private void textBoxNotes_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentPole.notes = textBoxNotes.Text;
        }

        async private void btnAddPoleInfo_Click(object sender, RoutedEventArgs e)
        {
            await App._database.dbUpdateTablePoleInfoAsync(currentPole);
        }
    }
}
