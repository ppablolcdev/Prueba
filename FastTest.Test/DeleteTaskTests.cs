using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Test
{
    public class DeleteTaskTests : IntegrationTestBase
    {
        public DeleteTaskTests(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task DeleteTask_ReturnsNoContentOrNotFound()
        {
            var id = Guid.NewGuid(); // igual que antes, deberías crear primero la tarea
            var response = await _client.DeleteAsync($"/tasks/{id}");

            Assert.True(
                response.StatusCode == System.Net.HttpStatusCode.NoContent ||
                response.StatusCode == System.Net.HttpStatusCode.NotFound
            );
        }
    }
}
