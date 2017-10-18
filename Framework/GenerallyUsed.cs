using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
namespace Framework
{
    public static class GenerallyUsed
    {
        public static void IWaitUntilElementAppears(Browser browser,string elementsXPath)
        {
            var elements = 0;
            var counter = 0;
            while (elements == 0)
            {
                elements = browser.browser.FindElements(By.XPath(elementsXPath)).Count;
                System.Threading.Thread.Sleep(500);
                counter++;
                if (counter > 20) break;
            }
        }
    }
}
