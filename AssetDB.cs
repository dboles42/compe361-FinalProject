using assets;
using Database;

namespace DBase
{
   public class AssetDB
    {
        public void AddToDB(Asset asset)
        {
            var db = new AssetDataDataContext();
            var table = new AssetInv();

            table.IDNumber = asset.IDnumber;
            table.Category = asset.Category;
            table.Name = asset.Name;
            table.Price = asset.Price;
            table.ItemDesc = asset.Description;
            table.Room = asset.Room;
            table.SerialNumber = asset.SerialNumber;
            table.Supplier = asset.Supplier;
            table.Area = asset.Area;
            table.InfoLink = asset.InfoLink;
            table.ModelNumber = asset.ModelNumber;
            table.DateIn = asset.Date;
            table.CalibDate = asset.CalibrationDate;
            table.UnitValue = asset.UnitValue;
            table.Condition = asset.Condition;

            db.AssetInvs.InsertOnSubmit(table);
            db.SubmitChanges();
        }
    }
}
