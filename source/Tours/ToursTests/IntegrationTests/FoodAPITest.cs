using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ToursAPI;

using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ToursTests.IntegrationTests
{
    public class FoodAPITest
    {
        private readonly HttpClient _client;

        public FoodAPITest()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath("/home/platosha/Desktop/BMSTU/7sem/Testing/source/Tours/ToursAPI")
                    .AddJsonFile("appsettings.json")
                    .Build())
                .UseUrls($"https://localhost:{config.GetValue<int>("Port")}")
                .UseStartup<Startup>());
            
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task FoodGetAllTestAsync(string method)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/v1/Foods");
            
            //Act
            var response = await _client.SendAsync(request);
            
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}