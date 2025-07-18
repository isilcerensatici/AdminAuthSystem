# AdminAuthSystem

This project demonstrates a secure and scalable admin authorization system using ASP.NET Core MVC and Entity Framework Core. It allows only specific users to access the admin panel, while others are redirected to a welcome page.

🔐 Features
- Login and registration system
- Cookie-based admin access control
- Role-based redirection (admin vs. regular user)
- Gallery management (upload, activate/deactivate, update)
- Profile image selection from gallery
- Secure logout mechanism

🧠 How It Works
- When a user logs in, the system checks if their username matches the predefined admin (e.g., `isilcerensatici`)
- If matched, a cookie (`AdminAccess=true`) is created for 60 minutes
- Admin users are redirected to the dashboard; others go to the welcome page
- The cookie is deleted upon logout, ending the session
- The system is designed to support multiple admin roles in the future

🚀 Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server (via EF migrations)
- Razor Views
- Cookie-based session control

📦 Setup Instructions
1. Clone the repository
2. Run `dotnet ef database update` to apply migrations
3. Launch the project with `dotnet run`
4. Default admin username: `isilcerensatici`

🧹 Clean Repository
The following folders and files have been excluded from this repository:
- `bin/`, `obj/` – build artifacts
- `.vs/` – Visual Studio local settings
- `appsettings.json` – contains sensitive connection strings
- `wwwroot/img/gallery/` – personal image content

This ensures the project remains lightweight and secure.

📸 Demo
Screenshots and video demo available on [LinkedIn](https://linkedin.com/in)

Built with ❤️ by Işıl Ceren Satıcı

