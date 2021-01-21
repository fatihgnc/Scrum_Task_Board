using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;

namespace stb_test
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver = new InternetExplorerDriver();

        [TestMethod]
        public void AddCard()
        {
            driver.Navigate().GoToUrl("https://localhost:44324");
            driver.FindElement(By.Id("goToApp")).Click();
            driver.FindElement(By.Id("newCard")).Click();
            driver.FindElement(By.Id("cardName")).SendKeys("Selenyum deneme");
            driver.FindElement(By.Id("cardExpert")).SendKeys("Fatih genc");
            driver.FindElement(By.Id("cardNo")).SendKeys("15");
            driver.FindElement(By.Id("cardDefinition")).SendKeys("lorem ipsum");
            driver.FindElement(By.Id("cardNotes")).SendKeys("lorem ipsum111");
            driver.FindElement(By.Id("submit")).Click();

            IWebElement element = driver.FindElement(By.Id("addAlert"));
            string textContent = element.Text;
            Assert.AreEqual("Kart başarıyla eklendi!", textContent);
        }

        [TestMethod]
        public void AddTask()
        {
            driver.Navigate().GoToUrl("https://localhost:44324");
            driver.FindElement(By.Id("goToApp")).Click();
            driver.FindElement(By.Id("cardDetail43")).Click();
            driver.FindElement(By.Id("addTask")).Click();
            driver.FindElement(By.Id("taskHeader")).SendKeys("Test Header");
            driver.FindElement(By.Id("taskDefinition")).SendKeys("Test Definition");
            driver.FindElement(By.Id("taskDate")).SendKeys("2021-01-15 19:00:58");
            driver.FindElement(By.Id("taskState")).SendKeys("In-Progress");
            driver.FindElement(By.Id("submit")).Click();

            IWebElement element = driver.FindElement(By.Id("addAlert"));
            string textContent = element.Text;
            Assert.AreEqual("İş başarıyla eklendi!", textContent);
        }

        [TestMethod]
        public void EditCard()
        {
            driver.Navigate().GoToUrl("https://localhost:44324");
            driver.FindElement(By.Id("goToApp")).Click();
            driver.FindElement(By.Id("cardEdit43")).Click(); 
            driver.FindElement(By.Id("cardName")).SendKeys("Selenyum düzenleme deneme");
            driver.FindElement(By.Id("cardExpert")).SendKeys("Fatih genc test");
            driver.FindElement(By.Id("cardNo")).SendKeys("21");
            driver.FindElement(By.Id("cardDefinition")).SendKeys("lorem ipsum deneme");
            driver.FindElement(By.Id("cardNotes")).SendKeys("lorem deneme");
            driver.FindElement(By.Id("submit")).Click();

            IWebElement element = driver.FindElement(By.Id("editAlert"));
            string textContent = element.Text;
            Assert.AreEqual("Kart başarıyla güncellendi!", textContent);
        }
    }
}
