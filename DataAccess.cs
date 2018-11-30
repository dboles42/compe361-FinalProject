using System;
using Microsoft.Data.Sqlite;
using System.Data.SqlTypes;
using assets;
using System.Collections.ObjectModel;

namespace DataAccessLibrary
{
    public static class DataAccess
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection DBase =
                new SqliteConnection("Filename=InventoryDB.db"))
            {
                DBase.Open();

                String CreateTableQuery = "CREATE TABLE IF NOT " +
                    "EXISTS AssetInv (" +
                    "IDNumber INT NOT NULL," +
                    "Category VARCHAR(255)," +
                    "Area VARCHAR(255)," +
                    "Room VARCHAR(255)," +
                    "DateIn VARCHAR(255)," +
                    "Supplier VARCHAR(255)," +
                    "CalibDate VARCHAR(255)," +
                    "Price FLOAT NOT NULL," +
                    "Condition VARCHAR(255)," +
                    "UnitValue FLOAT NOT NULL," +
                    "ModelNumber INT NOT NULL," +
                    "SerialNumber INT NOT NULL," +
                    "InfoLink VARCHAR(255))";

                SqliteCommand createTable = new SqliteCommand(CreateTableQuery, DBase);

                createTable.ExecuteReader();
                DBase.Close();
            }
        }

        public static void InsertIntoTable(Asset asset)
        {
            using (SqliteConnection DBase =
                new SqliteConnection("Filename=InventoryDB.db"))
            {
                DBase.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = DBase;

                insertCommand.CommandText = $"INSERT INTO AssetInv VALUES (@IDnumber, @Category, @Area, @Room, @DateIn, " +
                                            "@Supplier, @CalibDate, @Price, @Condition, @UnitValue, " +
                                            "@ModelNumber, @SerialNumber, @InfoLink);";
                insertCommand.Parameters.AddWithValue("@IDnumber", asset.IDnumber);
                insertCommand.Parameters.AddWithValue("@Category", asset.Category);
                insertCommand.Parameters.AddWithValue("@Area", asset.Area);
                insertCommand.Parameters.AddWithValue("@Room", asset.Room);
                insertCommand.Parameters.AddWithValue("@DateIn", asset.Date);
                insertCommand.Parameters.AddWithValue("@Supplier", asset.Supplier);
                insertCommand.Parameters.AddWithValue("@CalibDate", asset.CalibrationDate);
                insertCommand.Parameters.AddWithValue("@Price", asset.Price);
                insertCommand.Parameters.AddWithValue("@Condition", asset.Condition);
                insertCommand.Parameters.AddWithValue("@UnitValue", asset.UnitValue);
                insertCommand.Parameters.AddWithValue("@ModelNumber", asset.ModelNumber);
                insertCommand.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);
                insertCommand.Parameters.AddWithValue("@InfoLink", asset.InfoLink);

                insertCommand.ExecuteReader();

                DBase.Close();
            }
        }

        public static ObservableCollection<Asset> RetriveAllAssets()
        {
            ObservableCollection<Asset> entries = new ObservableCollection<Asset>();

            using (SqliteConnection DBase =
                new SqliteConnection("Filename=InventoryDB.db"))
            {
                DBase.Open();

                Asset asset = new Asset();
                InsertIntoTable(asset);
                Asset asset1 = new Asset("Omar's phone", "iPhone 7s", 37, "Mobile Devices", "70", "Room 0xFF", "8/31/1996", "Apple", "8/31/2018",
                             500.5, "Simply wonderful", 700, 22, 1234, "www.apple.com\\");
                InsertIntoTable(asset1);

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from AssetInv", DBase);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    Asset temp = new Asset();
                    temp.IDnumber = query.GetInt32(0);
                    temp.Category = query.GetString(1);
                    temp.Area = query.GetString(2);
                    temp.Room = query.GetString(3);
                    temp.Date = query.GetString(4);
                    temp.Supplier = query.GetString(5);
                    temp.CalibrationDate = query.GetString(6);
                    temp.Price = query.GetDouble(7);
                    temp.Condition = query.GetString(8);
                    temp.UnitValue = query.GetDouble(9);
                    temp.ModelNumber = query.GetInt32(10);
                    temp.SerialNumber = query.GetInt32(11);
                    temp.InfoLink = query.GetString(12);

                    entries.Add(temp);
                }

                DBase.Close();
            }

            return entries;
        }



    }
}
