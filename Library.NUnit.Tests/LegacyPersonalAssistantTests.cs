using System.Threading.Tasks;
using dotnet_unit_test;
using NUnit.Framework;

namespace Library.NUnit.Tests
{
    public class LegacyPersonalAssistantTests
    {
        [Test]
        public async Task GetWeatherAsync_Returns_Porto()
        {
            // Arrange
            var legacyPersonalAssistant = new LegacyPersonalAssistant();

            // Act
            var weatherReport = await legacyPersonalAssistant.GetWeatherAsync("Porto");

            // Assert
            Assert.NotNull(weatherReport);
            Assert.AreEqual("Porto", weatherReport.name);
        }

        [Test]
        public async Task PerformLongRunningTaskAsync_Returns_true()
        {
            // Arrange
            var legacyPersonalAssistant = new LegacyPersonalAssistant();

            // Act
            var response = await legacyPersonalAssistant.PerformLongRunningTaskAsync(3000);

            // Assert
            Assert.True(response);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void IsPrime_Returns_false_When_candidate_lower_than_2(int candidate)
        {
            // Arrange
            var legacyPersonalAssistant = new LegacyPersonalAssistant();

            // Act
            var result = legacyPersonalAssistant.IsPrime(candidate);

            // Assert
            Assert.False(result);
        }
    }
}