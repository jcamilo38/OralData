using Microsoft.AspNetCore.Mvc;
using OralData.Backend.Interfaces;
using OralData.Shared.Entities;

namespace OralData.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DentalSpecialtieController : GenericController<DentalSpecialtie>
    {
        public DentalSpecialtieController(IGenericUnitOfWork<DentalSpecialtie> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
