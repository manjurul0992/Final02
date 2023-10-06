create database Final02
go
use Final02
go
Create table Formats
(
FormatId int primary key identity,
FormatName nvarchar(100)
)
go
insert into Formats values ('Test Match')
insert into Formats values ('ODI Match')
insert into Formats values ('T20 Match')
Create table Players
(
PlayerId int primary key identity,
PlayerName nvarchar(100) not null,
DateOfBirth datetime ,
Phone nvarchar(100) not null,
Picture nvarchar(max),
MaritalStatus bit 
)
go
Create table SeriesEntry
(
SeriesEntryId int primary key identity,
PlayerId int references Players(PlayerId),
FormatId int references Formats(FormatId),
)
go