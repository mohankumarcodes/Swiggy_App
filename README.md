# Swiggy_App
Swiggy-App is a Web API built with ASP.NET Core that allows users to place, manage, and track food orders. It includes features like webhook notifications, email alerts, and database integration using Entity Framework Core and SQL Server.

Overview
The Swiggy-App API is a .NET Core Web API that manages orders and notifies external services via webhooks or email notifications.

Features
	• CRUD Operations on Orders (Create, Read, Delete)
	• Webhook Notifications for real-time order updates
	• Email Notifications if webhook fails
	• Database Integration using Entity Framework Core & SQL Server
	
Tech Stack
	• Backend: ASP.NET Core Web API
	• Database: SQL Server (via Entity Framework Core)
	• Logging: Console logging for debugging
	• Email Service: SMTP (MailKit)
	• Webhook Integration: HTTP Client
	
Installation & Setup
1️⃣ Prerequisites
	• .NET 6.0 or later
	• SQL Server
	• Postman or Swagger UI for testing
	
2️⃣ Clone the Repository
   git clone https://github.com/your-repo/swiggy-app.git
   cd swiggy-app
3️⃣ Configure Database
  Update appsettings.json with your SQL Server connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=SwiggyDB;Trusted_Connection=True;"
}
  Run Migrations : dotnet ef database update

4️⃣ Configure Webhook (Optional)
Set up the Webhook URL in appsettings.json:

"WebhookSettings": {
  "OrderWebhookUrl": "https://your-webhook-url.com"
}
5️⃣ Configure Email SMTP (Gmail Example)

"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "Port": 587,
  "SenderEmail": "your-email@gmail.com",
  "SenderPassword": "your-app-password"
}
	Note: Use an App Password instead of your actual email password if using Gmail.
	
6️⃣ Run the Application
    dotnet run

API Endpoints
📌 Order Endpoints

Method	Endpoint	Description
  GET	/api/order	Get all orders
  POST	/api/order	Create a new order
  DELETE	/api/order/{id}	Delete an order
		
📨 Email/Webhook Notifications
  	• Webhook is triggered on Order Creation
  	• If webhook fails, email notification is sent
	
Troubleshooting
    ❌ AuthenticationException: 534 5.7.9 Application-specific password required
    ✔ Solution: Enable App Password for Gmail SMTP.
    ❌ 404 Not Found - Order with ID not found

✔ Solution: Ensure the order exists in the database using:
    SELECT * FROM Orders WHERE Id = 15;

License
    This project is open-source under the MIT License.
