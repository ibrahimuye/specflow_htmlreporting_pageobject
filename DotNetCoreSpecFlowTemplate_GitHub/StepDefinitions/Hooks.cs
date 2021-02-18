using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using DotNetCoreSpecFlowTemplate.AppPages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace DotNetCoreSpecFlowTemplate.StepDefinitions
{
    [Binding]
    public sealed class Hooks :  BasePage
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly ParallelConfig _parallelConfig;

        private static AventStack.ExtentReports.ExtentReports _extent;
        private static ExtentTest _featureName;
        private static ExtentTest _scenario;

        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext, ParallelConfig parallelConfig)
        {
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
            this._parallelConfig = parallelConfig;
        }

        [AfterStep()]
        public void AfterEachStep()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
            }

            else if (_scenarioContext.TestError != null)
            {
                var mediaEntity =
                    _parallelConfig.CaptureScreenShotAndReturnModel(_scenarioContext.ScenarioInfo.Title.Trim());

                if (stepType == "Given")
                    _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                else if (stepType == "When")
                    _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
            }

            else if (_scenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "And")
                    _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            _extent.Flush();
        }

        [BeforeTestRun]
        public static void TestInitialize()
        {
            var htmlReporter = new ExtentHtmlReporter(Path.Combine(Directory.GetCurrentDirectory(), "ExtentReport.html"));
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            _extent = new AventStack.ExtentReports.ExtentReports();
            _extent.AttachReporter(htmlReporter);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _featureName = _extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            _scenario = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            // above lines for extent report
            // below to show the html reporting location for easy debugging
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Html test report at " + Path.Combine(Directory.GetCurrentDirectory(), "index.html") + " and visible on Team City Test Report tab");

            Driver.GetDriver().Url = GetJsonConfigurationValue("DefaultUrl");
            Driver.GetDriver().Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            Driver.GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            Driver.CloseDriver();
        }

        //this class to create a screen shot of error in html extent report
        public class ParallelConfig
        {
            public MediaEntityModelProvider CaptureScreenShotAndReturnModel(string name)
            {
                var screenShot = ((ITakesScreenshot)Driver.GetDriver()).GetScreenshot().AsBase64EncodedString;
                return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, name).Build();
            }
        }
    }
}
