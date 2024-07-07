# Aperture
Aperture is a website/blog engine for photographers built on ASP.Net.

## Development Environment SetUp

### Requirements

__SQL Server__ - Any recent version of SQL Server should work.  You can spin one up locally using docker compose.

``` 
version: "3"
services:
  default-sql:
    image: "mcr.microsoft.com/mssql/server:2022-latest"
    ports:
      - 1433:1433
    environment:
      SA_PASSWORD: "{YOUR-PASSWORD}"
      ACCEPT_EULA: "Y"
    volumes:
      - {LOCAL-DATA-PATH}:/var/opt/mssql/data
``` 

You can read more info on running SqlServer in Docker in [my blog post](https://dotnetninja.net/2020/01/running-microsoft-sql-server-in-a-container-on-windows-10/). 

__Auth0__ - You'll need an Auth0 domain.  You can set one up for free (up to 7000 monthly active users).
