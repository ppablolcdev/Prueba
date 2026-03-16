using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace FastTest.Test
{
    public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory>
    {
        protected readonly HttpClient _client;

        public IntegrationTestBase(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();

            // autenticación fake
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Test", "fake-token");
        }
    }
}
