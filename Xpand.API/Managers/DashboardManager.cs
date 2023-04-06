using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Xpand.API.Models;

namespace Xpand.API.Managers
{
    public class DashboardManager : IDashboardManager
    {
        private readonly HttpClient _httpClient;

        private readonly ServicesConfiguration _servicesConfig;

        public DashboardManager(HttpClient httpClient, IOptions<ServicesConfiguration> options)
        {
            _httpClient = httpClient;
            _servicesConfig = options.Value;
        }

        public async Task<IEnumerable<Planet>> GetDashboardAsync()
        {
            var planets = await GetAllPlanetsAsync();
            var crews = await GetAllCrewsAsync();

            foreach (var planet in planets)
            {
                //Add null check
                planet.Robots = crews.Where(c => c.Id == planet.CrewId)
                    .SelectMany(c => c.Robots)
                    .Select(r => r.Name)
                    .ToList();
            }
            return planets;
        }

        private async Task<IEnumerable<Planet>> GetAllPlanetsAsync()
        {
            var response = await _httpClient.GetAsync($"{_servicesConfig.PlanetsUrl}/planets");

            var content = JsonConvert.DeserializeObject<IEnumerable<Planet>>(
                        await response.Content.ReadAsStringAsync()
             );
            //TODO add validation

            return content;
        }

        private async Task<IEnumerable<Crew>> GetAllCrewsAsync()
        {
            var response = await _httpClient.GetAsync($"{_servicesConfig.CrewsUrl}/crews");

            var content = JsonConvert.DeserializeObject<IEnumerable<Crew>>(
                        await response.Content.ReadAsStringAsync()
             );

            //TODO add validation

            return content;
        }
    }
}
