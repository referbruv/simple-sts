# Ids4.Sts
A Minimalistic TokenServer based on the IdentityServer4 framework. Contains assembled and customized logic for Clients, ApiResources, ApiScopes and UserStore with integration to SQL Server database. Works based on the OAuth standards and can work as a boilerplate code for developers looking to setup a minimalistic TokenServer for development or staging environments.

The project offers the following:
1. Integration with Database for ClientStores and UserStores
2. Seed Client, ApiScopes and Users which can be changed from database
3. Contains auto-migration and Seeding logic which sets up the Database and adds the initial data
4. Support for Social Logins (Google and Facebook). Just update the ClientId and ClientSecret values in the appsettings file
5. Customized Login page. You can add your content in the placeholder divs inside the View

# What is IdentityServer4?

IdentityServer4 is a .NET Core based framework for creating OAuth Token server. 
The library provides the necessary functionalities for building robust and customizable TokenServers which help in separating the User session and token management while the applications can focus on their actual business logic.
Learn more about IdentityServer4 and implementing the various OAuth Grants using IdentityServer4 here: https://referbruv.com/blog/categories/identityserver4
