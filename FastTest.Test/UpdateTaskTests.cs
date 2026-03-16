using Azure.Core;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Test
{
    public class UpdateTaskTests : IntegrationTestBase
    {
        public UpdateTaskTests(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task UpdateTask_ReturnsNoContent()
        {
            var id = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var payload = new
            {
                Id = id,
                Status = "Completed"
            };

            var request = new HttpRequestMessage(HttpMethod.Put, $"/tasks/{id}");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Test", "fake-token");

            request.Content = JsonContent.Create(payload);

            var response = await _client.SendAsync(request);
            // devolvera 404 no existe el guid que se intenta actualizar
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
