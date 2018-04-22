# Important details

##  Local SQL Server DB Info for work machine
- Server Name: 
    - [yourWorkMachineName]\MAIN
- SQL Server Login
  - Main Login
    - username: sa
    - password: Admin123

## EF Migrations
- you have to comment out the seed-data class code before you can create a new migration because somehow the dotnet ef commands run the seeddata code before checking if there's already a migration and it causes an error 

### How to start new migration on new computer
- comment out seedData method
- use dotnet ef migrations add (new computer migration name)
- use dotnet ef database update (new computer migration name)
- Create a db connection string in config.json
- replace connection string name in WorldContext.cs
- uncomment your seedData method and run your app


##Angular setup
- moved project from {{angularProjName}}/src/ to {{projectRoot}}/ClientApp
- moved some filese in {{angularProjName}} directory into {{projectRoot}}
-- tsconfig.json, angular-cli.json, and package.json
--- Reconfigured the three files to point to new client app locations
