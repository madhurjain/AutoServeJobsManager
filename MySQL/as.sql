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


INSERT INTO `customer` (`CustomerID`,`CustomerName`,`ContactPerson`,`Address`,`Phone`,`MobileNo`) VALUES 
 (1,'Yogesh Deodhar','Yogesh','5,Amruta Apt.Tilak Lane,Ratnagiri-415612','','9922034848'),
 (2,'Wishveshwar Vidymandir','','Gawade -Ambere','','9423875703'),
 (3,'S.P.Nachankar','','Ratnagiri','227502','9422431244'),
 (4,'Computer Concepts','Yogesh','Ratnagiri','','9422056929'),
 (5,'R.M.C.E.T.','Mr.Shelar','At Post Ambav Tal .Sangameshwar ,Ratnagiri','','9421440640'),
 (6,'Comptel System','','Arogyamandir Ratnagiri','','9422050762'),
 (7,'Bank OF India,Purnagad','Mr.Vedak','Purnagad','','9423291425'),
 (8,'Nitin Jadhav','','Ratnagiri','','9869601280'),
 (9,'Revenew Society','','Ratnagiri','','9422053378'),
 (10,'Tej Electronics','Sanjay Vasave','Ratnagiri','','9421231024'),
 (11,'Haresh Bhanushali','Haresh','Ratnagiri','222012','9421230004'),
 (12,'Shrikant Fagare','','Ratnagiri','','9423875141'),
 (13,'V.G.Comps','','Ratnagriri','','9422053676'),
 (14,'Auchtel Products','Mr.Paradkar','Ratnagiri','228643','9421229171'),
 (15,'Parveen Solkar','','Ratnagir MIDC','9225719790','9422432258'),
 (16,'Shirish Sawant','','Ratnagiri','224109','9420671805'),
 (17,'Ekata Associates','','Maruti Mandir Ratnagiri','','9422011611'),
 (18,'ST Co. Operative Society','Ghosalkar','Ratnagiri','224478','9403145645'),
 (19,'Sunil HiTech','','Jaigad','','9890011404'),
 (20,'Vijay Patiyani','','Sapuche Tale,Lanja','','9273597568'),
 (21,'Umesh Ashok Ghanvatkar','','Ganpatipule ,Ratnagiri','','9372720072'),
 (22,'Aryan Collection','','Jaigad','','9637178130'),
 (23,'Marine Syndicate','','Ratnagiri','223814',''),
 (24,'Yash Foundation','Rupesh Kambale','Ratnagiri','02352- 270478','9767026272'),
 (25,'Vedika Mulye','','Nachane ,Ratnagiri','','9890983563'),
 (26,'Aniruddha Joshi','','Rajapur','232049','9422433669'),
 (27,'Sameer Sirmokadam','','Ratnagiri','221027','9890313781'),
 (28,'IT World','Praveen','K.C Jain Nagar,Ratnagiri','','8007554208'),
 (29,'Sandeep Patil','','Ratnagiri','229576','9421141359'),
 (30,'Comptech System','','Salvi Stop,Ratnagiri','','9422375696'),
 (31,'Madhyamik Vidyalaya','','Karjuve','','9921580932'),
 (32,'Bridge Infomatics','Rakesh','Ratnagiri','221999','9763130723'),
 (33,'planet I Cyber Cafe','','Congress Bhuvan ,Ratnagiri','','9422430474'),
 (34,'Shalimar Agencies','Anwar Meman','Ratnagiri','','9822588797'),
 (35,'J K Files & Tools','Hrishikesh','Ratnagiri','228621','9860171055'),
 (36,'J K Files India Ltd.','Hrishikesh','Ratnagiri','228621','9860171055'),
 (37,'Shankar Pilankar','','Ratnagiri','',''),
 (38,'Avadut Pawaskar','','Ratnagiri','','9423048724'),
 (39,'Deepak Jogalekar','Deepak','Ratnagiri','02359-243324','9421234246'),
 (40,'Milind Ghadashi','','Ratnagiri','220190',''),
 (41,'J K Files & ( India ) ltd','Harishikesh','','','9860171055'),
 (42,'T.G.Gandhi Vidyalya','','kondgaon ,Ratnagiri','',''),
 (43,'Arts & Commerce College,lanja','Sachin Dafale','Lanja','02351-230558','9960558255'),
 (44,'Shraddha Computers','','Devrukh','','9420156830'),
 (45,'Jain & Jain','Purushottam','Ratnagiri','270777','9226257580'),
 (46,'HTL Integreated System','','Ratnagiri','','9421231958'),
 (47,'Ashish Ghanekar','','Ratnagiri Gokhale naka','','9422738582'),
 (48,'Jaisingh Lohar','','Ratnagiri','','9423807634'),
 (49,'Vinod Golapkar','Vinod','Ratnagiri','270790','9422630613'),
 (50,'Suryakant Pandurang Gorule','Oni ,Rajapur','','','9960329522'),
 (51,'Abhida Enterprises','','Lanja','','9423047908'),
 (52,'Palande Courier','','Congress Bhuvan Naka,ratnagiri','02352-270968',''),
 (53,'Prant Office,Ratnagiri','KEER','Ratnagiri','02352-222422','9422631108'),
 (54,'Guruprasad Computers','Balu','Ratnagiri','','9403509717'),
 (55,'Shree  Xerox','','Ratnagiri','',''),
 (56,'Mandar Computers','','Ratnagiri','','9421140198'),
 (57,'Central Bank OF India','','Ratnagiri ,Jaisthamb','227528/227532',''),
 (58,'Aniruddha Limaye','','Tilak Ali ,Ratnagiri','','9422433717'),
 (59,'Ravindra Mangle','Ravindra','Devrukh','02354-243296','9403565157'),
 (60,'Bank Of Maharashtra','','GJ Collage Branch','223038',''),
 (61,'Bahar Mahakal','','Ratnagiri','','9423817492'),
 (62,'Vinayak Tamhankar','','Pawas','','9403145311'),
 (63,'Gat Shikshan Adhikari ,Sangameshwar','Sharad Bhadale','Sangameshwar','','9423831917'),
 (64,'Melekar','','Rajapur','','9422051494'),
 (65,'Bhatkar','','Ratnagiri','','9096825890'),
 (66,'Mujaffar Solkar','','Jaigad','','9730498981'),
 (67,'Karkare','','Ratnagiri','','9970079744'),
 (68,'Samarth Drug Agencies','','Ratnagiri','','9422431820'),
 (69,'Manoj Ayre','','Ratnagiri','','9822153054'),
 (70,'Umesh Gothivarekar','','Zadgaon ,Ratnagiri','','9423875860'),
 (71,'Rajesh Patil','','Ratnagiri','','9421229456'),
 (72,'Edak','','Jaigad','','9403080014'),
 (73,'Vasundara Enterprises','Samrat Patil','Kuvarbhav,Ratnagiri','','9422004144'),
 (74,'Soham Computer','Jaydeep','Ratnagiri','271724','9226166165'),
 (75,'Rahul Kosubkar','','Ratnagiri','','9423291591'),
 (76,'Sanket Hatiskar','','Ratnagiri','','9766926954'),
 (77,'SV Infotech','Vinay','Maruti Mandir,Ratnagiri','227074','9422433704'),
 (78,'Veda Infotech','Devendra','Ma','','9890983563'),
 (79,'Magnet Solutions','Devendra','Mal Naka,Ratnagiri','','9960505082'),
 (80,'Bapat','','Lanja','','9226263104'),
 (81,'V.R.Joshi','','Ratnagiri','','9420055059'),
 (82,'Akash Printers','','','',''),
 (83,'Navyug Photo Studio','','Rajapur','','9422382061'),
 (84,'Vijay Patil','','sawarda','','9423284172'),
 (85,'Paresh Construction','Prasad Dhulap','Ratnagiri','235442','9881056523'),
 (86,'Adya Computer','','Mahad','','9403094941'),
 (87,'Praveen Printers','','Ratnagiri','','9421141113'),
 (88,'Dayanand Chavan','','Ratnagiri','','9860387514'),
 (89,'Yojak Associates','','Tilak Ali ,Ratnagiri','234173','9970079744'),
 (90,'Senior Geologiest ,GSDA Ratnagiri','','Shere Naka,Ratnagiri','222671',''),
 (91,'Mandar Bhave','','Ratnagiri ,Pushpendra Complex','','9422429335'),
 (92,'Image Photo Studio','','Ratnagiri','','9225217767'),
 (93,'Advait Petrolium','','Ratnagiri','','8087237090');