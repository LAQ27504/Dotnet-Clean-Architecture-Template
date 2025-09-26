# .NET Clean Architecture Template with Blazor and MudBlazor

This template helps you quickly scaffold a working ASP.NET Core + Blazor + MudBlazor application following Clean Architecture principles.  
It includes a sample CRUD feature and Entity Framework Core integration so you can get up and running fast.

---

## Getting Started

Follow the steps below to create a new project, configure the database, run Entity Framework migrations, and launch your application.

### 1. Install Prerequisites

Install the latest [.NET SDK](https://dotnet.microsoft.com/download).  
Install the EF Core CLI tools if you haven’t already:

```bash
dotnet tool install --global dotnet-ef
```

Set up a PostgreSQL database or adjust the connection string for your own provider.

### 2. Install the Template

Run this command to install the template globally on your machine:

```bash
dotnet new install BundleCleanArchitecture.Templates
```

### 3. Create a New Project

Use the template to create your project:

```bash
dotnet new {template} -n MyProject
cd MyProject
```

Replace `{template}` with template, `MyProject` with your desired project name.
If the template supports options such as authentication you can pass them:

```bash
dotnet new {template} -n MyProject --authen true
```

### 4. Restore Dependencies

After creating the project, restore all NuGet packages:

```bash
dotnet restore
```

### 5. Configure the Database Connection

Open `appsettings.json` or `appsettings.Development.json` in the WebUI project and update the `DefaultConnection` string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword"
}
```

### 6. Create the Initial Database Migration

Run the following command from the root of your solution:

```bash
dotnet ef migrations add InitialCreate --project MyProject.Infrastructure --startup-project MyProject.Presentation
```

`--project` is the project containing your DbContext (MyProject.Infrastructure).  
`--startup-project` is the startup project (MyProject.Presentation).

### 7. Apply the Migration to the Database

```bash
dotnet ef database update -p Infrastructure -s WebUI
```

### 8. Build and Run the Application

Build the solution:

```bash
dotnet build
```

Run the WebUI project:

```bash
dotnet run --project WebUI
```

Your application should now be running at `https://localhost:5001` or `http://localhost:5000`.

### 9. Project Structure

After creation you will have:

```
MyProject/
│
├── Domain/           # Core business logic
├── Application/      # Use cases & services
├── Infrastructure/   # EF Core, database, external services
└── WebUI/            # Blazor/MudBlazor front-end
```

- Add new entities in **Domain**
- Add business logic in **Application**
- Implement data access in **Infrastructure**
- Add pages/components in **WebUI**

### 10. Next Steps

- Customize the template with your own entities and CRUD operations.
- Use MudBlazor components to build rich UI.
- Configure authentication and authorization if needed.

---

## Feedback

Have suggestions or improvements? Feel free to open an issue or contact me on [LinkedIn](https://www.linkedin.com/in/le-anh-quang-93262b300/).
