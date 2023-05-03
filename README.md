# Daily Planner
This repository contains the code for a Web API. The API provides the following features:

## Features
1. Task management.
2. Reports on plans.
3. Multiple notebooks.
4. Reminders 1 hour before task starts.
5. User account management.

## Getting Started
1. Clone repository.
2. Configure EmailSender:
- In env.worker change the following settings to yours (or email me for credentials):  
<pre>
EMAILSENDER__EMAIL=dailyplanner.official@outlook.com  
EMAILSENDER__PASSWORD=ReallyHardPassword7&  
EMAILSENDER__HOST=smtp.office365.com  
EMAILSENDER__PORT=587
</pre>
- In DailyPlanner/Systems/Api/DailyPlanner.Api/appsettings.json change the following settings to yours:
<pre>
"EmailSender": {  
    "Email": "dailyplanner.official@outlook.com",  
    "Password": "ReallyHardPassword7&",  
    "Host": "smtp.office365.com",  
    "Port": 587  
}
</pre>
3. Run "docker-compose build" in terminal.
4. Run "docker-compose up" in terminal.

API: http://localhost:10000  
Swagger: http://localhost:10000/api/index.html  
Web Client: http://localhost:10002  

.NET 7 is used.
