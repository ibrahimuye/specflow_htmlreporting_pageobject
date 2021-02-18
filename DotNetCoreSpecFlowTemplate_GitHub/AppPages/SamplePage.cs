using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace DotNetCoreSpecFlowTemplate.AppPages
{
    public class SamplePage
    {
        // for each physical application page we will create a class to keep page specific functions, web elements, variables 
        // we need the below constructor for non-angular pages
        // this class inherits from Base page


        public SamplePage()
        {
            PageFactory.InitElements(Driver.GetDriver(), this);
        }

        // samples for page specific web elements by regular locators

        [FindsBy(How = How.Id, Using = "sampleId5")]
        public IWebElement PageSpecificWebElement5 { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='SampleId6']")]
        public IWebElement PageSpecificWebElement6 { get; set; }


        // above page factory template does not work if locator has a dynamic structure, then we use the below way

        public IWebElement DynamicWebElement(string dynamicText)
        {
            var xpath = "//label[@class='form-check-label' and text()='" + dynamicText + "']";
            var element = Driver.GetDriver().FindElement(By.XPath(xpath));
            return element;
        }
    }
}
