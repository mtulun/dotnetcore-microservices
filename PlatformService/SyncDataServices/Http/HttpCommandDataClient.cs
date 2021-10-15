using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
        }

        public async Task SendPlatformToCommand(PlatformReadDto readDto)
        {
            var httpContent = new StringContent(
              JsonSerializer.Serialize(readDto),
              Encoding.UTF8,
              "application/json");
            var response = await httpClient.PostAsync($"{configuration["CommandService"]}", httpContent);

            System.Console.WriteLine((response.IsSuccessStatusCode) ? "--> Sync POST to CommandService was OK!" : "--> Sync POST to CommandService was not OK!");
        }
    }
}