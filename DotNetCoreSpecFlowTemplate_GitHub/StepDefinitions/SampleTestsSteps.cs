using System;
using DotNetCoreSpecFlowTemplate.AppPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;


namespace DotNetCoreSpecFlowTemplate.StepDefinitions
{
    // for Context Injection, we create a class and store data at any step and use in another step in the same or another binding class in the same scenario
    // this data can be defined or changed at any step and used in another step
    public class SharedData
    {
        public string data1;
        public int data2;
    }


    [Binding]
    public class SampleTestsSteps : BasePage
    {
        // we create a constructor with shared data object
        private readonly SharedData _sharedData;
        public SampleTestsSteps(SharedData sharedData)
        {
            this._sharedData = sharedData;
        }
        public readonly AllPages Pages = new AllPages();

       

        [When(@"I enter username and password to login")]
        public void WhenIEnterUsernameAndPasswordToLogin()
        {
            var userName = GetJsonConfigurationValue("UserName");
            var password = GetJsonConfigurationValue("Password");
            Pages.LoginPage().Login(userName, password);
        }

        
       
        [When(@"I do something required here")]
        public void WhenIDoSomethingRequiredHere()
        {
            Console.WriteLine("nothing to do here just to show context injection, we will create some data here and use in the same scenario in another step");
            _sharedData.data1 = "dataCreatedHereOrFromDbOrAnotherResource";
            _sharedData.data2 = 5;
        }

              

        [Then(@"I should see section headers ""(.*)"" at the top of home page")]
        public void ThenIShouldSeeSectionHeadersAtTheTopOfHomePage(string headerLinkText)
        {
            Console.WriteLine("");
        }

        [Then(@"another assertion here-")]
        public void ThenAnotherAssertionHere()
        {
            var a = _sharedData.data1;
            var b = _sharedData.data2;
            Console.WriteLine("We can use the data coming from previous step in assertion if required");
            Console.WriteLine("And means here is Then, the binding is  [Then(@\"another assertion here\")]");
        }

        [Given(@"I am at the google home page")]
        public void GivenIAmAtTheGoogleHomePage()
        {
            Assert.AreEqual(Pages.HomePage().Title, Driver.GetDriver().Title, "page title is different than " + Driver.GetDriver().Title);
        }

        [Then(@"I should see the searh bar")]
        public void ThenISshouldSeeTheSearhBar()
        {
            Assert.IsTrue(Pages.HomePage().SearchBar.Displayed);
        }

        [When(@"I search the any ""(.*)""")]
        public void WhenISearchTheAny(string searchItem)
        {
            Pages.HomePage().SearchBar.SendKeys(searchItem);
            Pages.HomePage().SearchBar.SendKeys(Keys.Enter);

        }

        [Then(@"I should see a result containing ""(.*)""")]
        public void ThenIShouldSeeAResultContaining(string searchItem)
        {
            Assert.IsTrue(Driver.GetDriver().FindElement(By.LinkText(searchItem)).Displayed);
        }

    }
}
