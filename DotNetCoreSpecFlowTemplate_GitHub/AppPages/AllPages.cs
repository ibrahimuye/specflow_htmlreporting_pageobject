using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreSpecFlowTemplate.AppPages
{
    public class AllPages
    {
        // we need a private page object and a getter function for each page class created for each physical page .

        private BasePage _basePage;
        private SamplePage _samplePage;
        private LoginPage _loginPage;
        private HomePage _homePage;
       
        public BasePage BasePage()
        {
            if (_basePage == null) _basePage = new BasePage();
            return _basePage;
        }

        public SamplePage SamplePage()
        {
            if (_samplePage == null) _samplePage = new SamplePage();
            return _samplePage;
        }


        public LoginPage LoginPage()
        {
            if (_loginPage == null) _loginPage = new LoginPage();
            return _loginPage;
        }

        public HomePage HomePage()
        {
            if (_homePage == null) _homePage = new HomePage();
            return _homePage;
        }

    }
}
