

---

# Dublin Bikes - Blazor Client (Assignment 2)

This project is a reactive Blazor WebAssembly application that provides a modern, accessible interface for managing Dublin Bikes station data. It connects to the **V2 CosmosDB API** implemented in Assignment 1.

## 🚀 How to Run the Project

### 1. Configure the API Base URL

The Blazor app needs to know where your API is running.

1. Open the Blazor project in Rider or Visual Studio.
2. Locate `Program.cs`.
3. Update the `BaseAddress` to match your local API URL (usually `https://localhost:7000` or `https://localhost:5001`):
```csharp
builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri("https://localhost:7000/") 
});

```



### 2. Launching the Application

1. Ensure **Assignment 1 API** is running first.
2. Open a terminal in the `DublinBikesBlazor` directory.
3. Run the following command:
```bash
dotnet watch run

```


4. The browser should automatically open to `https://localhost:7xxx`.

---

## ✨ Features Implemented

The following requirements from the Assignment 2 rubric have been fully implemented:

* **Master/Detail View:** * **Master List:** Uses `QuickGrid` for high-performance data rendering.
* **Detail View:** Navigates to a dedicated page showing capacity, coordinates, and real-time status.


* **Search & Filtering:**
* **Text Search:** Live search filtering by station name or address.
* **Status Filter:** Filter stations by "Open" or "Closed" status.
* **Minimum Bikes:** Filter to show only stations with a minimum number of available bikes.


* **Sorting & Paging:** Fully functional column sorting (Name, Available Bikes) and pagination.
* **Station Management:** * **Create:** A functional form to add new stations to the CosmosDB.
* **Update:** Ability to edit existing station details.


* **UI/UX & Accessibility:** * Responsive design using Bootstrap 5 and custom CSS.
* Accessible navigation with ARIA labels and semantic HTML.
* High-contrast theme with clear visual feedback for user actions.



---

## 📸 Screenshots

https://github.com/user-attachments/assets/3e787172-ce58-4680-80f9-976d8e68fc5e

### 1. Home Page (Hero Section)

<img width="1906" height="809" alt="Image" src="https://github.com/user-attachments/assets/7b5e788f-de4c-4ed8-af85-e79068e09bb4" />

*Professional landing page with responsive navigation.*

### 2. Master List (QuickGrid with Filters)

<img width="1342" height="706" alt="Image" src="https://github.com/user-attachments/assets/663fd725-26cd-4df8-b339-63b984bc3c42" />

*The station list showing live availability, sorting, and search functionality.*

### 3. Station Detail View

<img width="523" height="578" alt="Image" src="https://github.com/user-attachments/assets/ff2f4df3-dd29-4974-a298-951153a45ccd" />

*Detailed breakdown of a specific station, including capacity and location.*

### 4. Create/Edit Station Form

<img width="528" height="717" alt="Image" src="https://github.com/user-attachments/assets/aa228925-e241-408b-bca2-51c12a42f7cc" />

*Validation-enabled form for managing CosmosDB station records.*

---

## 🛠️ Configuration Troubleshooting

* **SSL Errors:** If you see "SSL connection could not be established," run:
`dotnet dev-certs https --trust`
* **CORS Errors:** Ensure your API's `Program.cs` includes:
```csharp
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

```
