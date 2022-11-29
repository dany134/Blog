This is a ASP.NET Web API project built with .NET 6 framework.

There are 2 ways to run this app:
One is to Navigate into the Blog.API folder and run the command dotnet run
the second way is using Visual Studio

SQlite DB is located in Blog.API if there are any problems with the database you can delete it and EntityFramework should create a new one when the project starts and seed it with some data I prepared.
In Program.cs file if the line 38 "await dbContext.Database.MigrateAsync();" were to cause any problems you can remove it, it is used to automatically run migrations generated with 
EF.

