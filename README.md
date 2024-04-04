# Security-dotNET
[![Build and deploy ASP.Net Core app to Azure Web App - auth-api-almo](https://github.com/AliHMohammad/Security-dotNET/actions/workflows/master_auth-api-almo.yml/badge.svg)](https://github.com/AliHMohammad/Security-dotNET/actions/workflows/master_auth-api-almo.yml)

Authentication & role-based Authorization template in ASP.NET EF Core using JWT. Configured for a MySQL database. The template utilizes two roles, `USER` and `ADMIN`, but that can be easily configured inside `SeedDataAuth.cs`.

Created By [AliHMohammad](https://github.com/AliHMohammad)

#### Deployment
[Deployed on Azure](https://auth-api-almo.azurewebsites.net/swagger/index.html)


## Installation


#### Prerequisites:

* Latest stable [.NET SDK](https://dotnet.microsoft.com/en-us/download)
* [.NET Core CLI-tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

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

Set __AdminPassword__ : No requirements are set, but choose one wisely! :). The default username for admin is `admin`

```
dotnet user-secrets set "AppSettings:AdminPassword" "ADMIN_PASSWORD"
```

#### 3. Build a migration

```
dotnet ef migrations add {NAME_OF_YOUR_CHOOSING}
```


#### 3. Run the application:


```
dotnet run 
```





## Docker

The following are the admin-credentials for your docker-container:

```
username: Admin
password: admin
```

If you wish to change the password, set the user-secret for `AppSettings:AdminPassword` and build a new migration before 4. Boot it up. See the steps above in the Installation section.

#### Prerequisites:

* [Docker Desktop](https://www.docker.com/products/docker-desktop/)

#### 1. Clone the repository:

```
git clone https://github.com/AliHMohammad/Security-CSharp.git .
```

#### 2. Create an .env file in project root:

```
touch .env
```

#### 3. Populate the .env with the following:

```
MYSQL_ROOT_PASSWORD=(MYSQL root password)
SECRET_STRING=(Secret string used in the hash algorithm)
```

#### 4. Boot it up

```
docker compose up -d
```

## Documentation

Navigate to `/swagger/index.html` for Swagger Documentation or click on the deployment link above.

