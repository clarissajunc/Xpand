using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Xpand.API.Domain.Models;
using Xpand.API.Managers.Abstractions;

namespace Xpand.API.Managers
{
    public class CaptainManager: ICaptainManager
    {
        private readonly HttpClient _httpClient;

        private readonly ServicesConfiguration _servicesConfig;

        public CaptainManager(HttpClient httpClient, IOptions<ServicesConfiguration> options)
        {
            _httpClient = httpClient;
            _servicesConfig = options.Value;
        }

        public async Task<IEnumerable<Human>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_servicesConfig.CrewsUrl}/captains");

            var content = JsonConvert.DeserializeObject<IEnumerable<Human>>(
                        await response.Content.ReadAsStringAsync()
             );
            //TODO add validation

            return content;
        }
    }
}
