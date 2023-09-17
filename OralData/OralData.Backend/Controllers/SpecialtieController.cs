using Microsoft.AspNetCore.Mvc;
using OralData.Backend.Interfaces;
using OralData.Shared.Entities;

namespace OralData.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialtieController : GenericController<Specialtie>
    {
        public SpecialtieController(IGenericUnitOfWork<Specialtie> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
