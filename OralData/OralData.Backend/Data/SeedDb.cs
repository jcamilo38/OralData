using Microsoft.EntityFrameworkCore;
using OralData.Backend.Data;
using OralData.Backend.services;
using OralData.Shared.Entities;
using OralData.Shared.Responses;

namespace OralData.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;

        public SeedDb(DataContext context, IApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Specialties.Any())
            {
                _context.Specialties.Add(new Specialtie { Name = "Blanqueamiento dental" });
                _context.Specialties.Add(new Specialtie { Name = "Carillas dentales" });
                _context.Specialties.Add(new Specialtie { Name = "Implantes dentales " });
                _context.Specialties.Add(new Specialtie { Name = "Ortodoncia" });
                _context.Specialties.Add(new Specialtie { Name = "Periodoncia" });
                _context.Specialties.Add(new Specialtie { Name = "Endodoncia" });
                _context.Specialties.Add(new Specialtie { Name = "Prótesis dentales" });
                _context.Specialties.Add(new Specialtie { Name = "Cirugía oral" });
                _context.Specialties.Add(new Specialtie { Name = "Carillas bucales de Porcelana" });
                _context.Specialties.Add(new Specialtie { Name = "Férula dental" });
                _context.Specialties.Add(new Specialtie { Name = "Ortodoncia invisible" });
                _context.Specialties.Add(new Specialtie { Name = "Prótesis dental removible" });
                _context.Specialties.Add(new Specialtie { Name = "Sellador individual" });
                _context.Specialties.Add(new Specialtie { Name = "Pulpotomia" });
                _context.Specialties.Add(new Specialtie { Name = " Alargamiento de corona" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                var responseCountries = await _apiService.GetAsync<List<CountryResponse>>("/v1", "/countries");
                if (responseCountries.WasSuccess)
                {
                    var countries = responseCountries.Result!;
                    foreach (var countryResponse in countries)
                    {
                        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            var responseStates = await _apiService.GetAsync<List<StateResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.WasSuccess)
                            {
                                var states = responseStates.Result!;
                                foreach (var stateResponse in states!)
                                {
                                    var state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        var responseCities = await _apiService.GetAsync<List<CityResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.WasSuccess)
                                        {
                                            var cities = responseCities.Result!;
                                            foreach (var cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                var city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}
