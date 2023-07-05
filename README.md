# JwtAuthServer

## Authors

* [Adam Stachowicz](https://github.com/Saibamen)
* [Grzegorz Chybziński](https://github.com/gregleon)
* [Grzegorz Radziejewski](https://github.com/flgf)

## Adding new migration

```
cd ...\src\DataAccess
dotnet ef migrations add [migrationName]
```

### All commands for EF Core

https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet

## [Authorize] attribute workaround

http://benjamincollins.com/blog/practical-permission-based-authorization-in-asp-net-core/#fnref:bc603b5d0602b8e850f1470fb6f73205:2

## Repository

Git repository: [https://github.com/FingoPraktyki2016/JwtAuthServer/](https://github.com/FingoPraktyki2016/JwtAuthServer/)

## Continious integration

Travis CI

- web: [https://travis-ci.org/FingoPraktyki2016/JwtAuthServer](https://travis-ci.org/FingoPraktyki2016/JwtAuthServer)
- status
[![Build Status](https://travis-ci.org/FingoPraktyki2016/JwtAuthServer.svg?branch=master)](https://travis-ci.org/FingoPraktyki2016/JwtAuthServer)
