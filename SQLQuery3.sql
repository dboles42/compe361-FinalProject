use InventoryDB;
GO
CREATE TABLE AssetInv(
	RowNum INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	IDNumber INT NOT NULL,
	Category VARCHAR(255),
	Area VARCHAR(255),
	Room VARCHAR(255),
	DateIn VARCHAR(255),
	Supplier VARCHAR(255),
	CalibDate VARCHAR(255),
	Price FLOAT NOT NULL,
	Condition VARCHAR(255),
	UnitValue FLOAT NOT NULL,
	ModelNumber INT NOT NULL,
	SerialNumber INT NOT NULL,
	InfoLink VARCHAR(255)
	);
GO
	--GRANT CREATE DATABASE TO [Fay];