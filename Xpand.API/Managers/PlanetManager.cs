using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xpand.API.Domain.Models;
using Xpand.API.Managers.Abstractions;

namespace Xpand.API.Managers
{
    public class PlanetManager: IPlanetManager
    {
        private readonly HttpClient _httpClient;

        private readonly ServicesConfiguration _servicesConfig;

        public PlanetManager(HttpClient httpClient, IOptions<ServicesConfiguration> options)
        {
            _httpClient = httpClient;
            _servicesConfig = options.Value;
        }

        public async Task<IEnumerable<Planet>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_servicesConfig.PlanetsUrl}/planets");

            var content = JsonConvert.DeserializeObject<IEnumerable<Planet>?>(
                        await response.Content.ReadAsStringAsync()
             );

            return content ?? new List<Planet>();
        }

        public async Task<HttpStatusCode> UpdateAsync(int planetId, EditPlanet editPlanet)
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
    }
}
