using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OralData.Backend.Controllers;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Shared.Entities;
using Orders.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OralData.UnitTest.Controllers
{
    [TestClass]
    public class StatesControllerTest
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
        public async Task GetComboAsync_Returns_CorrectResult()
        {
            // Arrange
            using (var context = new DataContext(_options))
            {
                var unitOfWorkMock = new Mock<IGenericUnitOfWork<State>>();
                var controller = new StatesController(unitOfWorkMock.Object, context);

                // Act
                var result = await controller.GetComboAsync(1);

                // Assert
                Assert.IsNotNull(result);
                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);
                var states = okResult.Value as List<State>;
                Assert.IsNotNull(states);

            }
        }

        [TestMethod]
        public async Task GetAsync_Returns_CorrectResult()
        {
            // Arrange
            using (var context = new DataContext(_options))
            {
                var unitOfWorkMock = new Mock<IGenericUnitOfWork<State>>();
                var controller = new StatesController(unitOfWorkMock.Object, context);
                var pagination = new PaginationDTO { Id = 1, Filter = "SomeFilter" };

                // Act
                var result = await controller.GetAsync(pagination);

                // Assert
                Assert.IsNotNull(result);
                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);
                var states = okResult.Value as List<State>;
                Assert.IsNotNull(states);

            }
        }

        [TestMethod]
        public async Task GetPagesAsync_Returns_CorrectResult()
        {
            // Arrange
            using (var context = new DataContext(_options))
            {
                var unitOfWorkMock = new Mock<IGenericUnitOfWork<State>>();
                var controller = new StatesController(unitOfWorkMock.Object, context);
                var pagination = new PaginationDTO { Id = 1, Filter = "SomeFilter", RecordsNumber = 10 };

                // Act
                var result = await controller.GetPagesAsync(pagination);

                // Assert
                Assert.IsNotNull(result);
                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);

            }
        }
    }
}
