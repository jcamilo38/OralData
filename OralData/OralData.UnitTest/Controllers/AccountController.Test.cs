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
namespace OralData.UnitTest.Controllers
{
    [TestClass]
    public class AccountsControllerTest
    {
        private DbContextOptions<DataContext> _options;
        private Mock<IGenericUnitOfWork<User>> _unitOfWorkMock;
        private Mock<IFileStorage> _fileStorageMock;
        private Mock<IMailHelper> _mailHelperMock;
        private Mock<IConfiguration> _configurationMock;

        public AccountsControllerTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _unitOfWorkMock = new Mock<IGenericUnitOfWork<User>>();
        }

        [TestMethod]
        public async Task Put_Updates_User_Successfully()
        {
            // Arrange
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
                    mailHelperMock.Object
                );

                // Simula un usuario existente para actualizar
                var userToUpdate = new User { /* Proporciona detalles del usuario */ };

                // Configura el comportamiento del mock para GetUserAsync
                userHelperMock.Setup(mock => mock.GetUserAsync(It.IsAny<string>()))
                    .ReturnsAsync(userToUpdate);



                // Act
                var result = await controller.Put(userToUpdate);

                Console.WriteLine($"Tipo de resultado: {result.GetType()}");

                //Assert

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));


                //Clean up
                context.Database.EnsureDeleted();
                context.Dispose();
            }
        }


    }
}