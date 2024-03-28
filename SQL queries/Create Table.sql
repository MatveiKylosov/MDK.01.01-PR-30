CREATE TABLE CarBrands (
    BrandName VARCHAR(50) PRIMARY KEY,
    CountryOrigin VARCHAR(255),
    ManufacturerFactory VARCHAR(255),
    Address VARCHAR(255)
);
CREATE TABLE Cars (
    CarID INT PRIMARY KEY,
    Name VARCHAR(255),
    Stamp VARCHAR(50),
    YearProduction INT,
    Colour VARCHAR(50),
    Category VARCHAR(50),
    Price DECIMAL(10, 2),
    FOREIGN KEY (Stamp) REFERENCES CarBrands(BrandName)
);
CREATE TABLE Customers (
    CustomersID INT PRIMARY KEY,
    FullName VARCHAR(255),
    PassportDetails VARCHAR(255),
    Address VARCHAR(255),
    City VARCHAR(255),
    DateOfBirth DATE,
    Gender BOOLEAN
);
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FullName VARCHAR(255),
    Experience INT,
    Salary DECIMAL(10, 2)
);
CREATE TABLE Sales (
    SaleID INT PRIMARY KEY,
    CustomersID INT,
    EmployeeID INT,
    CarID INT,
    DateSale DATETIME,
    FOREIGN KEY (CustomersID) REFERENCES Customers(CustomersID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    FOREIGN KEY (CarID) REFERENCES Cars(CarID)
);
