using OralData.Shared.Entities;

namespace OralData.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckStudentsAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country { Name = "Colombia" });
                _context.Countries.Add(new Country { Name = "Perú" });
                _context.Countries.Add(new Country { Name = "Chile" });
                _context.Countries.Add(new Country { Name = "Ecuador" });
                _context.Countries.Add(new Country { Name = "Venezuela" });
                _context.Countries.Add(new Country { Name = "Argentina" });
                _context.Countries.Add(new Country { Name = "Uruguay" });
                _context.Countries.Add(new Country { Name = "Paraguay" });

            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckStudentsAsync()
        {
            if (!_context.Students.Any())
            {
                _context.Students.Add(new Student { Name = "Pregrado" });
                _context.Students.Add(new Student { Name = "Posgrado" });

            }

            await _context.SaveChangesAsync();
        }
    }
}
