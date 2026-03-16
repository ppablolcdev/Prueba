using FastTest.Application.Querys.GetMetrics;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FastTest.Test
{
    public class MetricsTests : IntegrationTestBase
    {
        public MetricsTests(CustomWebApplicationFactory factory) : base(factory) { }

        [Fact]
        public async Task Metrics_ReturnsOkWithStats()
        {
            var response = await _client.GetAsync("/metrics");
            response.EnsureSuccessStatusCode();

            var metrics = await response.Content.ReadFromJsonAsync<MetricsResult>();
            Assert.NotNull(metrics);
            Assert.True(metrics.Total >= 0);
        }
    }
}
