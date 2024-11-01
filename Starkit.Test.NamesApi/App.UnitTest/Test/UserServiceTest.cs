using App.Controllers;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace App.UnitTest.Test
{
    public class UserServiceTest
    {
        [Fact]
        public void GetUsers_NoFilters_ReturnsAllUsers()
        {
            // Arrange
            var userServicesMock = new Mock<UserServices>();
            var loggerMock = new Mock<ILogger<UserController>>();

            userServicesMock.Setup(s => s.GetAllUsers()).Returns(new List<User> { new User { name = "Adrian", gender = "M" } });
            var controller = new UserController(userServicesMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetUsers(null, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.NotEmpty(users);
            Assert.Equal("Adrian", users[0].name);
        }

        [Fact]
        public void GetUsers_WithGenderFilter_ReturnsFilteredUsers()
        {
            // Arrange
            var userServicesMock = new Mock<UserServices>();
            var loggerMock = new Mock<ILogger<UserController>>();

            userServicesMock.Setup(s => s.GetFilteredUsers("M", null)).Returns(new List<User> { new User { name = "Adrian", gender = "M" } });
            var controller = new UserController(userServicesMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetUsers("M", null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Single(users);
            Assert.Equal("Adrian", users[0].name);
        }

        [Fact]
        public void GetUsers_WithStartWithFilter_ReturnsFilteredUsers()
        {
            // Arrange
            var userServicesMock = new Mock<UserServices>();
            var loggerMock = new Mock<ILogger<UserController>>();

            userServicesMock.Setup(s => s.GetFilteredUsers(null, "A")).Returns(new List<User> { new User { name = "Adrian", gender = "M" } });
            var controller = new UserController(userServicesMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetUsers(null, "A");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Single(users);
            Assert.Equal("Adrian", users[0].name);
        }

        [Fact]
        public void GetUsers_WithInvalidFilters_ReturnsBadRequest()
        {
            // Arrange
            var userServicesMock = new Mock<UserServices>();
            var loggerMock = new Mock<ILogger<UserController>>();

            userServicesMock.Setup(s => s.GetFilteredUsers("X", "Z")).Returns((List<User>?)null); // Simular caso de filtros inválidos
            var controller = new UserController(userServicesMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetUsers("X", "Z");

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No valid filter was added. Please try again with a valid filter or leave it empty.", badRequestResult.Value);
        }
    }
}
