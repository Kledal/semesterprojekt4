using System.Web.Mvc;
using Makerlab;
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
            var mock = new Mock<MakerContext>();
            
        }

        [Test]
        public void DeleteAction_Returns_UsersviewResult()
        {


     

            //mock.SetupGet(x => x.Users.Create().Email).Returns("jj@testidp.wayf.dk");
            //mock.SetupGet(x => _uut.CurrentUser).Returns(currentUser);
            //_uut.CurrentUser = mock.Object;
            
            ActionResult result = _uut.Delete(2); //  uut modtager et ID.
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [Test]
        public void Details_Returns_HttpBadRequestWhenIdIsNull()
        {
            var mock = new Mock<MakerContext>();
            var currentUser = new User();

            mock.Setup(x => x.Users).Equals("jj@testidp.wayf.dk");
            mock.Setup(x => _uut.CurrentUser).Returns(currentUser);

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
