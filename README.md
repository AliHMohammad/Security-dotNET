# Security-dotNET
[![Build and deploy ASP.Net Core app to Azure Web App - auth-api-almo](https://github.com/AliHMohammad/Security-dotNET/actions/workflows/master_auth-api-almo.yml/badge.svg)](https://github.com/AliHMohammad/Security-dotNET/actions/workflows/master_auth-api-almo.yml)

Authentication & role-based Authorization template in ASP.NET Core using JWT. Configured for a MySQL database. The template utilizes two roles, `USER` and `ADMIN`, but that can be easily configured inside `SeedDataAuth.cs`.

Created By [AliHMohammad](https://github.com/AliHMohammad)

[Deployed on Azure](https://auth-api-almo.azurewebsites.net/)

## Installation


#### Prerequisites:

* Latest [.NET Framework](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)
* [Dotnet ef](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) tool

#### 1. Clone the repository:

```
git clone https://github.com/AliHMohammad/Security-CSharp.git .
```

#### 2. Set user secrets:

You need to set the following: 

* __ConnectionString__ : Used to establish connection to your MySQL database.
* __TokenSecret__ : The Secret-string used in the hash-algorithm.
* __AdminPassword__ : The password for the admin-user created on startup.

Start by initializing The Secret Manager Tool. Run the following command in the project directory:

```
dotnet user-secrets init
```

Set __ConnectionString__. The project is set to look for "default". Modify the values:

```
dotnet user-secrets set "ConnectionStrings:default" "server=URL;port=PORT;database=SCHEMA_NAME;userid=USERNAME;password=PASSWORD"
```

Set __TokenSecret__. It must be atleast 16 characters in length. You can easily [generate one](https://dev.to/tkirwa/generate-a-random-jwt-secret-key-39j4):

```
dotnet user-secrets set "AppSettings:TokenSecret" "SECRET_STRING"
```

Set __AdminPassword__ : No requirements are set, but choose one wisely! :)

```
dotnet user-secrets set "AppSettings:AdminPassword" "ADMIN_PASSWORD"
```


#### 3. Run the application:


```
dotnet run 
```
Default URL is set to https://localhost:7236



## Documentation

Navigate to https://localhost:7236/swagger/index.html for Swagger Documentation

[<img src="https://run.pstmn.io/button.svg" alt="Run In Postman" style="width: 128px; height: 32px;">](https://app.getpostman.com/run-collection/29408900-e4d666a5-1a9f-4d61-bc17-568392a395de?action=collection%2Ffork&source=rip_markdown&collection-url=entityId%3D29408900-e4d666a5-1a9f-4d61-bc17-568392a395de%26entityType%3Dcollection%26workspaceId%3D61a12064-f02a-4b89-9cf0-b48f6e161f1c)





