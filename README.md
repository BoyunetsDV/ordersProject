# ordersProject
.Target Framework - .Net Core 2.2.  Project type: Web Aplication(MVC)

First step: Load project

Next:
1) Launch project in Visual Studio. 
2) In Package Manager Console execute "Add-migration initial" (if migration is absent)
3) Run solution. In IIS site runs on: http://localhost:54843

Or:
1) Open folder project (ordersProject), here run "dotnet ef migrations add initial" (if migration is absent)
2) Run "dotnet run". By default site runs on: http://localhost:5000

default url can be changed in Properties/launchSettings.json
