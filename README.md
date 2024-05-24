# Supermarket-CSharp Application

This is a WPF application designed to manage a supermarket's inventory, users, and transactions. The application is built using C#, WPF, MVVM architecture, and SQL Server for the database. It supports functionalities for both administrators and cashiers.

## Table of Contents
- [Features](#features)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Setup and Installation](#setup-and-installation)
- [Usage](#usage)
- [License](#license)

## Features
### Administrator
- Add, modify, delete, and view:
  - Users
  - Product categories
  - Producers
  - Products
  - Stocks
  - Receipts
- Perform logical deletion of records.
- Automatically calculate selling prices based on purchase prices.
- Search functionalities:
  - List products by manufacturer and category.
  - Display total value of products in each category.
  - View sales by user for a selected month.
  - View the highest sales receipt for a selected day.

### Cashier
- Search products by:
  - Name
  - Barcode
  - Expiration date
  - Manufacturer
  - Category
- Issue and view sales receipts:
  - Calculate correct prices and totals.
- Manage stock:
  - Mark stocks as inactive when depleted or expired.

## Project Structure
- **Model**: Contains the data models.
- **ViewModel**: Contains the logic for the application's views.
- **View**: Contains the user interface components.
- **Database**: Contains the SQL Server database and stored procedures.

## Technologies Used
- **C#**
- **WPF**
- **MVVM Architecture**
- **SQL Server**

## Setup and Installation
### Prerequisites
- Visual Studio
- SQL Server

### Steps
1. Clone the repository.
2. Open the solution in Visual Studio.
3. Set up the database:
   - Open SQL Server Management Studio (SSMS).
   - Execute the scripts in the `Database` folder to create the database and tables.
4. Update the connection string in `App.config` to match your SQL Server instance:
    ```xml
    <connectionStrings>
        <add name="SupermarketDbContext" connectionString="your_connection_string" providerName="System.Data.SqlClient" />
    </connectionStrings>
    ```
5. Build and run the application.

## Usage
### Administrator
1. Log in with administrator credentials.
2. Navigate to the desired section (Users, Products, Stocks, etc.).
3. Perform CRUD operations as needed.

### Cashier
1. Log in with cashier credentials.
2. Use the search functionality to find products.
3. Issue sales receipts and manage transactions.

## License
This project is not licensed and comes with no warranty or guarantee of any kind. You are free to view and fork the code for personal and educational purposes.
