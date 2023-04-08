using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xpand.API.Domain.Models;
using Xpand.API.Managers.Abstractions;

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
                //add mapping
            }
            return planets;
        }

        public async Task<HttpStatusCode> UpdatePlanetAsync(int planetId, EditPlanet editPlanet)
        {
            if (planetId == default)
            {
                throw new ArgumentNullException(nameof(planetId));
            }

            if (editPlanet == null)
            {
                throw new ArgumentNullException(nameof(editPlanet));
            }

            if (planetId != editPlanet.Id)
            {
                throw new ArgumentException($"The {planetId} cannot be different", nameof(editPlanet));
            }

            var response = await _httpClient.PostAsync(
                $"{_servicesConfig.PlanetsUrl}/planets/{planetId}",
                new StringContent(JsonConvert.SerializeObject(editPlanet), Encoding.UTF8, "application/json"));

            return response.StatusCode;
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
