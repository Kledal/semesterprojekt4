using System.Web.Mvc;
using Makerlab.Controllers;
using Makerlab.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Moq; 
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Makerlabr.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        private UsersController _uut;

        [SetUp]
        public void setup()
        {
            var _uut = new UsersController();
            
        }

        [Test]
        public void DeleteAction_Returns_UsersviewResult()
        {

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("SOMEUSER");
            mock.SetupGet(x => x.HttpContext.Request.IsAuthenticated).Returns(true);
            _uut.ControllerContext = mock.Object;
            
   
            
            ActionResult result = _uut.Delete(2); //  uut modtager et ID.
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [Test]
        public void Details_Returns_HttpBadRequestWhenIdIsNull()
        {
            ActionResult result = _uut.Details(null);

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [Test]
        public void Edit_Returns_HttpBadRequestWhenIdIsNull()
        {

            ActionResult result = _uut.Edit((int?)null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [Test]
        public void Edit_Returns_HttpNotFoundWhenPrinterIsNull()
        {
            var user = new User();
            user.Id = 0;
            ActionResult result = _uut.Edit(user.Id);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [Test]
        public void Edit_Returns_ViewWhenRun()
        {
            var user = new User();
            user.Id = 3;
            ActionResult result = _uut.Edit(user.Id);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
