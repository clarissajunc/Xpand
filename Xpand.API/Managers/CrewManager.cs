using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Xpand.API.Domain.Models;
using Xpand.API.Managers.Abstractions;

namespace Xpand.API.Managers
{
    public class CrewManager: ICrewManager
    {
        private readonly HttpClient _httpClient;

        private readonly ServicesConfiguration _servicesConfig;

        public CrewManager(HttpClient httpClient, IOptions<ServicesConfiguration> options)
        {
            _httpClient = httpClient;
            _servicesConfig = options.Value;
        }

        public async Task<IEnumerable<Crew>> GetAllCrewsAsync()
        {
            var response = await _httpClient.GetAsync($"{_servicesConfig.CrewsUrl}/crews");

            var content = JsonConvert.DeserializeObject<IEnumerable<Crew>?>(
                        await response.Content.ReadAsStringAsync()
             );

            return content ?? new List<Crew>();
        }

        public async Task<IEnumerable<Human>> GetAllCaptainsAsync()
        {
            var response = await _httpClient.GetAsync($"{_servicesConfig.CrewsUrl}/captains");

            var content = JsonConvert.DeserializeObject<IEnumerable<Human>?>(
                        await response.Content.ReadAsStringAsync()
             );

            return content ?? new List<Human>();
        }
    }
}
