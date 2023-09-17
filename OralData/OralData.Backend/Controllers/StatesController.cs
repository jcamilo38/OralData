using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OralData.Backend.Data;
using OralData.Backend.Interfaces;
using OralData.Shared.Entities;

namespace OralData.Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class StatesController : GenericController<State>
    {
        private readonly DataContext _context;

        public StatesController(IGenericUnitOfWork<State> unitOfWork, DataContext context) : base(unitOfWork)
        {
            _context = context;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var queryable = _context.States
                .Include(x => x.Cities)
                .AsQueryable();

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var state = await _context.States
                .Include(s => s.Cities)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
    }
}
