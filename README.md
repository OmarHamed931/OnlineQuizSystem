# 🧠 QuizSystem API

A modular and scalable quiz management API built with **ASP.NET Core** and **SQL Server**. Ideal for learning assessments, backend architecture practice, and future integration with frontend apps or mobile platforms.

---

## 📦 Tech Stack

- ASP.NET Core 7
- Entity Framework Core (Code-First)
- SQL Server (SQLEXPRESS)
- Swagger / OpenAPI
- xUnit for testing

---

## 🚀 Setup Instructions

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/your-username/QuizSystem.git
cd QuizSystem
```

2️⃣ Update the Connection String
Open appsettings.json and confirm your local SQL Server setup:
```json
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=QuizSystem;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```
if you're using a different SQL Server instance, update accordingly.

3️⃣ Apply Migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
4️⃣ Run the Application
```bash
dotnet run
```
## project structure
```plaintext
QuizSystem/
├── Controllers/         → API endpoints
├── Data/                → DbContext and EF config
├── Models/              → Entity definitions
├── Services/            → Business logic and utilities
├── Program.cs           → App configuration
└── appsettings.json     → Connection string and app config
```




