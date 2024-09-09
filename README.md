Restaurant Management System
Overview

This is a Restaurant Management System built using ASP.NET Core Razor Pages with Repository Pattern and N-Tier Architecture. The application enables restaurant staff to manage food categories, food types, menu items, and customer orders efficiently. The system integrates multiple roles, including Admin, Kitchen, FrontDesk, Manager, and Customer, with each role having specific permissions to manage orders and other key tasks.
Features

    Role-Based Access Control: Four distinct roles with specific permissions—Admin, KitchenRole, FrontDeskRole, ManagerRole, and CustomerRole.
    Order Management: Track the status of orders from placement to completion or cancellation.
    Menu Management: Add and organize food categories, food types, and menu items.
    API Integration: Allows flexible interactions with external or internal services.

How the System Works
1. Admin Login

    Admin Credentials:
        Username: admin@localhost.com
        Password: Admin@1

2. Menu Setup

    Step 1: Create Food Categories (e.g., Appetizers, Main Course, Beverages) from the dropdown.
    Step 2: Add Food Types under each category (e.g., Vegan, Non-Vegan).
    Step 3: Add Menu Items under the respective food types. Once menu items are created, they will automatically appear on the restaurant's home page.

3. Roles and Their Functions
Customer Role

    Function: View the menu, place orders, and track order status.

KitchenRole

    Function:
        Access the Manage Orders section to view all placed orders.
        Mark an order as In Process when cooking starts.
        Once the order is ready, mark it as Ready for Pickup.
        Completed orders will no longer appear in the order list.

FrontDeskRole

    Function:
        View orders by status: Cancelled, Completed, Ready for Pickup, and In Process.
        Perform actions like Complete Order, Cancel Order, or Refund Order when customers pick up their food.

ManagerRole

    Function:
        Has access to all functionalities of the KitchenRole, FrontDeskRole, and Admin.
        Can manage orders, update menus, and oversee other roles.

Installation and Setup
Requirements

    .NET Core SDK 6.0+
    SQL Server for the database
    Visual Studio 2022 or another preferred code editor

Steps to Run the Application

    Clone the Repository:

    bash

git clone https://github.com/your-repository/restaurant-management-system.git

How to Use the Application
Login

    Use the admin credentials to log in and set up the menu.

Menu Management

    Food Categories: Add new categories under the Menu Management section.
    Food Types: Under each category, define food types such as vegan, non-vegan, etc.
    Menu Items: Add food items, which will appear on the restaurant home page for customers to view and order.

Order Management

    KitchenRole: Marks orders as In Process or Ready for Pickup.
    FrontDeskRole: Manages order statuses and handles actions like Complete Order, Cancel Order, or Refund Order.
    ManagerRole: Oversees all operations.

Architecture

This system follows the N-Tier Architecture pattern, with a clear separation of concerns:

    Presentation Layer: Razor Pages for UI interactions.
    Business Logic Layer: Handles business operations and rules.
    Data Access Layer: Repository pattern for handling database interactions.
    APIs: Used to interact with external services.

Project Structure

    Controllers: Manages HTTP requests and handles role-specific logic.
    Models: Represents the application’s entities, such as MenuItems, Orders, and Roles.
    Services: Contains business logic and interacts with the repository layer.
    Data: Contains the database context and handles migrations.

Contributing

Contributions are welcome! If you have suggestions or encounter any issues, feel free to submit a pull request or open an issue.
License

This project is licensed under the MIT License.
