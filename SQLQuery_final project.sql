create database Drive;
use Drive 
Go

create table Users
(UserId int primary key identity(1,1),
Username nvarchar(60) ,
User_Password nvarchar(30) ,
Created_At smalldatetime);



create table Folders
(Folder_Id int primary key identity(1,1),
Folder_Name nvarchar(60),
Created_By int foreign key references Users(UserId),
Created_At smalldatetime,
isDeleted bit);



create table Documents
(DocumentId int primary key identity(1,1),
Document_Name nvarchar(60),
Document_Type nvarchar(30),
Size int,
Created_By int foreign key references Users(UserId),
Created_At smalldatetime,
Folder_Id int foreign key references folders(Folder_Id),
isDeleted bit);

use Drive
go

insert into Users values
('Varnika', 'varnika', '2022-06-03 15:58:44');