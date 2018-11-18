# Notification
Email notification service using Sendgrid and Quartz for scheduling task. Service can work on the standalone server, without any connection with your system.

In folder Database -> Seed -> NotificationQueueSeed need to change the email address where you can check is Service working on your environment.

Run migrations: Update-Database to create Database, Table and some rows from Database Seed.

Insert you Sendgrid Token in appsettings.json

Run the project.
