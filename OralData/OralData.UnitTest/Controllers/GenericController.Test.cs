using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Moq;
using OralData.Backend.Controllers;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Responses;
using OralData.Shared.Entities;
using Orders.Shared.DTOs;

namespace OralData.UnitTest.Controllers
{
    [TestClass]
    public class GenericControllerTest

    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IGenericUnitOfWork<Specialtie>> _UnitOfWorkmock;

        public GenericControllerTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _UnitOfWorkmock = new Mock<IGenericUnitOfWork<Specialtie>>();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);
            var pagination = new PaginationDTO();

            // Act
            var result = await controller.GetAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean Up
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);
            var pagination = new PaginationDTO();

            // Act
            var result = await controller.GetPagesAsync(pagination) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            // Clean Up
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task GetAsync_ReturnNoFoubdWhenEntityNotFound()
        {
            // Arrange
            using var context = new DataContext(_options);
            var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);

            // Act
            var result = await controller.GetAsync(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

            // Clean Up
            context.Database.EnsureDeleted();
        }

        //[TestMethod]
        //public async Task GetAsync_ReturnsRecord()
        //{
        //    // Arrange
        //    using var context = new DataContext(_options);
        //    var Specialtie = new Specialtie { Id = 1, Name = "Any" };

        //    _UnitOfWorkmock.Setup(x => x.GetAsync(Specialtie.Id))
        //        .ReturnsAsync(Specialtie);

        //    var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);

        //    // Act
        //    var result = await controller.GetAsync(Specialtie.Id) as OkObjectResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(200, result.StatusCode);
        //    _UnitOfWorkmock.Verify(x => x.GetAsync(Specialtie.Id), Times.Once());

        //    // Clean Up
        //    context.Database.EnsureDeleted();

        //}

        [TestMethod]
        public async Task PostAsync_ReturnsOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var specialtie = new Specialtie { Id = 1, Name = "Any" };
            var response = new Response<Specialtie> { WasSuccess = true, Result = specialtie };

            _UnitOfWorkmock.Setup(x => x.AddAsync(specialtie))
                .ReturnsAsync(response);

            var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);

            //    Act
            var result = await controller.PostAsync(specialtie) as OkObjectResult;

            //     Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var okResult = result.Value as Specialtie;
            Assert.AreEqual(specialtie.Name, okResult!.Name);
            _UnitOfWorkmock.Verify(x => x.AddAsync(specialtie), Times.Once());

            // Clean Up
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task PostAsync_ReturnsBadRequest()
        {
            // Arrange
            using var context = new DataContext(_options);
            var specialtie = new Specialtie { Id = 1, Name = "Any" };
            var response = new Response<Specialtie> { WasSuccess = false };

            _UnitOfWorkmock.Setup(x => x.AddAsync(specialtie))
                .ReturnsAsync(response);

            var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);

            //    Act
            var result = await controller.PostAsync(specialtie) as BadRequestObjectResult;

            //     Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            _UnitOfWorkmock.Verify(x => x.AddAsync(specialtie), Times.Once());

            // Clean Up
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task PutAsync_ReturnOkResult()
        {
            // Arrange
            using var context = new DataContext(_options);
            var specialtie = new Specialtie { Id = 1, Name = "Any" };
            var response = new Response<Specialtie> { WasSuccess = true };
            _UnitOfWorkmock.Setup(x => x.UpdateAsync(specialtie)).ReturnsAsync(response);
            var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);

            //    Act
            var result = await controller.PutAsync(specialtie) as OkObjectResult;

            //     Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            _UnitOfWorkmock.Verify(x => x.UpdateAsync(specialtie), Times.Once());

            // Clean Up
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task PutAsync_ReturnBadRequest()
        {
            // Arrange
            using var context = new DataContext(_options);
            var specialtie = new Specialtie { Id = 1, Name = "Any" };
            var response = new Response<Specialtie> { WasSuccess = false };
            _UnitOfWorkmock.Setup(x => x.UpdateAsync(specialtie)).ReturnsAsync(response);
            var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);

            //    Act
            var result = await controller.PutAsync(specialtie) as BadRequestObjectResult;

            //     Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            _UnitOfWorkmock.Verify(x => x.UpdateAsync(specialtie), Times.Once());

            // Clean Up
            context.Database.EnsureDeleted();
        }

        //[TestMethod]
        //public async Task DeleteAsync_ReturnsNoContentWhenEntityDelete()
        //{
        //    // Arrange
        //    using var context = new DataContext(_options);
        //    var specialtie = new Specialtie { Id = 1, Name = "Any" };
        //    _UnitOfWorkmock.Setup(x => x.GetAsync(specialtie.Id)).ReturnsAsync(specialtie);
        //    var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);

        //    //    Act
        //    var result = await controller.DeleteAsync(specialtie.Id) as BadRequestObjectResult;

        //    //     Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(204, result.StatusCode);
        //    _UnitOfWorkmock.Verify(x => x.GetAsync(specialtie.Id), Times.Once());

        //    // Clean Up
        //    context.Database.EnsureDeleted();
        //}

        //[TestMethod]
        //public async Task DeleteAsync_ReturnsNoContentWhenEntityNoFound()
        //{
        //    // Arrange
        //    using var context = new DataContext(_options);
        //    var specialtie = new Specialtie { Id = 1, Name = "Any" };
        //    var controller = new GenericController<Specialtie>(_UnitOfWorkmock.Object, context);

        //    // Act
        //    var result = await controller.DeleteAsync(specialtie.Id) as NoContentResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(204, result.StatusCode);

        //    // Clean Up
        //    context.Database.EnsureDeleted();
        //}
    }
}