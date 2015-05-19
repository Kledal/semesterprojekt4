using System;
using Makerlab.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Makerlabr.Tests.Units
{
    [TestFixture]
    public class FileUnitTests
    {
        private File _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new File();
        }

        [Test]
        public void File_ValidFilename_WithInvalidName_ShouldReturnFalse()
        {
            _uut.FileName = "test.x3g";
            Assert.That(_uut.ValidFilename(), Is.False);
        }

        [Test]
        public void File_ValidFilename_WithValidName_ShouldReturnTrue()
        {
            _uut.FileName = "test.gcode";
            Assert.That(_uut.ValidFilename(), Is.True);
        }
    }
}
