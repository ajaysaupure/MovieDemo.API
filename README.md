# MovieDemo.API (ASP.NET Core Web API)
REST API Movies - Demo 

#More controled way of designing

#More Control on Filte Criteria, Predicate creation responsibility will be on FilterCriteria

#Movie object is business object which carries business rule, AvgRating should be converted to 1 to 5 decimal value which has some Rounding logic, that logic will be in one place and we dont need to worry about ronding the avg Rating everytime we create the business entiry, in future if there any changes in rounding logic, it will be easy to change only at one place

Dependancies 
  None, application will run in Visual Studio (2019)+



Steps to run REST Movie Demo
1) Download/Clone source code from Git

2) To generate LocaDb database from Code First EntityFramework, please follow steps below
      a) Open Package Manager Console and run following command in sequence
      b) enable-Migrations -EnableAutomaticMigrations -Force
      c) add-migration Initial
      d) update-database

3) Build & Run application in Visual Studio

