CREATE TABLE CarBrands (
    BrandName VARCHAR(50) PRIMARY KEY,
    CountryOrigin VARCHAR(255),
    ManufacturerFactory VARCHAR(255),
    Address VARCHAR(255)
);
CREATE TABLE Cars (
    CarID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255),
    Stamp VARCHAR(50),
    YearProduction INT,
    Colour VARCHAR(50),
    Category VARCHAR(50),
    Price DECIMAL(10, 2),
    FOREIGN KEY (Stamp) REFERENCES CarBrands(BrandName)
);
CREATE TABLE Customers (
    CustomersID INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(255),
    PassportDetails VARCHAR(255),
    Address VARCHAR(255),
    City VARCHAR(255),
    DateOfBirth DATE,
    Gender BOOLEAN
);
CREATE TABLE Employees (
    EmployeeID INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(255),
    Experience INT,
    Salary DECIMAL(10, 2)
);
CREATE TABLE Sales (
    SaleID INT AUTO_INCREMENT PRIMARY KEY,
    CustomersID INT,
    EmployeeID INT,
    CarID INT,
    DateSale DATETIME,
    FOREIGN KEY (CustomersID) REFERENCES Customers(CustomersID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    FOREIGN KEY (CarID) REFERENCES Cars(CarID)
);

-- CarBrands
INSERT INTO CarBrands (BrandName, CountryOrigin, ManufacturerFactory, Address) VALUES
('Toyota', 'Japan', 'Toyota Motor Corporation', '1 Toyota-cho, Toyota City, Aichi Prefecture 471-8571, Japan'),
('Ford', 'USA', 'Ford Motor Company', '1 American Rd, Dearborn, MI 48126, USA'),
('Hyundai', 'South Korea', 'Hyundai Motor Company', '12 Heolleung-ro, Seocho-gu, Seoul, South Korea'),
('Mercedes-Benz', 'Germany', 'Daimler AG', 'Mercedesstraße 137, 70327 Stuttgart, Germany'),
('BMW', 'Germany', 'Bayerische Motoren Werke AG', 'Petuelring 130 D-80788 Munich, Germany'),
('Audi', 'Germany', 'AUDI AG', 'Ettinger Straße, 85057 Ingolstadt, Germany'),
('Chevrolet', 'USA', 'General Motors Company', '300 Renaissance Center, Detroit, MI 48243, USA'),
('Honda', 'Japan', 'Honda Motor Co., Ltd.', '2-1-1, Minami-Aoyama, Minato-ku, Tokyo 107-8556, Japan'),
('Nissan', 'Japan', 'Nissan Motor Co., Ltd.', '1-1, Takashima 1-chome, Nishi-ku, Yokohama-shi, Kanagawa 220-8686, Japan'),
('Volkswagen', 'Germany', 'Volkswagen AG', 'Berliner Ring 2, 38440 Wolfsburg, Germany');

-- Cars
INSERT INTO Cars (Name, Stamp, YearProduction, Colour, Category, Price) VALUES
('Corolla', 'Toyota', 2020, 'Blue', 'Sedan', 20000.00),
('Mustang', 'Ford', 2021, 'Red', 'Sports', 30000.00),
('Elantra', 'Hyundai', 2022, 'White', 'Sedan', 19000.00),
('C-Class', 'Mercedes-Benz', 2023, 'Black', 'Luxury', 40000.00),
('3 Series', 'BMW', 2022, 'Silver', 'Luxury', 41000.00),
('A4', 'Audi', 2023, 'Blue', 'Luxury', 42000.00),
('Impala', 'Chevrolet', 2021, 'Red', 'Sedan', 24000.00),
('Civic', 'Honda', 2022, 'White', 'Sedan', 21000.00),
('Altima', 'Nissan', 2023, 'Black', 'Sedan', 23000.00),
('Passat', 'Volkswagen', 2022, 'Silver', 'Sedan', 22000.00);

-- Customers
INSERT INTO Customers (FullName, PassportDetails, Address, City, DateOfBirth, Gender) VALUES
('John Doe', '123456789', '123 Main St', 'New York', '1980-01-01', TRUE),
('Jane Smith', '987654321', '456 Maple Ave', 'Los Angeles', '1985-02-02', FALSE),
('Alice Johnson', '234567890', '789 Oak St', 'Chicago', '1990-03-03', FALSE),
('Bob Williams', '876543210', '321 Pine St', 'Houston', '1995-04-04', TRUE),
('Charlie Brown', '345678901', '654 Elm St', 'Philadelphia', '2000-05-05', TRUE),
('Diana Davis', '765432109', '987 Willow St', 'Phoenix', '1995-06-06', FALSE),
('Ethan Evans', '456789012', '321 Cedar St', 'San Antonio', '1990-07-07', TRUE),
('Fiona Foster', '654321098', '789 Birch St', 'San Diego', '1985-08-08', FALSE),
('George Green', '567890123', '456 Spruce St', 'Dallas', '1980-09-09', TRUE),
('Hannah Harris', '543210987', '123 Redwood St', 'San Jose', '1975-10-10', FALSE);

-- Employees
INSERT INTO Employees (FullName, Experience, Salary) VALUES
('Alice Johnson', 5, 50000.00),
('Bob Williams', 10, 60000.00),
('Charlie Brown', 3, 40000.00),
('Diana Davis', 7, 55000.00),
('Ethan Evans', 12, 65000.00),
('Fiona Foster', 4, 45000.00),
('George Green', 8, 58000.00),
('Hannah Harris', 11, 62000.00),
('Ivan Ivanov', 6, 52000.00),
('Julia Johnson', 9, 59000.00);

-- Sales
INSERT INTO Sales (CustomersID, EmployeeID, CarID, DateSale) VALUES
(1, 1, 1, '2024-03-31 23:58:34'),
(2, 2, 2, '2024-03-31 23:58:34'),
(3, 3, 3, '2024-03-31 23:58:34'),
(4, 4, 4, '2024-03-31 23:58:34'),
(5, 5, 5, '2024-03-31 23:58:34'),
(6, 6, 6, '2024-03-31 23:58:34'),
(7, 7, 7, '2024-03-31 23:58:34'),
(8, 8, 8, '2024-03-31 23:58:34'),
(9, 9, 9, '2024-03-31 23:58:34'),
(10, 10, 10, '2024-03-31 23:58:34');
