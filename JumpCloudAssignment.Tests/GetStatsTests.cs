using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JumpCloudAssignment.Services;
using JumpCloudAssignment.Models;
using System.Threading.Tasks;

namespace JumpCloudAssignment.Tests
{
    [TestClass]
    public class GetStatsTests
    {
        [TestMethod]
        public void Get_StatsNoActions_ReturnsEmptyResult()
        {
            var service = new ActionService();

            var stats = service.GetStats();

            Assert.IsTrue(string.IsNullOrEmpty(stats));
        }

        public void Get_StatsSingleAction_ReturnsSameNumber()
        {
            var service = new ActionService();

            var test1 = service.AddAction("{\"action\":\"jump\",\"time\":100}");
            var stats = service.GetStats();

            var expectedResults = "[{\"action\":\"jump\",\"avg\":100.0}]";

            Assert.IsTrue(string.IsNullOrEmpty(test1));
            Assert.IsFalse(string.IsNullOrEmpty(stats));
            Assert.AreEqual(expectedResults, stats);
        }

        [TestMethod]
        public void Get_StatsMultipleActions_ReturnsExpectedAverages()
        {
            var service = new ActionService();

            var test1 = service.AddAction("{\"action\":\"jump\", \"time\":100}");
            var test2 = service.AddAction("{\"action\":\"run\", \"time\":75}");
            var test3 = service.AddAction("{\"action\":\"jump\", \"time\":200}");
            var stats = service.GetStats();

            var expectedResults = "[{\"action\":\"jump\",\"avg\":150.0},{\"action\":\"run\",\"avg\":75.0}]";

            Assert.IsTrue(string.IsNullOrEmpty(test1));
            Assert.IsTrue(string.IsNullOrEmpty(test2));
            Assert.IsTrue(string.IsNullOrEmpty(test3));
            Assert.IsFalse(string.IsNullOrEmpty(stats));
            Assert.AreEqual(expectedResults, stats);
        }

    }
}
