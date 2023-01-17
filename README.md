# _Pierre's Treats_

#### By _Robert Onstott_

#### _Web-based retail application for helping the enterprising baker Pierre organize what Treats he has on offer, and the categories of Flavors that they fit into. Through a many-to-many relationship, you can see all treats that fit into a given Flavor category, and you can see all of the Flavors that are associated with a given treat. Anyone can read the site, but only registered users can edit the site to change Pierre's offerings._

## Technologies Used

* _C# and .NET 6.0_
* _ASP.NET core MVC_
* _Entity Framework Core_
* _MySQL community server_
* _MySQL Workbench_
* _VS Code_
* _Github_

## Description

_This is a C# web app built with the ASP.NET Core MVC web development framework. It utilizes Entity Framework Core for mapping C# object classes into SQL database schemas. For the purposes of development, the database is hosted through a local server using MySql. The project uses the Identity Authentication and Authorization feautres provided through the ASP.NET 'Identity' namespace in order to create user accounts with password authentication. Any user can read the offerings on the website, but only logged-in users can make edits to the website. _

## Setup/Installation Requirements

* _Download the .NET framework if you do not already have it (version 6.0 or later)_
 
  _https://dotnet.microsoft.com/en-us/download/dotnet_
  
* _Download MySQL Community Server and MySQL Workbench if you don't already have them (both from this link). Make note of both the `User ID (UID)` and the `Password (PWD)` that you define in your setup configurations for MySQL. These values will go into your `appsettings.json` file in a few steps_
  
    _https://dev.mysql.com/downloads/_
  
* _Clone this repository to your machine_

* _Open the repository (VS Code is recommended) and create a new file in the root directory called `appsettings.json`. Copy the following code into the file, with your own values for `uid` and `pwd`_
 
 
  
  ```
       {
         "ConnectionStrings": {
           "DefaultConnection": "Server=localhost;Port=3306;database=pierres_treats;uid=[***your user ID here ***];pwd=[*** your password here *** ];"
         }
       }      
  ```
  
  
  
* _Entity Framework Core has tools to automatically build the database schema utiliaing object mapping. To build the database, open a terminal, go to the `PierresTreats` directory: `$ cd PierresTreats` and enter the following commands_

```
    $ dotnet ef database update   
```

* _While still in the `PierresTreats` directory, enter the command `$ dotnet run`. This will start the local web server. Enter the URL https://localhost:5001 in a browser window. You are now interacting with the web app_
  
  ```
        $ dotnet run
  ```  
  
  _https://localhost:5001_
  
 * _If the localhost:5001 is not loading, you may need to authenticate the https certificate by entering the admnistrator password_

## Known Bugs

* _No known bugs at this time_

## License

_MIT_

_Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:_

_The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software._

_THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE._

Copyright (c) January 2023_ _Robert Onstott_
