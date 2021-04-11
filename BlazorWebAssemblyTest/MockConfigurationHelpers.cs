using Bunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWebAssemblyTest
{
    public static class MockConfigurationHelpers
    {
        public static IConfiguration AddMockConfiguration(this TestServiceProvider services)
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
               .Build();
            services.AddSingleton<IConfiguration>(config);
            return config;
        }
    }
}
