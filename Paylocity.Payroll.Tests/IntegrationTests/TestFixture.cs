using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Paylocity.Payroll.ApiService
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestServer _server;

        public TestFixture()
        {
            var builder = new WebHostBuilder().UseStartup<TStartup>();

            _server = new TestServer(builder);

            httpClient = _server.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:6000");
        }

        public HttpClient httpClient { get; }

        public void Dispose()
        {
            httpClient.Dispose();
            _server.Dispose();
        }
    }
}