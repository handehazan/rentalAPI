Short-Term Stay API
Project Overview

The Short-Term Stay API is a cloud-hosted web api, designed for a short-term stay company. It provides functionalities for hosts, guests, and admins, enabling them to manage listings, book stays, and oversee platform activities.
This project is built using .NET 8 Web API with Azure for cloud hosting and database management.

Find the source code of the project on GitHub: https://github.com/handehazan/rentalAPI.

Find the swagger for the API: [rentalhouseapi-ezf3aza8g4eja7ay.canadacentral-01.azurewebsites.net 
](https://rentalhouseapi-ezf3aza8g4eja7ay.canadacentral-01.azurewebsites.net/index.html)

Design Overview
The application follows a clean architecture approach, separating concerns into layers:
    Controllers: Handle API requests and responses using DTOs.(ListingController, StayController, ReviewController, AuthenticationController)
    Services: Implement business logic and interact with the data access layer. (ListingService, StayService, ReviewService, UserService)
    Data Access: Encapsulates database operations.(ListingAccess, StayAccess, ReviewAccess, UserAccess),(ApplicationDbContext)

Representative class diagram:
        ![resim](https://github.com/user-attachments/assets/c48f3a2d-4455-4b99-9ecf-59d1be5d7496)
The API uses JWT for authentication and authorization. JWT ensures that only authenticated users with valid tokens can access protected resources, with role-based permissions implemented for access control.
Users table for testing:(since there is no registration end point, use only these users when testing also need to look for the roles since it uses roles for authorization)
    ![resim](https://github.com/user-attachments/assets/5916c7b7-3394-4851-8cc1-b6a74ca83ee8)


Issues Encountered
    Cloud Database Configuration: Initial Azure SQL setup required precise firewall and connection string configurations, causing deployment delays.
    Authentication Setup: Implementing role-based authentication across endpoints required careful planning and testing.
    
ER Diagram:
![Ekran görüntüsü 2024-11-29 214406](https://github.com/user-attachments/assets/8a066963-1451-4f16-bba7-b087ab8e6b70)


