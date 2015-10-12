using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AmecFWUPI.DataBaseModels;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Media.Capture;      //For MediaCapture  
using Windows.Media.MediaProperties;  //For Encoding Image in JPEG format  
using Windows.Devices.Enumeration;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using Windows.Storage.Streams;
using System.IO;
using System.Reflection;
using System.IO.IsolatedStorage;

namespace AmecFWUPI
{
    
    public sealed partial class AddPoleInfo : Page
    {

        PoleInfo currentPole;
        string id="3";

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
            Image1(openPicker);
            return;

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
                currentPole.pathOfImagesTaken = "\\poleimages\\" + file.Name;
                MetroEventSource.Log.Debug("Picked an image file");
                //imagePreivew.Source = @"c:\\Picture\\Photo.jpg";
                //BitmapImage bitmapImage = new BitmapImage();
                //Image img = imagePreivew as Image;
                //img.Width = bitmapImage.DecodePixelWidth = 280;
                //bitmapImage.UriSource = new Uri(@"c:\\Picture\\Photo.jpg");
                //img.Source = bitmapImage;
                //Image1(openPicker);
            }
            else
            {
                txtBlkPhotos.Text = "Operation cancelled.";
            }
        }
        
        private async void LoadImageAsync(string FileLocation)
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("AmecFWUPI:///Image/Photo.jpg"));
            using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
            {
                BitmapImage image = new BitmapImage();
                image.SetSource(fileStream);
                imagePreivew.Source = image;
            }
        }

        //private async void LoadImage_Click_1(object sender, RoutedEventArgs e)
        //{
        //    //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Image/Photo.jpg"));
        //    //using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
        //    //{
        //    //BitmapImage image = new BitmapImage();
        //    //    image.SetSource(fileStream);
        //    //    LoadImageDynam.Source = image;
        //    //}

        //    //byte[] data = File.ReadAllBytes(@"c:\Photo.png"); // not a good idea...
        //    //MemoryStream ms = new MemoryStream(data);
        //    //Image img = Image.FromStream(ms);
        //    //LoadImageDynam.Source = Image.Fromfile(@"Images\a.bmp");
        //    //byte[] data = File.ReadAllBytes(@"c:\Photo.png");
        //    //picker.FileTypeFilter.Add(".jpg");
        //    //var file = await picker.PickSingleFileAsync();
        //    //var stream = await file.OpenReadAsync();

        //    //BitmapImage img = new BitmapImage();
        //    //img.SetSource(stream);
        //    //imagePreivew.Source = img;
        //    // imagePreivew.Source = new BitmapImage(new Uri(@"AmecFWUPI:///Image/Photo.jpg", UriKind.Absolute));
        //    try
        //    {
        //        //Uri imageUri = new Uri("C:\\Picture\\Photo.jpg");
        //        //imagePreivew.Source = new BitmapImage(imageUri);

        //        //Windows.Storage.StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
        //        //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(imageUri);

        //        Stream file = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("Photo23.jpg");
        //        // imagePreivew.Source = file;
        //        //var byte2 = await response2.Content.ReadAsByteArrayAsync();

        //        //await FileIO.WriteBytesAsync(file, byte2);
        //        Uri imageUri2 = new Uri(@"C:\Users\salva\AppData\Local\Packages\f867452b-a946-4cec-bc0d-a7793b639c29_we21w4g8rdebr\LocalState\Photo23.jpg");
        //        imagePreivew.Source = new BitmapImage(imageUri2);

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        async public void Image1(FileOpenPicker picker)
        {
            // FileOpenPicker picker = new FileOpenPicker();

        }


        Windows.Media.Capture.MediaCapture captureManager;

        async private void InitCamera_Click(object sender, RoutedEventArgs e)
        {
            //First need to find all webcams
            DeviceInformationCollection webcamList = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            // A query to find the front webcam
            DeviceInformation frontWebcam = (from webcam in webcamList
                                             where webcam.EnclosureLocation != null
                                             && webcam.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front
                                             select webcam).FirstOrDefault();

            // Same for the back webcam
            DeviceInformation backWebcam = (from webcam in webcamList
                                            where webcam.EnclosureLocation != null
                                            && webcam.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Back
                                            select webcam).FirstOrDefault();

            // initialize MediaCapture
            var captureManager = new MediaCapture();
            await captureManager.InitializeAsync(new MediaCaptureInitializationSettings
            {
                // Choose the webcam (backWebcam or frontWebcam)
                VideoDeviceId = backWebcam.Id,
                AudioDeviceId = "",
                StreamingCaptureMode = StreamingCaptureMode.Video,
                PhotoCaptureSource = PhotoCaptureSource.VideoPreview
            });
            // Set the source of the CaptureElement to the  MediaCapture
            capturePreview.Source = captureManager;

            // Start the preview
            await captureManager.StartPreviewAsync();
            MetroEventSource.Log.Debug("Start Previewing the Camera");

        }

        async private void Stop_Capture_Preview_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                //_isPreviewing = false;
                await captureManager.StopPreviewAsync();
                MetroEventSource.Log.Debug("Stop Previewing the Camera");
            }
            catch (Exception ex)
            {
            
            }

            // Use the dispatcher because this method is sometimes called from non-UI threads
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                // Cleanup the UI
                capturePreview.Source = null;

            });
        }

        async private void Capture_Photo_Click(object sender, RoutedEventArgs e)
        {
            //Create JPEG image Encoding format for storing image in JPEG type  
            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();
            // create storage file in local app storage  
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Photo.jpg", CreationCollisionOption.GenerateUniqueName);
            // take photo and store it on file location.  
            await capturePreview.Source.CapturePhotoToStorageFileAsync(imgFormat, file);
            MetroEventSource.Log.Debug("Capture an image by using Camera");
            //// create storage file in Picture Library  
            //StorageFile file1 = await KnownFolders.PicturesLibrary.CreateFileAsync("Photo.jpg",CreationCollisionOption.GenerateUniqueName);  
            // Get photo as a BitmapImage using storage file path.  
            //BitmapImage bmpImage = new BitmapImage(new Uri(file.Path));
            // show captured image on Image UIElement.  
            //imagePreivew.Source = bmpImage;
            txtBlkPhotoss.Text = "Picked photo: " + file.Name;
            currentPole.pathOfImagesTaken = "\\poleimages\\" + file.Name;
        }
       
        
        private void textBoxNotes_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentPole.notes = textBoxNotes.Text;
            //textBoxNotes.Text = currentPole.notes;
            MetroEventSource.Log.Debug("Update Notes into the database");
        }


        async private void btnAddPoleInfo_Click(object sender, RoutedEventArgs e)
        {
            await App._database.dbUpdateTablePoleInfoAsync(currentPole);
            MetroEventSource.Log.Debug("Update the PoleInfo Table");
        }
        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            MetroEventSource.Log.Debug("Update the PoleInfo Table");
        }
    }
}
