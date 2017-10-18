using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Framework;

namespace SCP
{
    [TestClass]
    public class LoginLogout
    {
        public Browser Initialize()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var browser = new Browser();
            browser.SetBrowser = new ChromeDriver(options);
            browser.OpenPage(Variables.url);
            //browser.OpenPageWithAuthentication(Variables.urlWithAuthentication, Variables.url);
            return browser;
        }

        [TestMethod]
        public void SuccessfulLogin()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.loginEmail);
            scpPage.EnterLoginPassword(Variables.loginPassword);
            scpPage.ClickOnButton(Variables.loginButton);

            var expectedWelcome = "Stay connected with mySCP Messenger";
            var actualWelcome = scpPage.browser.FindElements(By.TagName("h1"))[1].Text;
            Assert.AreEqual(expectedWelcome, actualWelcome);

            scpPage.browser.Quit();
        }

        [TestMethod]
        public void UnsuccessfulLoginEmail()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.loginEmailWrong);
            scpPage.EnterLoginPassword(Variables.loginPassword);
            scpPage.ClickOnButton(Variables.loginButton);

            var xpath2 = "//*[contains(text(),'Username or Password is incorrect.')]";
            var actualNotice = scpPage.browser.FindElement(By.XPath(xpath2)).Displayed;
            var errorPhoto = scpPage.browser.FindElements(By.TagName("img"))[1].Displayed;
            Assert.IsTrue(actualNotice);
            Assert.IsTrue(errorPhoto);

            Thread.Sleep(1000);
            scpPage.browser.Quit();
        }


        [TestMethod]
        public void UnsuccessfulLoginPassword()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.loginEmail);
            scpPage.EnterLoginPassword(Variables.loginPasswordWrong);
            scpPage.ClickOnButton(Variables.loginButton);

            var xpath2 = "//*[contains(text(),'Username or Password is incorrect.')]";
            var actualNotice = scpPage.browser.FindElement(By.XPath(xpath2)).Displayed;
            var errorPhoto = scpPage.browser.FindElements(By.TagName("img"))[1].Displayed;
            Assert.IsTrue(actualNotice);
            Assert.IsTrue(errorPhoto);

            Thread.Sleep(1000);
            scpPage.browser.Quit();
        }

        /*No longer needed, as user can login without domain
         * [TestMethod]
        public void EmailValidation()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.loginEmailInvalid1);
            scpPage.EnterLoginPassword(Variables.loginPasswordWrong);
            scpPage.ClickOnButton(Variables.loginButton);

            var xpath2 = "//*[contains(text(),'Please enter a valid email address')]";
            var actualNotice = scpPage.browser.FindElement(By.XPath(xpath2)).Displayed;
            var errorPhoto = scpPage.browser.FindElements(By.TagName("img"))[1].Displayed;
            Assert.IsTrue(actualNotice);
            Assert.IsTrue(errorPhoto);

            scpPage.ClearField(Variables.usernameField);
            scpPage.EnterLoginEmail(Variables.loginEmailInvalid2);
            scpPage.ClickOnButton(Variables.loginButton);
            Thread.Sleep(2000);

            var actualNotice1 = scpPage.browser.FindElement(By.XPath(xpath2)).Displayed;
            var errorPhoto1 = scpPage.browser.FindElements(By.TagName("img"))[1].Displayed;
            Assert.IsTrue(actualNotice1);
            Assert.IsTrue(errorPhoto1);

            Thread.Sleep(1000);
            scpPage.browser.Quit();
        }*/

        [TestMethod]
        public void EmailFieldLimit()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            //Arba vietoj siu dvieju above ir options apacioj rasyti: browser.Manage().Window.Maximize();
            IWebDriver browser = new ChromeDriver(options);
            browser.Url = "https://qa.myscp.com";
            Thread.Sleep(1000);

            var emailField = browser.FindElement(By.Name("username"));

            emailField.SendKeys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            var readSymbols = emailField.GetAttribute("value");


            Thread.Sleep(1000);
            browser.Quit();
            Assert.AreEqual(50, readSymbols.Length);
        }
        [TestMethod]
        public void PasswordFieldLimit()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            //Arba vietoj siu dvieju above ir options apacioj rasyti: browser.Manage().Window.Maximize();
            IWebDriver browser = new ChromeDriver(options);
            browser.Url = "https://qa.myscp.com";
            Thread.Sleep(1000);

            var passwordField = browser.FindElement(By.Name("password"));

            passwordField.SendKeys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            var readSymbols = passwordField.GetAttribute("value");


            Thread.Sleep(1000);
            browser.Quit();
            Assert.AreEqual(50, readSymbols.Length);
        }
        [TestMethod]
        public void SuccessfulLogout()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            //Arba vietoj siu dvieju above ir options apacioj rasyti: browser.Manage().Window.Maximize();
            IWebDriver browser = new ChromeDriver(options);
            browser.Url = "https://admin:m1y2s3c4p5q@qa.myscp.com";
            Thread.Sleep(1000);
            browser.Url = "https://qa.myscp.com";
            Thread.Sleep(1000);

            var emailField = browser.FindElement(By.Name("username"));
            var passwordField = browser.FindElement(By.Name("password"));
            var xpath = "//*[contains(text(),'Sign in')]";
            var loginButton = browser.FindElement(By.XPath(xpath));

            emailField.SendKeys("jane.doe@devbridge.com");
            passwordField.SendKeys("pa$$word");
            loginButton.Click();
            Thread.Sleep(2000);

            var toggleMenu = browser.FindElement(By.TagName("button"));
            toggleMenu.Click();
            var xpath2 = "//*[contains(text(),'Sign out')]";
            var signOut = browser.FindElement(By.XPath(xpath2));
            signOut.Click();
            Thread.Sleep(1000);

            var expectedImage = browser.FindElements(By.TagName("img"))[0].Displayed;
            browser.Quit();

            Assert.IsTrue(expectedImage);
        }
    }

    [TestClass]
    public class MainPage
    {
        [TestMethod]
        public void NoMessages()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            //Arba vietoj siu dvieju above ir options apacioj rasyti: browser.Manage().Window.Maximize();
            IWebDriver browser = new ChromeDriver(options);
            browser.Url = "https://admin:m1y2s3c4p5q@qa.myscp.com";
            Thread.Sleep(1000);
            browser.Url = "https://qa.myscp.com";
            Thread.Sleep(1000);

            var emailField = browser.FindElement(By.Name("username"));
            var passwordField = browser.FindElement(By.Name("password"));
            var xpath = "//*[contains(text(),'Sign in')]";
            var loginButton = browser.FindElement(By.XPath(xpath));

            emailField.SendKeys("jim.doe@devbridge.com");
            passwordField.SendKeys("pa$$word");
            loginButton.Click();
            Thread.Sleep(1000);

            var expectedWelcome = "Stay connected with mySCP Messenger";
            var actualWelcome = browser.FindElements(By.TagName("h2"))[1].Text;

            var expectedNoMessages = "No Messages";
            var actualNoMessages = browser.FindElements(By.TagName("h2"))[0].Text;

            var actualMessages = browser.FindElement(By.TagName("a")).Displayed;

            Thread.Sleep(1000);
            browser.Quit();

            Assert.IsFalse(actualMessages);
            Assert.AreEqual(expectedNoMessages, actualNoMessages);
            Assert.AreEqual(expectedWelcome, actualWelcome);

        }
        [TestMethod]
        public void SeveralMessages()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            //Arba vietoj siu dvieju above ir options apacioj rasyti: browser.Manage().Window.Maximize();
            IWebDriver browser = new ChromeDriver(options);
            browser.Url = "https://admin:m1y2s3c4p5q@qa.myscp.com";
            Thread.Sleep(1000);
            browser.Url = "https://qa.myscp.com";
            Thread.Sleep(1000);

            var emailField = browser.FindElement(By.Name("username"));
            var passwordField = browser.FindElement(By.Name("password"));
            var xpath = "//*[contains(text(),'Sign in')]";
            var loginButton = browser.FindElement(By.XPath(xpath));

            emailField.SendKeys("jane.doe@devbridge.com");
            passwordField.SendKeys("pa$$word");
            loginButton.Click();
            Thread.Sleep(1000);

            var expectedWelcome = "Stay connected with mySCP Messenger";
            var actualWelcome = browser.FindElements(By.TagName("h1"))[1].Text;

            var actualMessages = browser.FindElements(By.TagName("span"))[0].Displayed;

            Thread.Sleep(1000);
            browser.Quit();

            Assert.IsTrue(actualMessages);
            Assert.AreEqual(expectedWelcome, actualWelcome);

        }
    }
    [TestClass]
    public class BlueButton
    {
        [TestMethod]
        public void OpenClose()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            //Arba vietoj siu dvieju above ir options apacioj rasyti: browser.Manage().Window.Maximize();
            IWebDriver browser = new ChromeDriver(options);
            browser.Url = "https://admin:m1y2s3c4p5q@qa.myscp.com";
            Thread.Sleep(1000);
            browser.Url = "https://qa.myscp.com";
            Thread.Sleep(1000);

            var emailField = browser.FindElement(By.Name("username"));
            var passwordField = browser.FindElement(By.Name("password"));
            var xpath = "//*[contains(text(),'Sign in')]";
            var loginButton = browser.FindElement(By.XPath(xpath));

            emailField.SendKeys("jim.doe@devbridge.com");
            passwordField.SendKeys("pa$$word");
            loginButton.Click();
            Thread.Sleep(1000);

            var blueButtonDisplayed = browser.FindElements(By.TagName("button"))[1].Displayed;

            Assert.IsTrue(blueButtonDisplayed);

            var blueButton = browser.FindElements(By.TagName("button"))[1];

            blueButton.Click();
            Thread.Sleep(2000);
            var menuListDisplayed = browser.FindElement(By.TagName("ul")).Displayed;

            Thread.Sleep(1000);
            browser.Quit();
        }
        [TestMethod]
        public void MenuItems()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            //Arba vietoj siu dvieju above ir options apacioj rasyti: browser.Manage().Window.Maximize();
            IWebDriver browser = new ChromeDriver(options);
            browser.Url = "https://admin:m1y2s3c4p5q@qa.myscp.com";
            Thread.Sleep(1000);
            browser.Url = "https://qa.myscp.com";
            Thread.Sleep(1000);

            var emailField = browser.FindElement(By.Name("username"));
            var passwordField = browser.FindElement(By.Name("password"));
            var xpath = "//*[contains(text(),'Sign in')]";
            var loginButton = browser.FindElement(By.XPath(xpath));

            emailField.SendKeys("jim.doe@devbridge.com");
            passwordField.SendKeys("pa$$word");
            loginButton.Click();
            Thread.Sleep(1000);

            var blueButton = browser.FindElements(By.TagName("button"))[1];

            blueButton.Click();

            Thread.Sleep(2000);

            var numberOfMenuItems = browser.FindElements(By.TagName("li")).Count;
            var expectedNumberOfMenuItems = 3;

            var xpath1 = "//*[contains(text(),'New broadcast')]";
            var xpath2 = "//*[contains(text(),'New group')]";
            var xpath3 = "//*[contains(text(),'New message')]";

            var newBroadcast = browser.FindElement(By.XPath(xpath1)).Displayed;
            var newGroup = browser.FindElement(By.XPath(xpath2)).Displayed;
            var newChat = browser.FindElement(By.XPath(xpath3)).Displayed;

            browser.Quit();

            Assert.AreEqual(expectedNumberOfMenuItems, numberOfMenuItems);
            Assert.IsTrue(newBroadcast);
            Assert.IsTrue(newGroup);
            Assert.IsTrue(newChat);

        }
    }
    [TestClass]
    public class SendMessages
    {
        public Browser Initialize()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var browser = new Browser();
            browser.SetBrowser = new ChromeDriver(options);
            browser.OpenPage(Variables.url);
            //browser.OpenPageWithAuthentication(Variables.urlWithAuthentication, Variables.url);
            browser.EnterLoginEmail(Variables.loginEmail);
            browser.EnterLoginPassword(Variables.loginPassword);
            browser.ClickOnButton(Variables.loginButton);
            return browser;

        }
        [TestMethod]
        public void DirectMessage()
        {
            var scpPage = Initialize();

            scpPage.ClickOnButton2(Variables.blueButtonTagName, 1);
            scpPage.ClickOnButton2(Variables.newChatButton,2);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.EnterMessage(Variables.messageText);
            scpPage.ClickOnButton(Variables.sendButton);

            var messageInWeb = scpPage.browser.FindElement(By.TagName("p")).Text;
            scpPage.browser.Quit();
            Assert.AreEqual(Variables.messageText, messageInWeb);
        }
        [TestMethod]
        public void GroupMessage()
        {
            var scpPage = Initialize();

            scpPage.ClickOnButton2(Variables.blueButtonTagName, 1);
            scpPage.ClickOnButton2(Variables.newGroupButton,1);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.ClearFieldByTag(Variables.searchFieldTag);
            scpPage.SearchContact(Variables.contactName1);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.ClickOnButton2(Variables.blueButtonTagName,1);
            scpPage.EnterTextByTag("input", Variables.groupTitle,0);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 1);
            scpPage.EnterMessage(Variables.groupText);
            scpPage.ClickOnButton(Variables.sendButton);

            var messageInWeb = scpPage.browser.FindElement(By.TagName("p")).Text;
            scpPage.browser.Quit();
            Assert.AreEqual(Variables.groupText, messageInWeb);
        }
        [TestMethod]
        public void BroadcastMessage()
        {
            var scpPage = Initialize();

            scpPage.ClickOnButton2(Variables.blueButtonTagName, 1);
            scpPage.ClickOnButton2(Variables.newGroupButton, 0);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.ClearFieldByTag(Variables.searchFieldTag);
            scpPage.SearchContact(Variables.contactName1);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 1);
            scpPage.EnterTextByTag("input", Variables.broadcastTitle, 1);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 1);
            scpPage.EnterMessage(Variables.broadcastText);
            scpPage.ClickOnButton(Variables.sendButton);

            var messageInWeb = scpPage.browser.FindElement(By.TagName("p")).Text;
            scpPage.browser.Quit();
            Assert.AreEqual(Variables.broadcastText, messageInWeb);
        }

    }
    [TestClass]
    public class EditGroup
    {
        public Browser Initialize()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var browser = new Browser();
            browser.SetBrowser = new ChromeDriver(options);
            browser.OpenPage(Variables.url);
            //browser.OpenPageWithAuthentication(Variables.urlWithAuthentication, Variables.url);
            browser.EnterLoginEmail(Variables.loginEmail);
            browser.EnterLoginPassword(Variables.loginPassword);
            browser.ClickOnButton(Variables.loginButton);
            return browser;

        }
        [TestMethod]
        public void Title()
        {
            var scpPage = Initialize();

            //open group chat
            scpPage.ClickOnButton(Variables.groupInList);
            //click on header
            scpPage.ClickOnButton2("button",2);
            //click on edit title button
            scpPage.ClickOnButton2("button", 6);
            //supply new title
            scpPage.EnterTextByTag("input", " edited", 1);
            //click save title
            scpPage.ClickOnButton2("button", 6);

            var groupTitleHeader = scpPage.browser.FindElements(By.TagName("h1"))[0].Text;
            var groupTitleDetails = scpPage.browser.FindElements(By.TagName("h2"))[0].Text;
            var expectedTitle = Variables.groupTitle + " edited";
            scpPage.browser.Quit();
            Assert.AreEqual(expectedTitle, groupTitleDetails);
            Assert.AreEqual(expectedTitle, groupTitleHeader);

        }

    }
    [TestClass]
    public class SendingMessages
    {
        public Browser Initialize1()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var browser = new Browser();
            browser.SetBrowser = new ChromeDriver(options);
            browser.OpenPage(Variables.url);
            //browser.OpenPageWithAuthentication(Variables.urlWithAuthentication, Variables.url);
            browser.EnterLoginEmail(Variables.louisaEmail);
            browser.EnterLoginPassword(Variables.louisaPassword);
            browser.ClickOnButton(Variables.loginButton);
            return browser;
        }
        public Browser Initialize2()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var browser = new Browser();
            browser.SetBrowser = new ChromeDriver(options);
            browser.OpenPage(Variables.url);
            //browser.OpenPageWithAuthentication(Variables.urlWithAuthentication, Variables.url);
            browser.EnterLoginEmail(Variables.aliyahEmail);
            browser.EnterLoginPassword(Variables.aliyahPassword);
            browser.ClickOnButton(Variables.loginButton);
            return browser;
        }
        [TestMethod]
        /* Sendig message to user2, deleting message from list, sending response to user1, opening message from user2*/
        public void DirectMessage ()
        {
            var user1 = Initialize1();
            var user2 = Initialize2();

            user1.ClickOnButton(Variables.blueButtonTitle);
            user1.ClickOnButton2(Variables.newChatButton, 2);
            user1.SearchContact(Variables.aliyahName);
            user1.ClickOnButton(Variables.aliyahXPath);
            user1.EnterMessage(Variables.messageText);
            user1.ClickOnButton(Variables.sendButton);

            var messageInWeb = user1.browser.FindElement(By.TagName("p")).Text;

            user1.ClickOnButton(Variables.deleteMessage);
            user1.ClickOnButton(Variables.agreeToDelete);
            user2.ClickOnButton(Variables.louisaName);
            user2.EnterMessage(Variables.responseText);
            user2.ClickOnButton(Variables.sendButton);
            user1.ClickOnButton(Variables.aliyahNameMessage);

            var responseInWeb = user1.browser.FindElement(By.TagName("p")).Text;
            //var messageStatus = user2.browser.FindElement(By.XPath("//span[@title= 'Message has been read')]")).Displayed;

            user1.browser.Quit();
            user2.browser.Quit();

            Assert.AreEqual(Variables.messageText, messageInWeb);
            Assert.AreEqual(Variables.responseText, responseInWeb);
            //Assert.AreEqual(Variables.messageWasRead, messageStatus);
            //Assert.IsTrue(messageStatus);
        }

    }
}
