# Simple Sts
A Minimalistic TokenServer based on the IdentityServer4 framework. Contains assembled and customized logic for Clients, ApiResources, ApiScopes and UserStore with integration to SQL Server database. Works based on the OAuth standards and can work as a boilerplate code for developers looking to setup a minimalistic TokenServer for development or staging environments.

# What is IdentityServer4?

IdentityServer4 is a .NET Core based framework for creating OAuth Token server. 
The library provides the necessary functionalities for building robust and customizable TokenServers which help in separating the User session and token management while the applications can focus on their actual business logic.
Learn more about IdentityServer4 and implementing the various OAuth Grants using IdentityServer4 at https://referbruv.com/collections/developing-a-securetokenserver-and-authenticating-requests-using-identityserver4

# About the Solution

The solution offers the following:
1. Integration with Database for ClientStores and UserStores
2. Seed Client, ApiScopes and Users which can be changed from database
3. Contains auto-migration and Seeding logic which sets up the Database and adds the initial data
4. Support for Social Logins (Google and Facebook). Just update the ClientId and ClientSecret values in the appsettings file
5. Customized Login page. You can add your content in the placeholder divs inside the View
6. One click Docker deployment - You can deploy and run the solution in Docker with a single command
7. Separated Layers for UI, Core and Contracts

# Technologies Used

1. ASP.NET Core 6
2. Entity Framework Core 6
3. Identity
4. IdentityServer 4
5. JWT Authentication Plugin for Google
6. JWT Authentication Plugin for Facebook
7. Docker

# Getting started

To get started with the solution:

1. Install .NET 6 SDK 
2. Clone the repository in your local directory
3. Navigate to ./SimpleSts.Web directory 
4. Update the ConnectionString in the appsettings JSON file
5. Update the secrets for Facebook and Google plugins

Use the below command to run the application.

```
> dotnet build && dotnet run
```

Once the application starts successfully, navigate to http://localhost:5002/Account/Login and you should see the login page in the browser.


# Docker Deployment (Requires a working Docker setup)

To run the solution in docker:

1. Clone the repository in your local directory 
2. Update the secrets for Facebook and Google plugins similar to the step above
3. Update the connection string in docker-compose.yml file present in the solution root

Run the below command, with Docker running.

```
> docker-compose up --force-recreate --build
```

Once the docker container starts successfully, navigate to http://localhost:5002/Account/Login and you should see the login page in the browser.


![SimpleSts in Action](assets/simpleSts.png?raw=true "SimpleSts solution")

# Issues or Ideas?

If you face any issues or would like to drop a suggestion, ![raise an issue](https://github.com/referbruv/simple-sts/issues/new/choose)


Leave a Star if you find the solution useful. For more detailed articles and how-to guides, visit https://referbruv.com

<a href="https://www.buymeacoffee.com/referbruv" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>