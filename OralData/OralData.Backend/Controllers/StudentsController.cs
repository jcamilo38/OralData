using Microsoft.AspNetCore.Mvc;
using OralData.Backend.Controllers;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Backend.UnitsOfWork;
using OralData.Shared.Entities;

namespace OralData.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : GenericController<Student>
    {
        public StudentsController(IGenericUnitOfWork<Student> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
        }

    }
}

