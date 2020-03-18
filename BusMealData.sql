-- Insert App Configuration
INSERT INTO BusMeal.dbo.AppConfiguration (RowGrid,LockedBusOrder,LockedMealOrder) VALUES 
(5,'10:00','05:00')
;

-- Insert User
INSERT INTO BusMeal.dbo.[User] (Username,PasswordHash,PasswordSalt,FirstName,LastName,FullName,GddbId,AdminStatus,LockTransStatus) VALUES 
('user','xHjY87FIEkl8+cJwXAUU6j/TweaNgE8qnsYNkcKMG5gzitzSDXDIe9U05ydSzqTQMbNQgeAeXRms1FzwvpR9/Q==','dEkWBoipcYXK6uWQdqp0Tx2G8p+1J9DpXmsbIngeJIWTGH2QU6+srEMeSIXKhM9P8HueP2Ajt1A02u4F5uwqicu75Q0gzQ+8xSsSEw/PvmvxZX4ue6PCiYjJOIJt73NHl1tp7/H5LclyED4yNtWQlddU5AKKDLNwY1zD96ccB9o=','string','string','string','string',1,0)
;

-- Insert Department
INSERT INTO BusMeal.dbo.Department (Code,Name) VALUES 
('FS','Fresslook')
,('IT','Information & Technology')
,('FL','Fasility')
,('HR','Human Resource')
;


-- Insert MealVendor
INSERT INTO BusMeal.dbo.MealVendor (Code,Name,ContactName,ContactPhone,ContactEmail) VALUES 
('0100','Pelangi Katering','Agus Subagio','+62938489811','agus@gmail.com')
,('0200','Citra Sehat','Sugiari','+629883929167','sugiari@yahoo.com')
,('0300','Nawani Katering','Basyanti','+62949493','yangi@gmail.com')
;

-- Inssert MealType
INSERT INTO BusMeal.dbo.MealType (Code,Name,MealVendorId) VALUES 
('0100','Breakfast',1)
,('0200','Lunch',2)
,('0300','Dinner',3)
,('0400','Supper',1)
,('0310','DinnerOT',3)
;

-- Insert Counter
INSERT INTO BusMeal.dbo.Counter (Code,Name,Location,Status) VALUES 
('01','Kantin 201 ','Lantai - 1',1)
,('02','Kanti 201 ','Lantai 2',1)
;

-- Insert Employee
INSERT INTO BusMeal.dbo.Employee (HrCoreNo,Firstname,Lastname,Fullname,HIDNo,DepartmentId) VALUES 
('36006526','Dong','Herti','Dong Herti','2239483',1)
,('36004483','Arhamni','Arhamni','Arhamni','3948292',2)
;

-- Insert Block Domitory
INSERT INTO BusMeal.dbo.DormitoryBlock (Code,Name) VALUES 
('010','Blok-P ')
,('020','Blok-M')
;



-- Insert Bus Time
INSERT INTO BusMeal.dbo.BusTime (Code,[Time],DirectionEnum) VALUES 
('010600','06:00',1)
,('011230','12:30',1)
,('011400','14:00',1)
,('011500','15:00',1)
,('011900','19:00',1)
,('012200','22:00',1)
,('012300','23:00',1)
,('010700','07:00',1)
,('010800','08:00',1)
,('010830','08:30',1)
,('011000','10:00',1)
;
INSERT INTO BusMeal.dbo.BusTime (Code,[Time],DirectionEnum) VALUES 
('021230','12:30',2)
,('021500','15:00',2)
,('021600','16:00',2)
,('021700','17:00',2)
,('021800','18:00',2)
,('021830','18:30',2)
,('021900','19:00',2)
,('022100','21:00',2)
,('022300','23:00',2)
;
INSERT INTO BusMeal.dbo.BusTime (Code,[Time],DirectionEnum) VALUES 
('030330','03:30',3)
,('030400','04:00',3)
,('030430','04:30',3)
,('030500','05:00',3)
,('030600','06:00',3)
,('030630','06:30',3)
,('030700','07:00',3)
,('030030','00:30',3)
,('030300','03:00',3)
;