using DotNetCoreSpecFlowTemplate.AppPages;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DotNetCoreSpecFlowTemplate.AppPages
{
    public class LoginPage : BasePage
    {

        public string XPathOfNextButton = "//input[@id='idp-discovery-submit']";
        public string IdOfSignInButtonForExternalUser = "okta-signin-submit";
        public string XpathOfSignInButtonForCorporateUser = "//*[@id='idSIButton9']";
        public string XpathOfYesButtonOnFriedkinPage = "//input[@id='idSIButton9']";

        public LoginPage()
        {
            PageFactory.InitElements((ISearchContext)Driver.GetDriver(0), (object)this);
        }

        [FindsBy(How = How.Id, Using = "idp-discovery-username")]
        public IWebElement UserNameFieldForLogin { get; set; }

        [FindsBy(How = How.Id, Using = "okta-signin-password")]
        public IWebElement PasswordFieldForExternalUser { get; set; }

        [FindsBy(How = How.Id, Using = "i0118")]
        public IWebElement PasswordFieldForCorporateUser { get; set; }

        public void Login(string userName, string password)
        {
            this.UserNameFieldForLogin.Clear();
            this.UserNameFieldForLogin.SendKeys(userName);
            Console.WriteLine(userName + " is logging in..");
            WaitForClickabilityAndClick(By.XPath(this.XPathOfNextButton));
            if (userName.Contains("@"))
            {
                this.PasswordFieldForCorporateUser.SendKeys(password);
                WaitForClickabilityAndClick(By.XPath(this.XpathOfSignInButtonForCorporateUser));
                WaitForClickabilityAndClick(By.XPath(this.XpathOfYesButtonOnFriedkinPage));
            }
            else
            {
                this.PasswordFieldForExternalUser.SendKeys(password);
                WaitForClickabilityAndClick(By.Id(this.IdOfSignInButtonForExternalUser));
            }
        }

        public static void WaitForClickabilityAndClick(By locator)
        {
            for (int index = 0; index < 60; ++index)
            {
                Thread.Sleep(500);
                try
                {
                    if (Driver.GetDriver(0).FindElement(locator).Displayed)
                    {
                        if (Driver.GetDriver(0).FindElement(locator).Enabled)
                        {
                            Driver.GetDriver(0).FindElement(locator).Click();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine((object)ex);
                }
            }
            By by = locator;
            Console.WriteLine("could not click after trying 60 times in 30 seconds " + ((object)by != null ? by.ToString() : (string)null));
            throw new ElementClickInterceptedException();
        }
    }
}

 

