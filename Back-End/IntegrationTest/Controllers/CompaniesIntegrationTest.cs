using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web;
using Xunit;

namespace IntegrationTest.Controllers
{
    public class CompaniesIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CompaniesIntegrationTest()
        {
            _factory = new CustomWebApplicationFactory<Startup>();
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Test]
        public async Task CreateCompany()
        {
            var companieRequest = new Company("SERASA");

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(companieRequest);

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("../companies/", content);

            Assert.AreEqual((int)HttpStatusCode.Created, (int)response.StatusCode);
        }
    }
}
