// Код создания БД

create database [Trade]

use [Trade]

create table [Role]
(
	ID int primary key identity,
	[Name] nvarchar(100) not null
)

create table [User]
(
	ID int primary key identity,
	Surname nvarchar(100) not null,
	[Name] nvarchar(100) not null,
	Patronymic nvarchar(100) not null,
	[Login] nvarchar(max) not null,
	[Password] nvarchar(max) not null,
	[Role] int foreign key references [Role](ID) not null
)

create table ProductCategory
(
	PCID int primary key,
	PCName nvarchar(max) not null
)

create table Product
(
	ArticleNumber nvarchar(100) primary key,
	[Name] nvarchar(max) not null,
	Unit nvarchar(max) not null,
	Cost decimal(19,4) not null,
	MaxDiscountAmount tinyint null,
	Manufacturer nvarchar(max) not null,
	Supplier nvarchar(max) not null,
	Category int foreign key references ProductCategory(PCID),
	DiscountAmount tinyint null,
	QuantityInStock int not null,
	[Description] nvarchar(max) not null,
	Photo nvarchar(max) null,
)

create table PickupPoints
(
	PPID int primary key,
	Address nvarchar(max) not null
)

create table [Status]
(
	ID int primary key,
	[Name] nvarchar(max) not null
)

create table [Order]
(
	ID int primary key identity,
	OrderDate datetime not null,
	DeliveryDate datetime not null,
	PickupPoint int foreign key references PickupPoints(PPID) not null,
	Client int foreign key references [User](ID) null,
	Code nvarchar(max) not null,
	[Status] int foreign key references [Status](ID) not null
)

create table OrderProduct
(
	ID int foreign key references [Order](ID) not null,
	ArticleNumber nvarchar(100) foreign key references Product(ArticleNumber) not null,
	Quantity int not null,
	Primary key (ID,ArticleNumber)
)
