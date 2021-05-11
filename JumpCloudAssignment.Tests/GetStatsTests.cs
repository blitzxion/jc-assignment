using System;
using JumpCloudAssignment.Services;
using JumpCloudAssignment.Models;
using System.Threading.Tasks;
using Xunit;

namespace JumpCloudAssignment.Tests
{
    public class GetStatsTests
    {
        [Fact]
        public void Get_StatsNoActions_ReturnsEmptyResult()
        {
            var service = new ActionService();

            var stats = service.GetStats();

            Assert.True(string.IsNullOrEmpty(stats));
        }

        [Fact]
        public void Get_StatsSingleAction_ReturnsSameNumber()
        {
            var service = new ActionService();

            var test1 = service.AddAction("{\"action\":\"jump\",\"time\":100}");
            var stats = service.GetStats();

            var expectedResults = "[{\"action\":\"jump\",\"avg\":100.0}]";

            Assert.True(string.IsNullOrEmpty(test1));
            Assert.False(string.IsNullOrEmpty(stats));
            Assert.Equal(expectedResults, stats);
        }

        [Fact]
        public void Get_StatsMultipleActions_ReturnsExpectedAverages()
        {
            var service = new ActionService();

            var test1 = service.AddAction("{\"action\":\"jump\", \"time\":100}");
            var test2 = service.AddAction("{\"action\":\"run\", \"time\":75}");
            var test3 = service.AddAction("{\"action\":\"jump\", \"time\":200}");
            var stats = service.GetStats();

            var expectedResults = "[{\"action\":\"jump\",\"avg\":150.0},{\"action\":\"run\",\"avg\":75.0}]";

            Assert.True(string.IsNullOrEmpty(test1));
            Assert.True(string.IsNullOrEmpty(test2));
            Assert.True(string.IsNullOrEmpty(test3));
            Assert.False(string.IsNullOrEmpty(stats));
            Assert.Equal(expectedResults, stats);
        }

    }
}
