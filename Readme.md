## .NET 6 is required to run the app.
## Navigate to Blog.API folder and run the app.
## `dotnet run`
## Another way to run the app is with Visual Studio.
## The app will start on port http://localhost:5000
#### http://localhost:5000/swagger for API documentation
#### The SQLite DB is located in the Blog.API folder named blogs if 
#### it were to cause any problems you can delete it and start the project
#### EntityFramework should create the DB on start and seed it with some init data.
##### If the line  *await dbContext.Database.MigrateAsync();* in Program.cs file causes problems you can remove it, 
##### it is used to automatically run EF core migrations.
###### On the CQRS branch I have used the CQRS pattern with MediatR
###### on the master branch I have used the repository/service pattern.

