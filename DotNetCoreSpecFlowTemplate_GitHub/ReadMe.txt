
1- Add "SpecFlow For Visual Studio" tool into your Visual Studio by 
	Tools -> Extensions and Updates -> Search Spec Flow -> Install
	if you have never used Spec Flow on your Visual Studio before

2- Project name must conform with QA code standards and must be Projectname.UITests otherwise you have to modify .sh file by writing project name

3- Update the NuGet Packages: On Solution Explorer -> Dependencies -> Manage Nuget Packages -> Search and  Update
	a. SpecFlow
	b. SpecFlow.MsTest
	c. SpecFlow.Tools.MsBuild.Generation
	d. Selenium Webdriver
	e. Selenium.Webdriver.ChromeDriver
	f. DotNetSeleniumExtras.PageObjects.Core
	g.
	h. SpecSync.AzureDevOps.SpecFlow.3-3 (numbers at the end changes according to version of SpecFlow libraries)
	i. 
	j. Selenium.WebDriver.IEDriver
	k. ExtentReports.Core
	l. Other NuGet packages depending on your project

4- On Solution Explorer -> References ->Add reference-> Assemblies->System.Configuration for App.Config to work properly

5- Move dockerfile_specflow and publishTestResults.sh from the project level to QA folder in Git project

6- Update the chromedriver version as your local chrome version and update chrome ubuntu version corresponding to the local version in dockerfile_specflow.
   Check available UBUNTU CHROME versions here: https://www.ubuntuupdates.org/package/google_chrome/stable/main/base/google-chrome-stable
   If you skip this step tests will run in docker but not in your local Visual studio

7- Create an new test suite so that specsync can push all the scenarios into this suite creating a TC for each scenario, use the ID of the test suite in specsync.json

8- Update the values of project url, test suite id and the token in specsync.json   
	"projectUrl": "yourTfsProjectURL",
    "user": "",  
    "testSuite": {
      "id": 1
	"filePath": "YourProject.UITests.csproj"

9- Update the AppConfig.json  To read data from this file use the method GetJsonConfigurationValue() in AutomationFoundaionDotNetCore

10- Use the command below to install specsync on your machine 
	dotnet tool install SpecSync.AzureDevOps

11- Run the command below to sync (create or update) the scenarios in feature files as TC's in TFS. 
	Command must be run at the same level with the spscsync.json file.
	After the test cases created the TC numbers will be written back into the feature files as tags

	specsync push

