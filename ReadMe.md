# Aperture
[![](https://shields.io/codacy/grade/1f4fe55f3aa34c56bf012b149419a37c?logo=Codacy)](https://app.codacy.com/gh/DotNet-Ninja/Aperture/dashboard)
[![Coveralls](https://img.shields.io/coveralls/github/DotNet-Ninja/Aperture?logo=coveralls)](https://coveralls.io/github/DotNet-Ninja/Aperture?branch=main)
[![](https://shields.io/github/workflow/status/Dotnet-Ninja/Aperture/Main%20Build%20Pipeline?logo=github)](https://github.com/DotNet-Ninja/Aperture/actions)
![GitHub last commit](https://img.shields.io/github/last-commit/DotNet-Ninja/Aperture?logo=github) 
[![GitHub](https://img.shields.io/github/license/DotNet-Ninja/Aperture)](https://github.com/DotNet-Ninja/Aperture/blob/main/License.txt)

Photographer's Web Site

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
