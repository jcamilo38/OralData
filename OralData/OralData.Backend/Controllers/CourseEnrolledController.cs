using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OralData.Backend.Controllers;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Backend.UnitsOfWork;
using OralData.Shared.Entities;

namespace OralData.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CourseEnrolledController : GenericController<Student>
    {
        public CourseEnrolledController(IGenericUnitOfWork<Student> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
        }

    }
}