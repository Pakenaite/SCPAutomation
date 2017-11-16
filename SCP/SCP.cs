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
        public void SuccessfulLoginDomain()
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
        public void SuccessfulLoginNoDomain()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.jimEmail);
            scpPage.EnterLoginPassword(Variables.jimPassword);
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
        public void UnsuccessfulLoginNoDomain()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.loginEmailInvalid2);
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
            scpPage.browser.Close();
        }
        [TestMethod]
        public void UnsuccessfulLoginPasswordNoDomain()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.jimEmail);
            scpPage.EnterLoginPassword(Variables.loginPasswordWrong);
            scpPage.ClickOnButton(Variables.loginButton);

            var xpath2 = "//*[contains(text(),'Username or Password is incorrect.')]";
            var actualNotice = scpPage.browser.FindElement(By.XPath(xpath2)).Displayed;
            var errorPhoto = scpPage.browser.FindElements(By.TagName("img"))[1].Displayed;
            Assert.IsTrue(actualNotice);
            Assert.IsTrue(errorPhoto);

            Thread.Sleep(1000);
            scpPage.browser.Close();
        }
        [TestMethod]
        public void ForgotPassword()
        {
            var scpPage = Initialize();
            scpPage.ClickOnButton(Variables.forgotPassword);

            var expectedHeader = "Forgot your password";
            var actualHeader = scpPage.browser.FindElement(By.TagName("h3")).Text;             
            Thread.Sleep(1000);
            scpPage.browser.Close();
            Assert.AreEqual(expectedHeader, actualHeader);
        }
        [TestMethod]
        public void EmailFieldLimit()
        {
            var scpPage = Initialize();
            var emailField = scpPage.browser.FindElement(By.Name("username"));
            emailField.SendKeys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            var readSymbols = emailField.GetAttribute("value");
            Thread.Sleep(1000);
            scpPage.browser.Quit();
            Assert.AreEqual(50, readSymbols.Length);
        }
        [TestMethod]
        public void PasswordFieldLimit()
        {
            var scpPage = Initialize();
            var passwordField = scpPage.browser.FindElement(By.Name("password"));
            passwordField.SendKeys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            var readSymbols = passwordField.GetAttribute("value");
            Thread.Sleep(1000);
            scpPage.browser.Quit();
            Assert.AreEqual(50, readSymbols.Length);
        }
        [TestMethod]
        public void SuccessfulLogout()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.jimEmail);
            scpPage.EnterLoginPassword(Variables.jimPassword);
            scpPage.ClickOnButton(Variables.loginButton);
            scpPage.ClickOnButton(Variables.toggleMenu);
            scpPage.ClickOnButton(Variables.signOut);

            var actualTitle = scpPage.browser.FindElement(By.TagName("h1")).Text;
            var expectedTitle = "Communication Portal";
            scpPage.browser.Quit();
            Assert.AreEqual(expectedTitle, actualTitle);
        }
        [TestMethod]
        public void Relogin()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.jimEmail);
            scpPage.EnterLoginPassword(Variables.jimPassword);
            scpPage.ClickOnButton(Variables.loginButton);
            scpPage.ClickOnButton(Variables.toggleMenu);
            scpPage.ClickOnButton(Variables.signOut);
            scpPage.EnterLoginEmail(Variables.loginEmail);
            scpPage.EnterLoginPassword(Variables.loginPassword);
            scpPage.ClickOnButton(Variables.loginButton);

            var expectedWelcome = "Stay connected with mySCP Messenger";
            var actualWelcome = scpPage.browser.FindElements(By.TagName("h1"))[1].Text;
            Assert.AreEqual(expectedWelcome, actualWelcome);

            scpPage.browser.Quit();
        }
    }
    [TestClass]
    public class MainPage
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
        public void HomePageHeader()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.jimEmail);
            scpPage.EnterLoginPassword(Variables.jimPassword);
            scpPage.ClickOnButton(Variables.loginButton);

            var expectedHeader = "mySCP Messenger";
            var actualHeader = scpPage.browser.FindElements(By.TagName("a"))[0].Text;
            scpPage.browser.Quit();
            Assert.AreEqual(expectedHeader, actualHeader);
        }
        [TestMethod]
        public void HomePageWelcome()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.jimEmail);
            scpPage.EnterLoginPassword(Variables.jimPassword);
            scpPage.ClickOnButton(Variables.loginButton);

            var expectedWelcome = "Stay connected with mySCP Messenger";
            var actualWelcome = scpPage.browser.FindElements(By.TagName("h1"))[1].Text;
            scpPage.browser.Quit();
            Assert.AreEqual(expectedWelcome, actualWelcome);
        }
        [TestMethod]
        public void SeveralMessages()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.jimEmail);
            scpPage.EnterLoginPassword(Variables.jimPassword);
            scpPage.ClickOnButton(Variables.loginButton);

            var hrDisplayed = scpPage.browser.FindElement(By.TagName("hr")).Displayed;
            scpPage.browser.Quit();
            Assert.IsTrue(hrDisplayed);
        }
        [TestMethod]
        public void NoMessages()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.loginEmail);
            scpPage.EnterLoginPassword(Variables.loginPassword);
            scpPage.ClickOnButton(Variables.loginButton);

            var expectedNoMessages = "No Messages";
            var actualNoMessages = scpPage.browser.FindElement(By.TagName("h2")).Text;
            Thread.Sleep(1000);
            scpPage.browser.Quit();
            Assert.AreEqual(expectedNoMessages, actualNoMessages);
        }
        [TestMethod]
        public void BlueButton()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.loginEmail);
            scpPage.EnterLoginPassword(Variables.loginPassword);
            scpPage.ClickOnButton(Variables.loginButton);

            var blueButton = scpPage.browser.FindElement(By.XPath("//button[@title='Start new message']")).Displayed;
            scpPage.browser.Quit();
            Assert.IsTrue(blueButton);
        }
        [TestMethod]
        public void SettingsMenu()
        {
            var scpPage = Initialize();

            scpPage.EnterLoginEmail(Variables.loginEmail);
            scpPage.EnterLoginPassword(Variables.loginPassword);
            scpPage.ClickOnButton(Variables.loginButton);
            scpPage.ClickOnButton(Variables.toggleMenu);
            scpPage.ClickOnButton(Variables.settings);

            var actualHeader = scpPage.browser.FindElements(By.TagName("h1"))[0].Text;
            var expectedHeader = "Settings";
            scpPage.browser.Quit();
            Assert.AreEqual(expectedHeader, actualHeader);
        }
    }
    [TestClass]
    public class Settings
    {
        public Browser Initialize()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var browser = new Browser();
            browser.SetBrowser = new ChromeDriver(options);
            browser.OpenPage(Variables.url);
            //browser.OpenPageWithAuthentication(Variables.urlWithAuthentication, Variables.url);
            browser.EnterLoginEmail(Variables.jimEmail);
            browser.EnterLoginPassword(Variables.jimPassword);
            browser.ClickOnButton(Variables.loginButton);
            browser.ClickOnButton(Variables.toggleMenu);
            browser.ClickOnButton(Variables.settings);
            return browser;
        }
        [TestMethod]
        public void LoggedInAs()
        {
            var scpPage = Initialize();

            var expectedSetting = "Logged in as";
            var actualSetting = scpPage.browser.FindElements(By.TagName("b"))[1].Text;
            scpPage.browser.Quit();
            Assert.AreEqual(expectedSetting, actualSetting);
        }
        [TestMethod]
        public void SendFeedbackOrGetSupport()
        {
            var scpPage = Initialize();

            var expectedSetting = "Send feedback or get support";
            var actualSetting = scpPage.browser.FindElements(By.TagName("b"))[2].Text;
            var expectedValue = "myscp@schumacherclinical.com";
            var actualValue = scpPage.browser.FindElements(By.TagName("a"))[0].Text;
            scpPage.browser.Quit();
            Assert.AreEqual(expectedSetting, actualSetting);
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void SCPWebsite()
        {
            var scpPage = Initialize();

            var expectedSetting = "Schumacher Clinical Partners website";
            var actualSetting = scpPage.browser.FindElements(By.TagName("b"))[3].Text;
            var expectedValue = "www.schumacherclinical.com";
            var actualValue = scpPage.browser.FindElements(By.TagName("a"))[1].Text;
            scpPage.browser.Quit();
            Assert.AreEqual(expectedSetting, actualSetting);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
    [TestClass]
    public class DirectMessage
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
        public void CreateNew()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.aliyahEmail);
            scpPage.EnterLoginPassword(Variables.aliyahPassword);
            scpPage.ClickOnButton(Variables.loginButton);
            scpPage.ClickOnButton(Variables.blueButtonTitle);
            scpPage.ClickOnButton(Variables.newChatButton);
            scpPage.ClickOnButton2("button", 2);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.EnterMessage(Variables.messageText);
            scpPage.ClickOnButton(Variables.sendButton);

            var messageInWeb = scpPage.browser.FindElement(By.TagName("p")).Text;
            var messageStatus = scpPage.browser.FindElement(By.XPath("//span[@title='Message has been sent']")).Displayed;
            scpPage.browser.Quit();
            Assert.AreEqual(Variables.messageText, messageInWeb);
            Assert.IsTrue(messageStatus);
        }        
        [TestMethod]
        public void ReceiveMessage()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.louisaEmail);
            scpPage.EnterLoginPassword(Variables.louisaPassword);
            scpPage.ClickOnButton(Variables.loginButton);
            scpPage.ClickOnButton(Variables.aliyahNameMessage);

            var messageInWeb = scpPage.browser.FindElement(By.TagName("p")).Text;            
            scpPage.browser.Quit();
            Assert.AreEqual(Variables.messageText, messageInWeb);
        }
        [TestMethod]
        public void SendMessageInOld()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.aliyahEmail);
            scpPage.EnterLoginPassword(Variables.aliyahPassword);
            scpPage.ClickOnButton(Variables.loginButton);
            scpPage.ClickOnButton(Variables.louisaName);
            var messageStatus = scpPage.browser.FindElement(By.XPath("//span[@title='Message has been read']")).Displayed;
            scpPage.EnterMessage(Variables.messageText);
            scpPage.ClickOnButton(Variables.sendButton);

            var messageInWeb = scpPage.browser.FindElement(By.TagName("p")).Text;
            scpPage.browser.Quit();
            Assert.AreEqual(Variables.messageText, messageInWeb);
            Assert.IsTrue(messageStatus);
        }
        [TestMethod]
        public void Delete()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.aliyahEmail);
            scpPage.EnterLoginPassword(Variables.aliyahPassword);
            scpPage.ClickOnButton(Variables.loginButton);
            scpPage.ClickOnButton(Variables.deleteMessage);
            scpPage.ClickOnButton(Variables.agreeToDelete);

            var messageInList = scpPage.browser.FindElements(By.XPath("//div[contains(text(),'Louisa Conn')]")).Count;
            Assert.AreEqual(0, messageInList);
            scpPage.browser.Quit();
        }        
        [TestMethod]
        public void StartWithUserThatWasDeleted()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.aliyahEmail);
            scpPage.EnterLoginPassword(Variables.aliyahPassword);
            scpPage.ClickOnButton(Variables.loginButton);
            scpPage.ClickOnButton(Variables.blueButtonTitle);
            scpPage.ClickOnButton(Variables.newChatButton);
            scpPage.ClickOnButton2("button", 2);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.EnterMessage(Variables.messageText);
            scpPage.ClickOnButton(Variables.sendButton);
            scpPage.ClickOnButton(Variables.deleteMessage);
            scpPage.ClickOnButton(Variables.agreeToDelete);
            scpPage.ClickOnButton(Variables.blueButtonTitle);
            scpPage.ClickOnButton(Variables.newChatButton);
            scpPage.ClickOnButton2("button", 2);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.EnterMessage(Variables.messageText);
            scpPage.ClickOnButton(Variables.sendButton);

            var messageInWeb = scpPage.browser.FindElement(By.TagName("p")).Text;
            var messageStatus = scpPage.browser.FindElement(By.XPath("//span[@title='Message has been sent']")).Displayed;
            scpPage.browser.Quit();
            Assert.AreEqual(Variables.messageText, messageInWeb);
            Assert.IsTrue(messageStatus);
        }        
        [TestMethod]
        public void SearchForDirectMessage()
        {
            var scpPage = Initialize();
            scpPage.EnterLoginEmail(Variables.aliyahEmail);
            scpPage.EnterLoginPassword(Variables.aliyahPassword);
            scpPage.ClickOnButton(Variables.loginButton);
            scpPage.SearchContact(Variables.contactName);

            var messageInList = scpPage.browser.FindElements(By.XPath("//div[contains(text(),'Louisa Conn')]")).Count;
            scpPage.browser.Quit();
            Assert.AreEqual(1, messageInList);
        }
    }
    [TestClass]
    public class GroupMessage
    {
        public Browser Initialize(string email, string password)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            var browser = new Browser();
            browser.SetBrowser = new ChromeDriver(options);
            browser.OpenPage(Variables.url);
            //browser.OpenPageWithAuthentication(Variables.urlWithAuthentication, Variables.url);
            browser.EnterLoginEmail(email);
            browser.EnterLoginPassword(password);
            browser.ClickOnButton(Variables.loginButton);
            return browser;
        }
        [TestMethod]
        public void CreateNew()
        {
            var scpPage = Initialize(Variables.loginEmail,Variables.loginPassword);
            scpPage.ClickOnButton(Variables.blueButtonTitle);
            scpPage.ClickOnButton2(Variables.newGroupButton, 0);
            scpPage.ClickOnButton2("button", 2);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.ClickOnButton2("button", 1);
            scpPage.SearchContact(Variables.contactName1);
            scpPage.ClickOnButton(Variables.contactTitle1);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 4);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 2);

            var headerInWeb = scpPage.browser.FindElement(By.TagName("h1")).Text;
            scpPage.browser.Quit();
            Assert.AreEqual("Louisa C., Aashish G.", headerInWeb);
        }
        [TestMethod]
        public void NewCustomTitle()
        {
            var scpPage = Initialize(Variables.loginEmail, Variables.loginPassword);
            scpPage.ClickOnButton(Variables.blueButtonTitle);
            scpPage.ClickOnButton2(Variables.newGroupButton, 0);
            scpPage.ClickOnButton2("button", 2);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.ClickOnButton2("button", 1);
            scpPage.SearchContact(Variables.contactName1);
            scpPage.ClickOnButton(Variables.contactTitle1);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 4);
            scpPage.ClickOnButton2("button", 1);
            scpPage.EnterTextByTag("input", Variables.groupTitle, 0);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 2);

            var headerinWeb = scpPage.browser.FindElement(By.TagName("h1")).Text;
            scpPage.browser.Quit();
            Assert.AreEqual("E2E Group title", headerinWeb);
        }
        [TestMethod]
        public void SendGroupMessage()
        {
            var scpPage = Initialize(Variables.loginEmail, Variables.loginPassword);
            scpPage.ClickOnButton(Variables.blueButtonTitle);
            scpPage.ClickOnButton2(Variables.newGroupButton, 0);
            scpPage.ClickOnButton2("button", 2);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.ClickOnButton2("button", 1);
            scpPage.SearchContact(Variables.contactName1);
            scpPage.ClickOnButton(Variables.contactTitle1);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 4);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 2);
            scpPage.EnterMessage(Variables.groupText);
            scpPage.ClickOnButton(Variables.sendButton);

            var messageInWeb = scpPage.browser.FindElement(By.TagName("p")).Text;
            scpPage.browser.Quit();
            Assert.AreEqual("E2E testing group message 2", messageInWeb);
        }
        [TestMethod]
        public void ReceiveGroupMessage()
        {
            var user1 = Initialize(Variables.loginEmail, Variables.loginPassword);
            var user2 = Initialize(Variables.louisaEmail, Variables.louisaPassword);
            user1.ClickOnButton(Variables.blueButtonTitle);
            user1.ClickOnButton2(Variables.newGroupButton, 0);
            user1.ClickOnButton2("button", 2);
            user1.SearchContact(Variables.contactName);
            user1.ClickOnButton(Variables.contactNameXPath);
            user1.ClickOnButton2("button", 1);
            user1.SearchContact(Variables.contactName1);
            user1.ClickOnButton(Variables.contactTitle1);
            user1.ClickOnButton2(Variables.blueButtonTagName, 4);
            user1.ClickOnButton2(Variables.blueButtonTagName, 2);
            user1.EnterMessage(Variables.groupText);
            user1.ClickOnButton(Variables.sendButton);
            user2.ClickOnButton("//div[contains(text(),'Louisa C., Aashish G.')]");
            user1.ClickOnButton2("h3", 0);

            var messageInWeb = user2.browser.FindElement(By.TagName("p")).Text;
            var messageStatus = user1.browser.FindElement(By.XPath("//span[@title='Message has been read']")).Displayed;
            user1.browser.Quit();
            user2.browser.Quit();
            Assert.AreEqual("E2E testing group message 2", messageInWeb);
            Assert.IsTrue(messageStatus);
        }
        [TestMethod]
        public void AddNewParticipants()
        {
            var scpPage = Initialize(Variables.loginEmail, Variables.loginPassword);
            var user2 = Initialize(Variables.aliyahEmail, Variables.aliyahPassword);
            scpPage.ClickOnButton(Variables.blueButtonTitle);
            scpPage.ClickOnButton2(Variables.newGroupButton, 0);
            scpPage.ClickOnButton2("button", 2);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.ClickOnButton2("button", 1);
            scpPage.SearchContact(Variables.contactName1);
            scpPage.ClickOnButton(Variables.contactTitle1);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 4);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 2);
            scpPage.EnterMessage(Variables.groupText);
            scpPage.ClickOnButton(Variables.sendButton);
            scpPage.ClickOnButton2("h1", 0);
            scpPage.ClickOnButton(Variables.addNewParticipants);
            scpPage.EnterTextByTag("input", Variables.aliyahName, 2);
            scpPage.ClickOnButton(Variables.aliyahXPath);
            
            var messageInWeb = user2.browser.FindElements(By.TagName("p"))[0].Text;
            var groupExists = user2.browser.FindElement(By.XPath("//div[contains(text(),'Louisa C., Aashish G.')]")).Displayed;
            scpPage.browser.Quit();
            user2.browser.Quit();
            Assert.IsTrue(groupExists);
            Assert.AreEqual("",messageInWeb);
        }
        [TestMethod]
        public void SendMessageToNewParticipant()
        {
            var scpPage = Initialize(Variables.loginEmail, Variables.loginPassword);
            var user2 = Initialize(Variables.aliyahEmail, Variables.aliyahPassword);
            scpPage.ClickOnButton(Variables.blueButtonTitle);
            scpPage.ClickOnButton2(Variables.newGroupButton, 0);
            scpPage.ClickOnButton2("button", 2);
            scpPage.SearchContact(Variables.contactName);
            scpPage.ClickOnButton(Variables.contactNameXPath);
            scpPage.ClickOnButton2("button", 1);
            scpPage.SearchContact(Variables.contactName1);
            scpPage.ClickOnButton(Variables.contactTitle1);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 4);
            scpPage.ClickOnButton2(Variables.blueButtonTagName, 2);
            scpPage.EnterMessage(Variables.groupText);
            scpPage.ClickOnButton(Variables.sendButton);
            scpPage.ClickOnButton2("h1", 0);
            scpPage.ClickOnButton(Variables.addNewParticipants);
            scpPage.EnterTextByTag("input", Variables.aliyahName, 2);
            scpPage.ClickOnButton(Variables.aliyahXPath);
            scpPage.EnterMessage("Added user");
            scpPage.ClickOnButton(Variables.sendButton);

            var messageInWeb = user2.browser.FindElements(By.TagName("p"))[0].Text;
            var groupExists = user2.browser.FindElement(By.XPath("//div[contains(text(),'Louisa C., Aashish G.')]")).Displayed;
            scpPage.browser.Quit();
            user2.browser.Quit();
            Assert.IsTrue(groupExists);
            Assert.AreEqual("Added user", messageInWeb);
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
