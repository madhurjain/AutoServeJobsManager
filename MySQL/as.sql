CREATE DATABASE IF NOT EXISTS autoserve;
USE autoserve;
GRANT ALL ON autoserve.* TO 'autoserve'@'%' IDENTIFIED BY 'qwerty';
GRANT SELECT ON MySQL.Proc to autoserve@'%';

CREATE TABLE customer (
	CustomerID INT(5) UNSIGNED PRIMARY KEY AUTO_INCREMENT,
	CustomerName VARCHAR(50) NOT NULL,
	ContactPerson VARCHAR(50) DEFAULT NULL,
	Address VARCHAR(255) NOT NULL,
	Phone VARCHAR(15) DEFAULT NULL,
	MobileNo VARCHAR(15) DEFAULT NULL
);

CREATE TABLE jobs (
	ServiceTag INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
	CustomerId INT UNSIGNED NOT NULL REFERENCES customer(CustomerID),
	ArrivalDate DATE NOT NULL,
	PrinterModel VARCHAR(30) NOT NULL,
	PrinterSerial VARCHAR(20) NOT NULL,
	Problem TEXT NOT NULL,
	STATUS VARCHAR(30) NOT NULL,
	DeliveryDate DATETIME DEFAULT NULL,
	WorkInProgress VARCHAR(255) DEFAULT NULL,
	SpareParts TEXT DEFAULT NULL,
	Remarks TEXT DEFAULT NULL	
)AUTO_INCREMENT=3300;

CREATE TABLE SMSQueue(
  NUMBER VARCHAR(15) NOT NULL,
  MESSAGE TEXT NOT NULL
);

DELIMITER $$

CREATE FUNCTION sf_CreateCustomer(CustName VARCHAR(50),ContactPerson VARCHAR(50),Address VARCHAR(255),Phone VARCHAR(15),MobileNo VARCHAR(15)) RETURNS INTEGER
BEGIN
  INSERT INTO `customer` VALUES(NULL,CustName,ContactPerson,Address,Phone,MobileNo);
  RETURN LAST_INSERT_ID();
END$$

DELIMITER ;

DELIMITER $$

CREATE FUNCTION sf_AddJob(CustomerId INT(5),arrivalDate DATE,printerModel VARCHAR(30),printerSerial VARCHAR(20),problem TEXT,status VARCHAR(30),deliveryDate DATETIME,workInProgress VARCHAR(255),spareParts TEXT, remarks TEXT) RETURNS INTEGER
BEGIN
  INSERT INTO `jobs` VALUES (NULL,CustomerId,arrivalDate,printerModel,printerSerial,problem,status,deliveryDate,workInProgress,spareParts, remarks);
  RETURN LAST_INSERT_ID();
END$$

DELIMITER ;