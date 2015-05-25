using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Channels;
using System.Web.Mvc;
using Makerlab;
using Makerlab.Controllers;
using Makerlab.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using NUnit.Framework;

using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Makerlabr.Tests.Controllers
{
    [TestClass]
    public class PrinterControllerTest
    {
        private PrintersController _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new PrintersController();
        }

        [Test]
        public void CreateAction_Returns_IndexViewWhenModelStateIsValid()
        {
            _uut.ModelState.Clear();

            RedirectToRouteResult result = _uut.Create(new Printer()) as RedirectToRouteResult;
            result.RouteValues["Action"].Equals("Index");
            Assert.AreEqual("Action", result.RouteValues["Index"]);
        }

        [Test]
        public void CreateAction_Adds_PrinterToDBWhenModelStateIsValid()
        {
            var set = new Mock<DbSet<Printer>>();
            var mockContext = new Mock<MakerContext>();

            var printers = new List<Printer>();
            mockContext.Setup(r => r.Printers).Returns(set.Object);

            ActionResult result = _uut.Create(new Printer()) as ActionResult;
           
          //   var service = new Printer();
          //  _uut.Create(service);


          
        }

        [Test]
        public void CreateAction_Returns_ViewWhenModelstateIsInvalid()
        {

            _uut.ModelState.Add("data", new ModelState());
            _uut.ModelState.AddModelError("testError", "test");  // Gør modelstate invalid. 

            string viewName = "editPrinter";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Create()).ViewName, "View name should have been {0}", viewName);

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

            var printer = new Printer();
            printer.Id = 0;
            ActionResult result = _uut.Delete(printer.Id);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [Test]
        public void EditAction_Returns_ViewWhenModelstateIsInvalid()
        {

            _uut.ModelState.Add("data", new ModelState());
            _uut.ModelState.AddModelError("testError", "test");  // Gør modelstate invalid. 

            string viewName = "Index";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Create()).ViewName, "View name should have been {0}", viewName);

        }

        [Test]
        public void DeleteAction_Returns_Printerview()
        {

            ActionResult result = _uut.Delete(2); //  uut modtager et ID.
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [Test]
        public void DeleteConfirmedAction_Returns_Printerview()
        {

            RedirectToRouteResult result = _uut.Create(new Printer()) as RedirectToRouteResult;
            ActionResult aresult = _uut.DeleteConfirmed(3);

           // Assert.AreEqual("Index", result.RouteValues["Index"]);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [Test]
        public void DeleteConfirmedAction_Deletes_Printer()
        {
            var john = true;
            Assert.Equals("false", john);
        }

        [Test]
        public void Edit_Returns_HttpBadRequestWhenIdIsNull()
        {
           
            ActionResult result = _uut.Edit((int?) null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [Test]
        public void Edit_Returns_HttpNotFoundWhenPrinterIsNull()
        {

            var printer = new Printer();
            printer.Id = 0;
            ActionResult result = _uut.Edit(printer.Id);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [Test]
        public void Edit_Returns_ViewWhenRun()
        {
            var printer = new Printer();
            printer.Id = 3;
            ActionResult result = _uut.Edit(printer.Id);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
