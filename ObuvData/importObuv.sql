use Obuv

create table Manufacturer
(
manufacturerID int primary key identity,
manufacturerName nvarchar(max) not null
);

create table Provider
(
providerID int primary key identity,
providerName nvarchar(max) not null
);

create table Category
(
categoryID int primary key identity,
categoryName nvarchar(max) not null
);

create table Product
(
productID nvarchar(100) primary key not null,
productName nvarchar(max) not null,
productUnit nvarchar(max) null,
productCost decimal(19,4) not null,
productManufacturer int foreign key references Manufacturer(manufacturerID),
productProvider int foreign key references Provider(providerID),
productCategory int foreign key references Category(categoryID),
productMaxDiscountAmount tinyint null,
productActiveDiscountAmount tinyint null,
productQuantityInStock int not null,
productDescription nvarchar(max) null,
productPicture nvarchar(max) null,
);

create table StaffRole
(
	roleID int primary key identity,
	roleName nvarchar(100) not null
)
go
create table Customer
(
	userID int primary key identity,
	userSurname nvarchar(100) not null,
	userName nvarchar(100) not null,
	userPatronymic nvarchar(100) not null,
	userLogin nvarchar(max) not null,
	userPassword nvarchar(max) not null,
	userRole int foreign key references StaffRole(roleID) not null
)