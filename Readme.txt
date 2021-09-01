
Command to start for entity framework core.

	dotnet ef database update --------To create or update database
	dotnet ef migrations add Init-----To create migration script which contains sql
	dotnet ef database update---------If it contains migration script then it will apply to db.
	dotnet ef database drop-----------To drop database


Target Framework
	.net core 3.1


Used packages in project
	AutoMapper.Extensions.Microsoft.DependencyInjection Version 7.0.0
	Microsoft.EntityframeworkCore.SqlServer 5.0.1
	MIcrosoft.AspNetCore.MVC.NewtonsoftJson 3.1.0
	Microsoft.AspNetCore.Identity.EntityFrameworkCore 5.0.0
	Microsoft.AspNetCore.Authentication.JwtBearer 3.1.0
	Jquery
	Bootstrap
	jquery-validation
	jquery-validation-unobtrusive
	fortasesome

Frontend technology	
	Angular
	node --verion -------------node js is required to build our angular application.
	npm --verion -------------- npm is used to install angualr CLI - to build and run angular application.
	npm install @angular/cli -g ---to install npm globally.
	ng new {Name Of Angular Project} 
	ng new --skip-git --skip-tests --minimal --defaults -- dry-run
	
Tools
	Visual Studio 2019
	Vistual STudio Code
	http://json2ts.com/ ------------- convert json data to class

Course to learn .net api in .net core
www.pluralsight.com/courses/building-api-aspdotnet-core



Pushing code to git

	https://www.youtube.com/watch?v=OVL7R0eT8jw
	rd .git /S/Q
	git init
	git add .
	git commit -m "My First Commit"
	git remote add origin https://github.com/maparedev/AngularWithPayU.git
	git push -u origin master

Payu Credentials
	
	Hi ,	
	
	Test Account Created Successfully ,
	 
	Below are the details:
	
	
	Merchant Name: Vivek Mapare
	Merchant Email: Vivek.Mapare@blueconchtech.com
	MID: 4957757
	Merchant key: BWT6ez
	Salt : Up7i5j4L
	
	
	URL to login on dashboard: https://testtxncdn.payubiz.in/login
	Username: super
	Key/Alias: BWT6ez
	Password: payu@1234
	
	
	
	For Salt : Login to the merchant Panel -> My Account -> Click on System Settings -> Take 8-character Salt from here for integration
	For test server, please redirect to (https://test.payu.in/_payment).
	
	
	Below are the test card details for doing a test transaction in the testing mode.
	
	
	Card Name: Any Name
	Card Number: 5123 4567 8901 2346
	CVV: 123
	Expiry: May 2022
	OTP : 123456

