using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Paylocity.Payroll.Operation.Models;
using Xunit;

namespace Paylocity.Payroll.ApiService
{
    public class PayrollControllerIntegrationTests : IClassFixture<TestFixture<TestStartup>>
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public PayrollControllerIntegrationTests(TestFixture<TestStartup> fixture)
        {
            httpClient = fixture.httpClient;

            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        private HttpClient httpClient { get; }


        [Fact]
        public async Task Api_Get_Should_Return_Success_With_Data()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), "api/Payroll/paycheck/v1");

            var employeeModel = new EmployeeModel
            {
                Name = "Joe doe"
            };
            request.Content = new StringContent(JsonSerializer.Serialize(employeeModel, jsonSerializerOptions),
                Encoding.UTF8, "application/json");
            // Act

            // Act
            var response = await httpClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            var paycheckModel = JsonSerializer.Deserialize<PaycheckModel>(content, jsonSerializerOptions);

            paycheckModel.Should().NotBeNull();
        }

        [Fact]
        public async Task Api_Get_Should_Return_BadRequest()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), "api/Payroll/paycheck/v1");

            var employeeModel = new EmployeeModel
            {
                Name = ""
            };
            request.Content = new StringContent(JsonSerializer.Serialize(employeeModel, jsonSerializerOptions),
                Encoding.UTF8, "application/json");
            // Act

            // Act
            var response = await httpClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Api_Get_Should_Return_Success_With_No_Discount_Data()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("POST"), "api/Payroll/paycheck/v1");

            var employeeModel = new EmployeeModel
            {
                Name = "Joe doe"
            };
            request.Content = new StringContent(JsonSerializer.Serialize(employeeModel, jsonSerializerOptions),
                Encoding.UTF8, "application/json");
            // Act
            const decimal expectedDeductible = 38.46m;
            var response = await httpClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            var paycheckModel = JsonSerializer.Deserialize<PaycheckModel>(content, jsonSerializerOptions);

            paycheckModel.Should().NotBeNull();

            paycheckModel.TotalDeductible.Should().Be(expectedDeductible);
        }

        //and more integration tests can be added.
    }
}