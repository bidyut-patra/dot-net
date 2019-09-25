using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using sample.Controllers;
using sample.Models;
using sample.Services;

namespace sample.unit.tests
{
    [TestClass]
    public class TrainingUnitTest
    {
        [TestMethod]
        public void CheckDateError()
        {
            var mockService = MockRepository.GenerateStub<ITrainingService>();
            var controller = new TrainingController(mockService);
            var results = controller.GetAllTrainingData().Result;
            var content = results as OkNegotiatedContentResult<List<TrainingData>>;

            Assert.IsNotNull(content);
            Assert.IsNotNull(content.Content);
            Assert.IsTrue(content.Content.Count > 0);
        }
    }
}
