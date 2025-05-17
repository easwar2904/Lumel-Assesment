
---

# Lumel Assessment

## Project Setup

### Step 1: Create the Project

Create a new project using the **Console Application Template** with **.NET 8**.

### Step 2: Open Package Manager Console

Navigate to:

```
Tools → NuGet Package Manager → Package Manager Console
```

### Step 3: Install Required Packages

Run the following commands in the Package Manager Console:

```bash
dotnet add package CsvHelper
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.15
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.15
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.15
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0
```

---

## Database Setup

### Step 4: Run the Following MySQL Scripts

> Ensure your MySQL database is running and properly configured.

#### 🧾 `customers` Table

```sql
CREATE TABLE customers (
  Id INT NOT NULL AUTO_INCREMENT,
  CustomerId VARCHAR(255) NOT NULL,
  NAME VARCHAR(255) NOT NULL,
  Email VARCHAR(320) NOT NULL, -- 320 is the max email length
  Address TEXT NOT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY unique_CustomerId (CustomerId)
);
```

#### 📦 `products` Table

```sql
CREATE TABLE products (
  Id INT NOT NULL AUTO_INCREMENT,
  ProductId VARCHAR(255) NOT NULL,
  NAME TEXT NOT NULL,
  Category TEXT NOT NULL,
  PRIMARY KEY (Id),
  UNIQUE KEY unique_ProductId (ProductId)
);
```

#### 🧾 `orders` Table

```sql
CREATE TABLE orders (
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
```

#### 📦 `orderdetails` Table

```sql
CREATE TABLE orderdetails (
  Id INT NOT NULL AUTO_INCREMENT,
  OrderRefId INT NOT NULL,
  OrderId INT NOT NULL,
  ProductRefId VARCHAR(100) NOT NULL,
  ProductId INT NOT NULL,
  QuantitySold INT NOT NULL,
  UnitPrice DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  Discount DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (Id),
  FOREIGN KEY (OrderId) REFERENCES orders (Id) ON DELETE CASCADE,
  FOREIGN KEY (ProductId) REFERENCES products (Id) ON DELETE CASCADE
);
```

---

Let me know if you'd like me to turn this into a downloadable `.md` file or add other sections (e.g., CSV loading, API usage, analysis features).
