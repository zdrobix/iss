# 🏥 PharmacyHospitalApp System

An internal web application that enables medical staff to place medication-related orders and pharmacy staff to process and resolve them.

## 📌 Overview

This system facilitates communication between medical and pharmacy departments. Admin users manage roles, storages, medication menu and system configuration. Hospital staff can create medication orders; pharmacy staff resolve them.

## 👤 User Roles

### 🔹 Admin
- Manage users, roles and asigns workplaces
- Manage medication menu
- Manage pharmacy storage
- Configure system settings

### 🔹 Hospital Staff
- Place medication orders
- Check the medication menu
- Check the order history related to their activity

### 🔹 Pharmacy Staff
- View and resolve pending orders
- Check the pharmacy inventory

## 🛠 Tech Stack

| Layer      | Technology      |
|------------|-----------------|
| Frontend   | Angular 16.0.0  |
| Backend    | ASP.NET (C#)     |
| Database   | SQL Server |
| Auth       | Role-based  |

---

## 🌱 This app was developed in 3 main phases
[Check planning](https://github.com/zdrobix/iss/blob/master/planification/iterations.md)

[Usecases](https://github.com/zdrobix/iss/blob/master/planification/usecases.md)


## 🚀 Getting Started

### Prerequisites

* [![.NET SDK][DotNetSDK.io]][DotNetSDK-url]
* [![Node.js][NodeJS.io]][NodeJS-url]
* [![Angular][Angular.io]][Angular-url]
* [![Bootstrap][Bootstrap.com]][Bootstrap-url]
* [![SQL Server][SQLServer.io]][SQLServer-url]

### Installation

#### Backend (ASP.NET)

```bash
cd api/
dotnet restore
dotnet build
dotnet run
```

#### Frontend (Angular)
```bash
cd visual/PharmacyHospitalUI
npm install
ng serve --open
```

[DotNetSDK.io]: https://img.shields.io/badge/.NET%20SDK-512BD4?style=for-the-badge&logo=.net&logoColor=white
[DotNetSDK-url]: https://dotnet.microsoft.com/download
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[NodeJS.io]: https://img.shields.io/badge/Node.js-339933?style=for-the-badge&logo=node.js&logoColor=white
[NodeJS-url]: https://nodejs.org/
[SQLServer.io]: https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white
[SQLServer-url]: https://www.microsoft.com/en-us/sql-server
