using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace DotNetCoreSpecFlowTemplate.AppPages
{
    public class HomePage : BasePage
    {
        public HomePage()
        {
            PageFactory.InitElements(Driver.GetDriver(), this);
        }

        
        [FindsBy(How = How.Id, Using = "realbox")]
        public IWebElement SearchBar { get; set; }
        public string Title = "Google";
    }
}
