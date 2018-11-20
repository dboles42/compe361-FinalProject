using System;
using System.Collections.Generic;

namespace COMPE361Project
{
    /// <summary>
    /// Class for the inventory
    /// </summary>
    public class Inventory
    {
        private List<Asset> listOfAssets = new List<Asset>();

        /// <summary>
        /// Not sure if we want to do anything here for the constructor
        /// </summary>
        public Inventory()
        {
        }

        /// <summary>
        /// Sorts the inventory based on the unique ID number for each asset
        /// </summary>
        public void SortInventory()
        {
            listOfAssets.Sort();
        }

        /// <summary>
        /// Adds an asset to the inventory
        /// </summary>
        /// <param name="A">A.</param>
        public void AddAsset(Asset A){
            listOfAssets.Add(A);
            listOfAssets.Sort();
        }

        /// <summary>
        /// Removes an asset from the inventory
        /// </summary>
        /// <param name="index">Index of the asset to be removed</param>
        public void RemoveAsset(int index){
            listOfAssets.RemoveAt(index);
        }

        /// <summary>
        /// Clears the inventory.
        /// </summary>
        public void ClearInventory(){
            listOfAssets.Clear();
        }



        /// <summary>
        /// Finds the total value for all assets in the inventory
        /// </summary>
        /// <returns>The total value for the inventory</returns>
        public double FindTotalValue(){
            double TotalValue = 0;
            foreach (Asset A in listOfAssets){
                TotalValue += A.Price;
            }
            return TotalValue;
        }

        /// <summary>
        /// Returns a string that represents the current inventory
        /// </summary>
        /// <returns>A string that represents the current inventory.</returns>
        public override string ToString()
        {
            string s = "";
            foreach (Asset A in listOfAssets)
            {
                s += A.ToString();
                s += "****************************************\n";
            }
            return s;
        }
    }

    /// <summary>
    /// Main class
    /// </summary>
    class MainClass
    {
        public static void Main(string[] args)
        {
            Asset A1 = new Asset("Omar's phone", "iPhone 7s", 5, "Mobile Devices", "70", "Room 0xFF", "8/31/1996", "Apple", "8/31/2018",
                                         500.2, "Simply wonderful", 700, 22, 1234, "www.apple.com");
            Asset A2 = new Asset("David's phone", "iPhone 7s", 2, "Mobile Devices", "70", "Room 0xFF", "8/31/1996", "Apple", "8/31/2018",
                                        700, "Simply wonderful", 700, 22, 1234, "www.apple.com");
            Asset A3 = new Asset("Amack's phone", "iPhone 7s", 4, "Mobile Devices", "70", "Room 0xFF", "8/31/1996", "Apple", "8/31/2018",
                                        700, "Simply wonderful", 700, 22, 1234, "www.apple.com");
            Inventory x = new Inventory();
            x.AddAsset(A1);
            x.AddAsset(A2);
            x.AddAsset(A3);
            Console.WriteLine(x);
        }
    }
}
