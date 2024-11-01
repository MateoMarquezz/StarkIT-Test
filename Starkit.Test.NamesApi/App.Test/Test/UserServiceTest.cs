using App.Controllers;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;
namespace App.UnitTest.Test
{
    public class UserControllerTests
    {
        [Fact]
        public void GetUsers_NoFilters_ReturnsAllUsers()
        {
            var mockUserServices = new Mock<IUserServices>();
            var mockLogger = new Mock<ILogger<UserController>>();

            var testUsers = new List<User>
            {
                new User { name = "Adrian", gender = "M" },
                new User { name = "Brenda", gender = "F" }
            };

            mockUserServices.Setup(s => s.GetAllUsers()).Returns(testUsers);

            var controller = new UserController(mockUserServices.Object, mockLogger.Object);

            var result = controller.GetUsers(null, null);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Equal(2, users.Count);
            Assert.Contains(users, u => u.name == "Adrian" && u.gender == "M");
            Assert.Contains(users, u => u.name == "Brenda" && u.gender == "F");
        }

        [Fact]
        public void GetUsers_WithGenderFilter_ReturnsFilteredUsers()
        {
            
            var mockUserServices = new Mock<IUserServices>();
            var mockLogger = new Mock<ILogger<UserController>>();

            var filteredUsers = new List<User>
            {
                new User { name = "Adrian", gender = "M" }
            };

            mockUserServices.Setup(s => s.GetFilteredUsers("M", null)).Returns(filteredUsers);

            var controller = new UserController(mockUserServices.Object, mockLogger.Object);

            
            var result = controller.GetUsers("M", null);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Single(users);
            Assert.Equal("Adrian", users[0].name);
            Assert.Equal("M", users[0].gender);
        }

        [Fact]
        public void GetUsers_WithStartWithFilter_ReturnsFilteredUsers()
        {
            
            var mockUserServices = new Mock<IUserServices>();
            var mockLogger = new Mock<ILogger<UserController>>();

            var filteredUsers = new List<User>
            {
                new User { name = "Adrian", gender = "M" }
            };

            mockUserServices.Setup(s => s.GetFilteredUsers(null, "A")).Returns(filteredUsers);

            var controller = new UserController(mockUserServices.Object, mockLogger.Object);

            
            var result = controller.GetUsers(null, "A");

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<List<User>>(okResult.Value);
            Assert.Single(users);
            Assert.Equal("Adrian", users[0].name);
        }

        [Fact]
        public void GetUsers_WithInvalidFilters_ReturnsBadRequest()
        {
            
            var mockUserServices = new Mock<IUserServices>();
            var mockLogger = new Mock<ILogger<UserController>>();

            mockUserServices.Setup(s => s.GetFilteredUsers("X", "Z")).Returns((List<User>?)null);

            var controller = new UserController(mockUserServices.Object, mockLogger.Object);

            
            var result = controller.GetUsers("X", "Z");

            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No valid filter was added. Please try again with a valid filter or leave it empty.", badRequestResult.Value);
        }
    }
}
