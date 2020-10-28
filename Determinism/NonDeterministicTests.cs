using System;
using dotnet_unit_test;
using Xunit;

namespace Determinism
{
    public class NonDeterministicTests
    {
        /// <summary>
        /// Not deterministic.
        /// </summary>
        [Fact]
        public void GetTodaysDate_Returns_formatted_date()
        {
            // Arrange

            // Act
            var todaysDate = PersonalAssistant.GetTodaysDate();

            // Assert
            Assert.NotNull(todaysDate);
            Assert.NotEmpty(todaysDate);
            Assert.True(todaysDate.IndexOf("2020") > 0, "today's date should contain the year 2020");
        }

        [Fact]
        public void GetTodaysDate_Returns_formatted_date_in_Portuguese()
        {
            // Arrange

            // Act
            var todaysDate = PersonalAssistant.GetTodaysDate();

            // Assert
            Assert.NotNull(todaysDate);
            Assert.NotEmpty(todaysDate);
            Assert.True(todaysDate.IndexOf("de ") > 0, $"today's date should contain the article 'de'. got this instead: {todaysDate}");
        }
    }
}
