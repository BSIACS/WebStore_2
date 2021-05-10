using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;


namespace WebStore.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests //57:50
    {
        [TestMethod()]
        public void Index_Returns_View()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void Blog_Returns_View()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            var result = controller.Blog();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void BlogSingle_Returns_View()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            var result = controller.BlogSingle();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void SecondAction_Returns_View()
        {
            //Arrange
            HomeController controller = new HomeController();
            string expected_content_string = "Second controllers action";

            //Act
            var result = controller.SecondAction();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ContentResult));
            Assert.AreEqual(expected_content_string, (result as ContentResult).Content);
        }

        [TestMethod()]
        public void Error404_Returns_View()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            var result = controller.Error404();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void ErrorStatus404_RedirectToError404()
        {
            //Arrange
            HomeController controller = new HomeController();
            const string error_status_code = "404";

            //Act
            var result = controller.ErrorStatus(error_status_code);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual((result as RedirectToActionResult).ActionName, nameof(controller.Error404));
            Assert.IsNull((result as RedirectToActionResult).ControllerName);
        }

        //[TestMethod(), ExpectedException(typeof(ApplicationException))]
        //public void Throw_thrown_ApplicationException()
        //{
        //    HomeController controller = new HomeController();
        //    var expected_exception_message = "Test";


        //    var result = controller.Throw(expected_exception_message);
        //}

        [TestMethod()]
        public void Throw_Thrown_ApplicationException()
        {
            //Arrange
            HomeController controller = new HomeController();
            var expected_exception_message = "Test";

            #region Alternative logic
            //Exception actual_exception = null;

            //try
            //{
            //    controller.Throw(expected_exception_message);
            //}
            //catch (Exception e) {
            //    actual_exception = e;
            //}

            //Assert.IsInstanceOfType(actual_exception, typeof(ApplicationException));
            //Assert.AreEqual(expected_exception_message, actual_exception.Message);
            #endregion
            //Act, Assert
            Assert.ThrowsException<ApplicationException>(() => {
                controller.Throw(expected_exception_message);
            }, expected_exception_message);
        }

        [TestMethod()]
        public void ContactUs_Returns_View()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            var result = controller.ContactUs();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}