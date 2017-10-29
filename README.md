# JwtAuthServer

## Authors

* [Adam Stachowicz](https://github.com/Saibamen) <saibamenppl@gmail.com>
* [Grzegorz Chybziński](https://github.com/gregleon)
* [Grzegorz Radziejewski](https://github.com/flgf)

## For add new migrations
 cd .\src\DataAccess

 dotnet ef migrations add [migrationName]

 ### All commends for ef core:
 https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet

 ## [Authorize] attribute workaround

 http://benjamincollins.com/blog/practical-permission-based-authorization-in-asp-net-core/#fnref:bc603b5d0602b8e850f1470fb6f73205:2

## For run seed data
-> Make sure database connection string in JwtDbContext is correct for db which you want to seed

-> Uncomment code in Program.cs located in DataAccess Project

-> Right click on DataAccess Project -> Debug -> Start new instance

-> If no errors data should be seeded

-> Recomment code in Program.cs located in DataAccess Project

## Repository
Git repository: [https://github.com/FingoPraktyki2016/JwtAuthServer/](https://github.com/FingoPraktyki2016/JwtAuthServer/)

## Continious integration
Travis CI
- web: [https://travis-ci.org/FingoPraktyki2016/JwtAuthServer](https://travis-ci.org/FingoPraktyki2016/JwtAuthServer)
- status 
[![Build Status](https://travis-ci.org/FingoPraktyki2016/JwtAuthServer.svg?branch=master)](https://travis-ci.org/FingoPraktyki2016/JwtAuthServer)

