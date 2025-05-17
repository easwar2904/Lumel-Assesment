# Lumel Assesment


Step 1 :Create the new project using Console Application Template using (.Net 8 version)
</br>
Step 2 : Navigate to tools --> Nuget Package Manager --> Package manager console
Step 3 : Run the following Comments 
dotnet add package CsvHelper
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.15
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.15
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.15
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0
Step 4 : Run the following tables in your databse


--------------------------MYSql scripts -------------------------------------------------

CREATE TABLE customers(
Id INT NOT NULL AUTO_INCREMENT,
CustomerId VARCHAR(255) NOT NULL,
NAME VARCHAR(255) NOT NULL,
Email VARCHAR(320) NOT NULL, -- 320 is the max email length
Address TEXT NOT NULL,
PRIMARY KEY (Id),
UNIQUE KEY unique_CustomerId (CustomerId));
 
 

CREATE TABLE orderdetails(
  Id INT NOT NULL AUTO_INCREMENT,
  OrderRefId INT NOT NULL,
  OrderId INT NOT NULL,
  ProductRefId VARCHAR(100) NOT NULL,
  ProductId INT NOT NULL,
  QuantitySold INT NOT NULL,
  UnitPrice DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  Discount DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (Id),
  FOREIGN KEY (OrderId) REFERENCES orders(Id) ON DELETE CASCADE,
  FOREIGN KEY (ProductId) REFERENCES products(Id) ON DELETE CASCADE
  );
  



CREATE TABLE orders(
  Id INT NOT NULL AUTO_INCREMENT,
  OrderId VARCHAR(255) NOT NULL,
  DateOfSale DATETIME NOT NULL,
  Region TEXT NOT NULL,
  PaymentMethod VARCHAR(200) NOT NULL,
  Shippingcost DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  CustomerRefId VARCHAR(100) NOT NULL,
  CustomerId INT NOT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY unique_OrderId (OrderId),
  FOREIGN KEY (CustomerId) REFERENCES customers (Id) ON DELETE CASCADE
);



CREATE TABLE products(
  Id INT NOT NULL AUTO_INCREMENT,
  ProductId VARCHAR(255) NOT NULL,
  NAME TEXT NOT NULL,
  Category TEXT NOT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY unique_ProductId(ProductId)
);

--------------------------------------------------------------------------------------------------------
