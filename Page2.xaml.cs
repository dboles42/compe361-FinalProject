using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using COMPE361Project;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InventoryGUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        Inventory i1 = new Inventory();
        
        public Page2()
        {
            this.InitializeComponent();
            i1.AddAsset("Omar's phone", "Good phone", 700, 28, 222222, true);
            i1.AddAsset("David's phone", "Bad phone (it's not an iphone)", 300, 12, 124, true);
            i1.AddAsset("Amack's phone", "Probably really good", 7000.1, 444, 1234, false);
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// simple button that takes user to first page if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page1));
        }

        private void Lists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Asset> listItems = new ObservableCollection<Asset>();

            // GOTO: 
            foreach (Asset A in i1.listOfAssets)
            {
                listItems.Add(A);
            }

            ListView itemListView = new ListView();
            itemListView.ItemsSource = listItems;
           // stackPanel1.Children.Add(itemListView);

        }
    }
}
