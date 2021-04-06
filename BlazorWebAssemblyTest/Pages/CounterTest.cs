using BlazorWebAssembly.Pages;
using Bunit;
using System;
using Xunit;

namespace BlazorWebAssemblyTest.Pages
{
    public class CounterTest
    {
        [Fact]
        public void CountDisplayShouldBeUpdatedWhenButtonIsClicked()
        {
            // Arrange
            using var ctx = new TestContext();
            var component = ctx.RenderComponent<Counter>();

            Random rd = new();
            int rand_num = rd.Next(1, 200);

            // Act
            for (var j = 0; j<rand_num; j++)
            {
                component.Find("button").Click();
            }

            // Assert
            var result = $"<h1>Counter</h1><p>Current count: {rand_num}</p><button class='btn btn-primary'>Click me</button>";
            component.MarkupMatches(result);
        }

    }
}