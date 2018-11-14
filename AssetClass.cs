    using System;
    /// <summary>
    /// Class for assets 
    /// </summary>
    public class Asset : IComparable
    {
        private string Name { get; set; }
        private string Description { get; set; }
        private int IDnumber { get; set; }
        private string Category { get; set; }
        private string Area { get; set; }
        private string Room { get; set; }
        private string Date { get; set; }
        private string Supplier { get; set; }
        private string CalibrationDate { get; set; }
        private double Price { get; set; }
        private string Condition { get; set; }
        private double UnitValue { get; set; }
        private int ModelNumber { get; set; }
        private int SerialNumber { get; set; }
        private string InfoLink { get; set; }

        /// <summary>
        /// Explicit value constructor that Initializes a new instance of the asset class.
        /// </summary>
        /// <param name="Name">Name.</param>
        /// <param name="Description">Description.</param>
        /// <param name="IDnumber">Identifier number.</param>
        /// <param name="Category">Category.</param>
        /// <param name="Area">Area.</param>
        /// <param name="Room">Room.</param>
        /// <param name="Date">Date.</param>
        /// <param name="Supplier">Supplier.</param>
        /// <param name="CalibrationDate">Calibration date.</param>
        /// <param name="Price">Price.</param>
        /// <param name="Condition">Condition.</param>
        /// <param name="UnitValue">Unit value.</param>
        /// <param name="ModelNumber">Model number.</param>
        /// <param name="SerialNumber">Serial number.</param>
        /// <param name="InfoLink">Info link.</param>
        public Asset(string Name, string Description, int IDnumber, string Category, string Area, string Room, string Date,
                    string Supplier, string CalibrationDate, double Price, string Condition, double UnitValue, int ModelNumber,
                     int SerialNumber, string InfoLink){
            this.Name = Name;
            this.Description = Description;
            this.IDnumber = IDnumber;
            this.Category = Category;
            this.Area = Area;
            this.Room = Room;
            this.Date = Date;
            this.Supplier = Supplier;
            this.CalibrationDate = CalibrationDate;
            this.Price = Price;
            this.Condition = Condition;
            this.UnitValue = UnitValue;
            this.ModelNumber = ModelNumber;
            this.SerialNumber = SerialNumber;
            this.InfoLink = InfoLink;
        }

        /// <summary>
        /// Compares two assets based on their ID number
        /// </summary>
        /// <returns>Returns 1 if the current assets ID number is greater than the ID number of the asset
        /// being compared to, -1 if less than, and 0 if equal to</returns>
        /// <param name="obj">Object.</param>
        public int CompareTo(object obj)
        {
            if (!(obj is Asset) || obj == null)
                throw new ArgumentException("The object passed is invalid");
            Asset that = (Asset)obj;                //Cast the object as an Asset
            //Compare the two ID numbers of the two assets
            return this.IDnumber.CompareTo(that.IDnumber);
        }

        /// <summary>
        /// Serves as a hash function for an asset object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current asset.
        /// </summary>
        /// <returns>A string that represents the current asset.</returns>
        public override string ToString()
        {
            return $"name: {this.Name}\nDescription: {this.Description}\nID number: {this.IDnumber}\nArea: {this.Area}\n" +
                $"Room: {this.Room}\nDate: {this.Date}\nSupplier: {this.Supplier}\nCalibration Date: {this.CalibrationDate}\n" +
                $"Price: {this.Price}\nCondition: {this.Condition}\nUnit Value: {this.UnitValue}\nModel Number: {this.ModelNumber}\n" +
                $"Serial Number: {this.SerialNumber}\nInfo Link: {this.InfoLink}\n";
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Asset firstAsset = new Asset("Omar's phone", "iPhone 7s", 5, "Mobile Devices", "70", "Room 0xFF", "8/31/1996", "Apple", "8/31/2018",
                                         500.5, "Simply wonderful", 700, 22, 1234, "www.apple.com");
            Console.WriteLine(firstAsset);
        }
    }