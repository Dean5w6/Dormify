# Dormitory Management System (Dormify)

This Dormitory Management System was developed as the final project for the **Fundamentals of Database Systems** course. The primary goal was to apply core database concepts—such as relational schema design, normalization, SQL querying, and data integrity—to build a practical, real-world application.

The system is a comprehensive desktop application designed to simplify and automate the management of residential dormitories. It replaces manual, paper-based processes with a reliable and centralized digital solution, providing administrators with the tools to efficiently manage rooms, tenants, bookings, and finances.

---

## Key Features

-   **User Role Management**: A secure login system for different user roles (Admin, Dorm Manager, Tenant) with distinct, role-based permissions.
-   **Enhanced Security**: Implements password hashing (SHA-256) to ensure user credentials are never stored in plain text, protecting sensitive user data.
-   **Room & Dormitory Management**: Easily add, view, and update details for multiple dormitories and individual rooms, including capacity, rent rates, and maintenance status.
-   **End-to-End Booking System**: A transaction-like process where tenants can request to book rooms, managers approve/reject requests, and the system automatically updates room occupancy.
-   **Automated Billing & Payment Tracking**: The system generates bills and tracks payments against each invoice, utilizing one-to-many relationships to ensure data integrity.
-   **Dynamic Reporting**: Utilizes **SAP Crystal Reports** to generate printable and exportable reports for billings, payments, tenant summaries, and financial overviews.
-   **Centralized Dashboard**: An at-a-glance view of room availability and key metrics, powered by complex SQL queries that join multiple tables.
-   **Activity Logging**: A secure log tracks important actions performed by administrative users for accountability and auditing.

---

## Technology Stack

-   **Language**: VB.NET
-   **UI Framework**: Windows Forms (.NET Framework)
-   **Database**: MariaDB
-   **Reporting**: SAP Crystal Reports for Visual Studio
-   **Local Server Environment**: **XAMPP** (providing Apache and MariaDB)

---

## Database Schema

The system is backed by a robust relational database designed to ensure data integrity through primary keys, foreign keys, and appropriate data types. The schema was designed to be in at least Third Normal Form (3NF) to reduce data redundancy and improve consistency.

![ERD Diagram](path/to/your/erd_image.png)
*(Replace the path above with the actual path to your ERD image in the repository)*

---

## Getting Started

To run this project on your local machine for evaluation, follow these steps:

### Prerequisites

-   Visual Studio (2019 or later recommended)
-   **XAMPP Control Panel** (which includes the required MariaDB database and Apache server)
-   **SAP Crystal Reports for Visual Studio** (the runtime needs to be installed for reports to work)
-   A database management tool like HeidiSQL or the phpMyAdmin included with XAMPP.

### Installation

1.  **Clone the repository:**
    ```sh
    git clone https://your-repository-url.git
    ```
2.  **Set up the database:**
    -   Start the Apache and MySQL services from the XAMPP Control Panel.
    -   Using phpMyAdmin (usually at `http://localhost/phpmyadmin`) or another tool, create a new database named `dormitory_db`.
    -   Import the provided `dormitory_db.sql` file into the new database to set up the tables and sample data.
3.  **Configure the connection string:**
    -   Open the project solution in Visual Studio.
    -   Locate the module or class where the database connection string is defined.
    -   Update the connection string with your server details (the default for XAMPP is usually `Server=localhost; Uid=root; Pwd=;`).
4.  **Build and run:**
    -   Build the solution in Visual Studio (Build > Build Solution).
    -   Run the project by pressing F5 or clicking the "Start" button.
