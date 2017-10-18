using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium;

namespace Framework
{
    public class Browser
    {
        //variables
        public int sleepTime = 3000;
        public IWebDriver browser;

        //methods
        public IWebDriver SetBrowser
        {
            set { browser = value; }
        }

        public void OpenPageWithAuthentication(string authenticationURL, string url)
        {
            browser.Url = authenticationURL;
            Thread.Sleep(2000);
            browser.Url = url;
        }

        public void OpenPage(string url)
        {
            browser.Url = url;
        }

        public void EnterLoginEmail(string email)
        {
            browser.FindElement(By.Name("username")).SendKeys(email);
        }

        public void EnterLoginPassword(string password)
        {
            browser.FindElement(By.Name("password")).SendKeys(password);
        }

        public void ClickOnButton(string buttonXpath)
        {
            browser.FindElement(By.XPath(buttonXpath)).Click();
            System.Threading.Thread.Sleep(sleepTime);
        }
        public void ClickOnButtonN(string buttonXpath, int n)
        {
            browser.FindElements(By.XPath(buttonXpath))[n].Click();
            System.Threading.Thread.Sleep(sleepTime);
        }

        public void ClickOnButton2(string tagName, int n)
        {
            browser.FindElements(By.TagName(tagName))[n].Click();
            System.Threading.Thread.Sleep(sleepTime);
        }

        public void ClearField(string field)
        {
            browser.FindElement(By.Name(field)).Clear();
        }

        public void ClearFieldByTag(string field)
        {
            browser.FindElement(By.TagName(field)).Clear();
        }

        public void SearchContact (string name)
        {
            browser.FindElement(By.TagName("input")).SendKeys(name);
            System.Threading.Thread.Sleep(sleepTime);
        }

        public void EnterMessage (string message)
        {
            browser.FindElement(By.TagName("textarea")).SendKeys(message);
        }

        public void EnterTextByTag(string tagName, string text, int n)
        {
            browser.FindElements(By.TagName(tagName))[n].SendKeys(text);
        }

    }
}
