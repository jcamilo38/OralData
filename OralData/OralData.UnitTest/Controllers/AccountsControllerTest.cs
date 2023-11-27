using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using OralData.Backend.Controllers;
using OralData.Backend.Data;
using OralData.Backend.Helpers;
using OralData.Backend.Interfaces;
using OralData.Shared.DTOs;
using OralData.Shared.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OralData.UnitTest.Controllers
{
    [TestClass]
    public class AccountsControllerTest
    {
        private DbContextOptions<DataContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public async Task Put_Updates_User_Successfully()
        {
            using (var context = new DataContext(_options))
            {
                var userHelperMock = new Mock<IUserHelper>();
                var configurationMock = new Mock<IConfiguration>();
                var fileStorageMock = new Mock<IFileStorage>();
                var mailHelperMock = new Mock<IMailHelper>();

                var controller = new AccountsController(
                    userHelperMock.Object,
                    configurationMock.Object,
                    fileStorageMock.Object,
                    mailHelperMock.Object,
                    context // Provide the data context
                );

                // Simulate an existing user to update
                var userToUpdate = new User { /* User details */ };

                // Configure mock behavior for GetUserAsync
                userHelperMock.Setup(mock => mock.GetUserAsync(It.IsAny<string>()))
                    .ReturnsAsync(userToUpdate);

                // Act
                var result = await controller.Put(userToUpdate);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

                // Clean up
                context.Database.EnsureDeleted();
                context.Dispose();
            }
        }


    }

}

