using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OralData.Backend.Controllers;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using OralData.Backend.UnitsOfWork;
using OralData.Responses;
using OralData.Backend.Repositories;

namespace OralData.UnitTest.Controllers
{
    [TestClass]
    public class CourseEnrolledControllerTests
    {
        private CourseEnrolledController _controller;
        private DataContext _context;
        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);

            var repository = new GenericRepository<CourseEnrolled>(_context); // Crear un repositorio
            var unitOfWork = new GenericUnitOfWork<CourseEnrolled>(repository); // Usar el repositorio en el Unit of Work

            _controller = new CourseEnrolledController(unitOfWork, _context);
        }

        [TestMethod]
        public async Task GetCombo_Returns_EmptyList_When_NoCoursesEnrolled()
        {
            // Act
            var result = await _controller.GetComboAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedCourses = okResult.Value as List<CourseEnrolled>;
            Assert.IsNotNull(returnedCourses);
            Assert.AreEqual(0, returnedCourses.Count);
        }



    }
}
