using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Test
{
    public class CreateTaskTests : IntegrationTestBase
    {
        public CreateTaskTests(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task CreateTask_ReturnsCreated()
        {
            var payload = new
            {
                Title = "Test Task",
                Description = "DEscripcion prueba"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/tasks");
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Test", "fake-token");

            request.Content = JsonContent.Create(payload);

            var response = await _client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }
    }
}
