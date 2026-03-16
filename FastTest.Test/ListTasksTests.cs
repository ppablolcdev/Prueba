using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Test
{
    public class ListTasksTests : IntegrationTestBase
    {
        public ListTasksTests(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task GetTasks_ReturnsOk()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/tasks");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Test", "fake-token");

            request.Headers.Add("X-Page", "1");
            request.Headers.Add("X-PageSize", "10");

            var response = await _client.SendAsync(request);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
