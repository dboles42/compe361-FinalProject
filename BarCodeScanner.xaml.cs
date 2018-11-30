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
using ZXing.Rendering;
using Edi.UWP.Helpers;

using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.Foundation;
using Windows.ApplicationModel.Activation;

using InventoryManagement;
using System.IO;
using Windows.Storage.Pickers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.Generic;

namespace InventoryManagement
{

    public partial class BarCodeScanner : Page
    {
        // Provides functionality to capture the output from the camera
        public MediaCapture _mediaCapture;

        // This object allows us to manage whether the display goes to sleep 
        // or not while our app is active.
        private readonly DisplayRequest _displayRequest = new DisplayRequest();

        // Tells us if the camera is external or on board.
        //private bool _externalCamera = false;

        public BarCodeScanner()
        {
            InitializeComponent();

            // https://msdn.microsoft.com/en-gb/library/windows/apps/hh465088.aspx
            //Application.Current.Resuming += Application_Resuming;
            //Application.Current.Suspending += Application_Suspending;
        }

        private void Application_Suspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            Dispose();
            deferral.Complete();
        }

       // private async void Application_Resuming(object sender, object o)
       // {
            //test
      //  }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //await InitializeCameraAsync();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Dispose();
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

        private void InitializeWebCam(object sender, RoutedEventArgs e)
        {
            Dispose();
            //await InitializeCameraAsync();
        }

        public async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //QR code conversion from jepg and return string.
            Asset A2 = new Asset("David's phone", "iPhone 7s", "Mobile Devices", "70", "Room 0xFF", "8/31/1996", "Apple", "8/31/2018",
                                        700, "Simply wonderful", 700, 22, 1234, "www.apple.com", false);

            ZXing.IBarcodeWriter writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,//Mentioning type of bar code generation
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 300,
                    Width = 300,
                },
           
            };
            var result = writer.Write(A2.IDnumber.ToString());
            var wb = result as Windows.UI.Xaml.Media.Imaging.WriteableBitmap;

            //Saving QRCode Image as jpg
            var localFolder = KnownFolders.SavedPictures;
            string filename = A2.IDnumber.ToString() + ".png";
            var file = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            using (var ras = await file.OpenAsync(FileAccessMode.ReadWrite, StorageOpenOptions.None))
            {
                WriteableBitmap bitmap = wb;
                var stream = bitmap.PixelBuffer.AsStream();
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, ras);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight, 150, 150, buffer);
                await encoder.FlushAsync();
            }


        }
    }



}
