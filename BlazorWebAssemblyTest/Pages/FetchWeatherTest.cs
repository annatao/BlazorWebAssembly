using BlazorWebAssembly.Pages;
using Bunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace BlazorWebAssemblyTest.Pages
{
    public class FetchWeatherTest
    {
        [Fact]
        public void TestFetchWeather_NullCurrentWeather()
        {
            // Arrange
            using var ctx = new Bunit.TestContext();
            ctx.Services.AddMockConfiguration();
            ctx.Services.AddMockHttpClient();

            // Act
            var component = ctx.RenderComponent<FetchWeather>();

            // Assert
            var result = @"<p><em>Loading...</em></p>";
            component.MarkupMatches(result);
        }

        [Fact]
        public void TestFetchWeather_NotNullCurrentWeather()
        {
            // Arrange
            using var ctx = new Bunit.TestContext();
            var mockConfiguration = ctx.Services.AddMockConfiguration();

            var mockClient = ctx.Services.AddMockHttpClient();
            var mockAPI = String.Format(mockConfiguration["OpenWeatherMapAPIUrl"], "Stockholm");
            var mockCurrentWeather = new Mock<FetchWeather.CurrentWeather>().SetupAllProperties().Object;
            mockCurrentWeather.Main = new Mock<FetchWeather.Main>().SetupAllProperties().Object;
            mockCurrentWeather.Coord = new Mock<FetchWeather.Coord>().SetupAllProperties().Object;
            mockCurrentWeather.Weather = new[] { new Mock<FetchWeather.Weather>().SetupAllProperties().Object };
            mockCurrentWeather.Wind = new Mock<FetchWeather.Wind>().SetupAllProperties().Object;
            mockCurrentWeather.Clouds = new Mock<FetchWeather.Clouds>().SetupAllProperties().Object;
            mockCurrentWeather.Sys = new Mock<FetchWeather.Sys>().SetupAllProperties().Object;
            mockClient.When(mockAPI).RespondJson(mockCurrentWeather);

            // Act
            var component = ctx.RenderComponent<FetchWeather>();

            // Assert
            component.WaitForState(() => component.FindAll(".table").Count > 0); // blocks until the predicate passes or the timeout is passed (default 1 sec).
            component.WaitForAssertion(() => NUnit.Framework.Assert.AreEqual(1, component.FindAll(".table").Count)); 
        }
    }
}
