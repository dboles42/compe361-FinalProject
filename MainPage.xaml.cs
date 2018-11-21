using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.System.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;

using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.Foundation;

namespace Webcam_Test
{
    public sealed partial class MainPage : Page
    {
        // Provides functionality to capture the output from the camera
        public MediaCapture _mediaCapture;

        // This object allows us to manage whether the display goes to sleep 
        // or not while our app is active.
        private readonly DisplayRequest _displayRequest = new DisplayRequest();

        // Tells us if the camera is external or on board.
        private bool _externalCamera = false;

        public MainPage()
        {
            InitializeComponent();

            // https://msdn.microsoft.com/en-gb/library/windows/apps/hh465088.aspx
            Application.Current.Resuming += Application_Resuming;
            Application.Current.Suspending += Application_Suspending;
        }

        private void Application_Suspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            Dispose();
            deferral.Complete();
        }

        private async void Application_Resuming(object sender, object o)
        {
            //test
        }

        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //await InitializeCameraAsync();
        }
        
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Dispose();
        }

        private async Task InitializeCameraAsync()
        {
            if (_mediaCapture == null)
            {
                // Get the camera devices
                var cameraDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

                // try to get the back facing device for a phone
                var backFacingDevice = cameraDevices
                    .FirstOrDefault(c => c.EnclosureLocation?.Panel == Windows.Devices.Enumeration.Panel.Back);

                // but if that doesn't exist, take the first camera device available
                var preferredDevice = backFacingDevice ?? cameraDevices.FirstOrDefault();

                // Store whether the camera is onboard of if it's external.
                _externalCamera = backFacingDevice == null;

                // Create MediaCapture
                _mediaCapture = new MediaCapture();

                // Stop the screen from timing out.
                _displayRequest.RequestActive();

                // Initialize MediaCapture and settings
                await _mediaCapture.InitializeAsync(
                    new MediaCaptureInitializationSettings
                    {
                        VideoDeviceId = preferredDevice.Id
                    });

                // Set the preview source for the CaptureElement
               // PreviewControl.Source = _mediaCapture;

                // Start viewing through the CaptureElement 
                await _mediaCapture.StartPreviewAsync();

                // Set rotation properties to ensure the screen is filled with the preview.
            }
        }




        private void Dispose()
        {
            if (_mediaCapture != null)
            {
                _mediaCapture.Dispose();
                _mediaCapture = null;
            }

    //        if (PreviewControl.Source != null)
            {
     //           PreviewControl.Source.Dispose();
       //         PreviewControl.Source = null;
            }
            
        }

        public async void SoftwareButton(object sender, RoutedEventArgs e)
        {
            // This is where we want to save to.
            var storageFolder = KnownFolders.SavedPictures;

            // Create the file that we're going to save the photo to.
            var file = await storageFolder.CreateFileAsync("sample.jpg", CreationCollisionOption.ReplaceExisting);


            // Update the file with the contents of the photograph.
            await _mediaCapture.CapturePhotoToStorageFileAsync(ImageEncodingProperties.CreateJpeg(), file);

            await _mediaCapture.StopPreviewAsync();
           
        }

        public async void btnscan_Click(object sender, RoutedEventArgs e)
        {

            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var size = new Size(bounds.Width * scaleFactor, bounds.Height * scaleFactor);
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(size.Width, size.Height);
            StorageFile file = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (file != null)
            {
                //QR code conversion from jepg and return string.
                WriteableBitmap writeableBitmap;
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

                    writeableBitmap = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);
                    writeableBitmap.SetSource(fileStream);
                    imgshow.Source = writeableBitmap;
                }
                // create a barcode reader instance
                BarcodeReader reader = new BarcodeReader();
                // detect and decode the barcode inside the  writeableBitmap
                var barcodeReader = new BarcodeReader
                {
                    AutoRotate = true,
                    Options = { TryHarder = true }
                };
                Result result = reader.Decode(writeableBitmap);
                // do something with the result
                if (result != null)
                {
                    txtDecoderType.Text = result.BarcodeFormat.ToString();
                    txtDecoderContent.Text = result.Text;
                }
                
            }
            
        }

        private async void InitializeWebCam(object sender, RoutedEventArgs e)
        {
            Dispose();
            await InitializeCameraAsync();
        }
    }
}
