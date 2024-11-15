# Task Management Application

## Overview

This application is a Task Management system built using **.NET 8** and follows the principles of **Clean Architecture**. The application utilizes several design patterns including the **Repository Pattern**, **Unit of Work Pattern**, and **Specification Pattern** to ensure maintainability, scalability, and flexibility. The backend exposes **REST APIs** to manage tasks, and the front-end is implemented using an **MVC pattern**, consuming these APIs via **HTTP Client**.
## Screenshots

![Screenshot (249)](https://github.com/user-attachments/assets/20d2740d-aab0-4766-beba-e41c656057c6)
![Screenshot (250)](https://github.com/user-attachments/assets/d6bd7b5b-c28f-47f4-beb8-ae2511f02908)
![Screenshot (251)](https://github.com/user-attachments/assets/811b67a5-50bc-4699-8a56-4f5db337846f)
![Screenshot (252)](https://github.com/user-attachments/assets/38cec5d7-d97f-4d50-b152-f456f5251bcb)


## Technologies Used

- **.NET 8** - for backend development
- **ASP.NET MVC** - for front-end and consuming APIs
- **HTTP Client** - to communicate with backend APIs from the MVC view
- **Repository Pattern** - to abstract data access logic
- **Unit of Work Pattern** - to manage transactions
- **Specification Pattern** - for query filtering and pagination
- **Entity Framework Core** - for database interaction
- **Swagger** - for API documentation

## Architecture

The application follows a **n-tier architecture** that includes:

1. **Presentation Layer**: The front-end MVC application that interacts with users, collects inputs, and displays data. It consumes the backend APIs using **HTTPClient** to perform CRUD operations on tasks.
2. **Business Logic Layer**: Contains the business logic and core functionality of the application, such as validation, task management, and interaction with repositories.
3. **Data Access Layer**: Implements the **Repository Pattern** to abstract all data access logic, and the **Unit of Work Pattern** to handle transactions and coordinate the operations across repositories.
4. **API Layer**: Exposes the backend functionality as a REST API, which is consumed by the MVC frontend.

## Features

- **CRUD Operations**: Create, Read, Update, and Delete tasks.
- **Pagination**: Get paginated results for task lists.
