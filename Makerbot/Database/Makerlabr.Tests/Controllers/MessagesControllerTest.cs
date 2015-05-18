using System;
using System.Web.Mvc;
using Makerlab.Controllers;
using Makerlab.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Makerlabr.Tests.Controllers
{
    [TestClass]
    public class MessagesControllerTest
    {
        private MessagesController _uut;

        [SetUp]
        public void setup()
        {

            _uut = new MessagesController();
        }

        [Test]
        public void IndexAction_Returns_View()
        {
            string viewName = "Index";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);

        }

        [Test]
        public void CreateAction_Returns_IndexViewWhenModelStateIsValid()
        {
            
            _uut.ModelState.Clear();

            RedirectToRouteResult result = _uut.Create(new Message()) as RedirectToRouteResult;


              Assert.AreEqual("Controller", result.RouteValues["dashboard"]);

        }

        [Test]
        public void CreateAction_Adds_MessagesToDBWhenModelStateIsValid()
        {
            ActionResult result = _uut.Create(new Message()) as ActionResult;
        }

        [Test]
        public void CreateAction_Returns_ViewWhenModelstateIsInvalid()
        {
            _uut.ModelState.Add("data", new ModelState());
            _uut.ModelState.AddModelError("testError", "test");  // Gør modelstate invalid. 

            string viewName = "Index";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);

        }

        [Test]
        public void DeleteAction_Returns_Messagesview()
        {
            ActionResult result = _uut.Delete(null); //  uut modtager et ID.
            // Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("HttpNotFound", result);
        }


        [Test]
        public void DeleteConfirmedAction_Returns_Mssageview()
        {
            var sandt = true;
            Assert.AreEqual(sandt, false);
        }

    }
}
