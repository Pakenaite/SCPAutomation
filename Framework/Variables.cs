using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static class Variables
    {
        public static string url = "https://qa.myscp.com";

        public static string loginEmail = "edvinas.alenskas@devbridge.com";
        public static string loginPassword = "pa$$word";

        public static string louisaEmail = "louisa.conn@devbridge.com";
        public static string louisaPassword = "pa$$word";

        public static string jimEmail = "jim.doe";
        public static string jimPassword = "pa$$word";

        public static string aliyahEmail = "aliyah.miller@devbridge.com";
        public static string aliyahPassword = "pa$$word";

        public static string loginEmailWrong = "john.doe.minique@devbridge.com";
        public static string loginPasswordWrong = "password";

        public static string loginEmailInvalid1 = "john.doe.minique@.com";
        public static string loginEmailInvalid2 = "john.doee";

        public static string longEmail = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        public static string loginButton = "//*[contains(text(),'Sign in')]";

        public static string usernameField = "username";

        public static string blueButtonTagName = "button";
        public static string blueButtonTitle = "//button[@title='Start new message']";

        public static string newChatButton = "//span[contains(text(),'New message')]";
        public static string newGroupButton = "li";
        public static string newBroadcastButton = "li";

        public static string contactName = "Louisa Conn";
        public static string louisaName = "//div[contains(text(),'Louisa Conn')]";
        public static string aliyahName = "Aliyah Miller";
        public static string aliyahXPath = "//div[@title='Aliyah Miller']";
        public static string aliyahNameMessage = "//div[contains(text(),'Aliyah Miller')]";
        public static string contactName1 = "Aashish Gupta";
        public static string contactNameXPath = "//*[contains(text(),'Unknown')]";

        public static string messageText = "E2E testing message";
        public static string responseText = "Responding to E2E testing message";
        public static string groupText = "E2E testing group message 2";
        public static string broadcastText = "E2E testing broadcast message 2";

        public static string sendButton = "//*[contains(text(),'Send')]";

        public static string searchFieldTag = "input";

        public static string groupInList = "//*[contains(text(),'E2E Group title')]";

        public static string groupTitle = "E2E Group title";
        public static string broadcastTitle = "E2E Broadcast title";

        public static string deleteMessage = "//button[@title='Delete message']";
        public static string agreeToDelete = "//span[contains(text(),'Delete')]";

        public static string messageWasRead = "Message has been read";

        public static string forgotPassword = "//*[contains(text(),'Forgot')]";

        public static string toggleMenu = "//button[@title='Toggle menu']";
        public static string signOut = "//*[contains(text(),'Sign out')]";
        public static string settings = "//*[contains(text(),'Settings')]";
    }
}
