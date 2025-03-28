using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace TimeAngle.Services.Tests
{
    public class TimeAngleServiceTests
    {
        private readonly TimeAngleService _service;
        private readonly Mock<ILogger<TimeAngleService>> _loggerMock;

        public TimeAngleServiceTests()
        {
            _loggerMock = new Mock<ILogger<TimeAngleService>>();
            _service = new TimeAngleService(_loggerMock.Object);
        }

        [Theory]
        [InlineData(3, 15, 187.5)]
        [InlineData(23, 59, 713.5)]
        [InlineData(12, 1, 6.5)]
        public async Task CalculateTimeAngle_HourMinute_ValidInput(int hour, int minute, double expected)
        {
            var result = await _service.CalculateTimeAngle(hour, minute);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(24)]
        public async Task CalculateTimeAngle_HourMinute_HourBadRequest(int hour)
        {
            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                _service.CalculateTimeAngle(hour, 30));
            Assert.Contains("Hour must be between 0 and 23", ex.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(60)]
        public async Task CalculateTimeAngle_HourMinute_MinuteBadRequest(int minute)
        {
            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                _service.CalculateTimeAngle(6, minute));
            Assert.Contains("Minute must be between 0 and 59", ex.Message);
        }

        [Fact]
        public async Task CalculateTimeAngle_StringInput_ParsesCorrectly()
        {
            var result = await _service.CalculateTimeAngle("03:15");
            Assert.Equal(187.5, result);
        }

        [Fact]
        public async Task CalculateTimeAngle_InvalidString_ThrowsArgumentException()
        {
            // Arrange
            var invalidInput = "not-a-time";

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.CalculateTimeAngle(invalidInput));

            Assert.Equal("Invalid time format. Expected format: HH:mm", exception.Message);
        }
    }
}