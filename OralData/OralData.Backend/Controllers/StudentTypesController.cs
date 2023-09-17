using Microsoft.AspNetCore.Mvc;
using OralData.Backend.Interfaces;
using OralData.Backend.UnitsOfWork;
using OralData.Shared.Entities;

namespace OralData.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentTypesController : GenericController<StudentType>
    {
        public StudentTypesController(IGenericUnitOfWork<StudentType> unit) : base(unit)
        {
        }
    }
}