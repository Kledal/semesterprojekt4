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

            Assert.IsInstanceOfType(result, typeof(ViewResult));
           // Assert.AreEqual("Controller", result.RouteValues["dashboard"]);

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
        public void Delete_Returns_HttpBadRequestWhenIdIsNull()
        {
            ActionResult result = _uut.Delete(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [Test]
        public void Delete_Returns_HttpNotFoundWhenPrinterIsNull()
        {
            var message = new Message();
            message.Id = 0;
            ActionResult result = _uut.Delete(message.Id);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [Test]
        public void DeleteAction_Returns_Messageview()
        {
            var message = new Message();
            message.Id = 2;
            message.Content = "fielaursen";
            ActionResult result = _uut.Delete(message.Id);

           // ActionResult result = _uut.Delete(2); //  uut modtager et ID.
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        [Test]
        public void DeleteConfirmedAction_Returns_Messageview()
        {
            var sandt = true;
            Assert.AreEqual(sandt, false);
        }



    }
}
