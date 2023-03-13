create database Pet

-- Создание таблицы "Роли пользователей"
CREATE TABLE UserRoles (
    UserRoleId INT IDENTITY(1,1) PRIMARY KEY,
    UserRoleName NVARCHAR(50) NOT NULL
);

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
	UserLastname NVARCHAR(50) NOT NULL,
	UserPatronimyc NVARCHAR(50) NOT NULL,
    UserEmail NVARCHAR(50) NOT NULL,
	UserPhoneNumber NVARCHAR(50) UNIQUE NOT NULL,
    UserPassword NVARCHAR(50) NOT NULL,
    UserRole INT NOT NULL,
    CONSTRAINT FK_Users_UserRoles FOREIGN KEY (UserRole) REFERENCES UserRoles(UserRoleId)
);

-- Создание таблицы "Категории услуг"
CREATE TABLE ServiceCategories (
    ServiceCategoryId INT IDENTITY(1,1) PRIMARY KEY,
    ServiceCategoryName NVARCHAR(50) NOT NULL
);

-- Создание таблицы "Услуги"
CREATE TABLE Services (
    ServiceId INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName NVARCHAR(50) NOT NULL,
    ServicePrice DECIMAL(10,2) NOT NULL,
	ServiceDiscount NVARCHAR(50) NULL,
	ServiceImage VARBINARY(MAX) NOT NULL,
    ServiceCategoryId INT NOT NULL,
    CONSTRAINT FK_Services_ServiceCategories FOREIGN KEY (ServiceCategoryId) REFERENCES ServiceCategories(ServiceCategoryId)
);

-- Создание таблицы "Заказы"
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    OrderUserId INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    OrderStatus NVARCHAR(50) NOT NULL,
    OrderTotal DECIMAL(10,2) NOT NULL,
    PaymentStatus NVARCHAR(50) NOT NULL,
	CONSTRAINT FK_Orders_Users FOREIGN KEY (OrderUserId) REFERENCES Users(UserId)
);

-- Создание таблицы "Состав заказа"
CREATE TABLE OrderServices (
    OrderServiceId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ServiceId INT NOT NULL,
    CONSTRAINT FK_OrderServices_Orders FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    CONSTRAINT FK_OrderServices_Services FOREIGN KEY (ServiceId) REFERENCES Services(ServiceId)
);