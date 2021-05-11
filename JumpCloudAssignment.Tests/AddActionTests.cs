using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JumpCloudAssignment.Services;
using JumpCloudAssignment.Models;
using System.Threading.Tasks;

namespace JumpCloudAssignment.Tests
{
    [TestClass]
    public class AddActionTests
    {
        [TestMethod]
        public void Add_SingleAction_ReturnsNoError()
        {
            var service = new ActionService();

            var test1 = service.AddAction("{\"action\":\"jump\",\"time\":100}");
            
            Assert.IsTrue(string.IsNullOrEmpty(test1));
        }

        [TestMethod]
        public void Add_MultipleActions_ReturnsExpectedAverages()
        {
            var service = new ActionService();

            var test1 = service.AddAction("{\"action\":\"jump\", \"time\":100}");
            var test2 = service.AddAction("{\"action\":\"run\", \"time\":75}");
            var test3 = service.AddAction("{\"action\":\"jump\", \"time\":200}");

            Assert.IsTrue(string.IsNullOrEmpty(test1));
            Assert.IsTrue(string.IsNullOrEmpty(test2));
            Assert.IsTrue(string.IsNullOrEmpty(test3));
        }

        [TestMethod]
        public void Add_InvalidAction_ReturnsErrorMessage()
        {
            var service = new ActionService();

            var test1 = service.AddAction("{\"action\":\"step\",\"time\":100}");

            Assert.IsFalse(string.IsNullOrEmpty(test1));
        }

        [TestMethod]
        public void Add_InvalidPropertyName_ReturnsErrorMessages()
        {
            var service = new ActionService();

            var test1 = service.AddAction("{\"invalid_property\":\"jump\",\"time\":100}");

            Assert.IsFalse(string.IsNullOrEmpty(test1));
        }

        [TestMethod]
        public void Add_MissingPropertyAction_ReturnsErrorMessages()
        {
            var service = new ActionService();

            var test1 = service.AddAction("{\"action\":\"jump\"}");
            var test2 = service.AddAction("{\"time\":100}");

            Assert.IsFalse(string.IsNullOrEmpty(test1));
            Assert.IsFalse(string.IsNullOrEmpty(test2));
        }

        [TestMethod]
        public void Add_BadJson_ReturnsErrorMessage()
        {
            var service = new ActionService();

            var test1 = service.AddAction("InvalidJson");

            Assert.IsFalse(string.IsNullOrEmpty(test1));
        }

        [TestMethod]
        public void Add_AsyncMultipleActions_Succeeds()
        {
            var service = new ActionService();

            Parallel.For(0, 10, (time) => {
                var actionName = time < 5 ? "jump" : "run";
                service.AddAction($"{{\"action\":\"{actionName}\", \"time\":{time}}}");
            });

            var stats = service.GetStats();

            var expectedResults = "[{\"action\":\"jump\",\"avg\":2.0},{\"action\":\"run\",\"avg\":7.0}]";

            Assert.AreEqual(expectedResults, stats);
        }

        [TestMethod]
        public async Task AddGet_AsyncMultipleActions_Succeeds()
        {

            var service = new ActionService();

            var addJumpTask = Task.Run(() => {
                var addResults = "";
                for (int i = 0; i < 10; i++)
                    addResults += service.AddAction($"{{\"action\":\"jump\", \"time\":50}}");
                return addResults;
            });

            var addRunTask = Task.Run(() => {
                var addResults = "";
                for (int i = 0; i < 10; i++)
                    addResults += service.AddAction($"{{\"action\":\"run\", \"time\":10}}");
                return addResults;
            });

            var getTask = Task.Run(async () => {
                var getResults = "";
                for (int i = 0; i < 10; i++)
                {
                    getResults += service.GetStats();
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                return getResults;
            });

            var allResults = await Task.WhenAll(
                addJumpTask, 
                addRunTask, 
                getTask
            );

            Assert.IsTrue(string.IsNullOrEmpty(allResults[0])); // Add Jump
            Assert.IsTrue(string.IsNullOrEmpty(allResults[1])); // Add Run
            Assert.IsFalse(string.IsNullOrEmpty(allResults[2])); // Get Stats
        }

    }
}
