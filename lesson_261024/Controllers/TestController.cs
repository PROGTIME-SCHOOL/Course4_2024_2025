using lesson_261024.Services;
using lesson_261024.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lesson_261024.Controllers
{
    public class TestController : Controller
    {
        private ITestService testService;
        private AnotherTestService anotherTestService;

        public TestController(ITestService testService, AnotherTestService anotherTestService)
        {
            this.anotherTestService = anotherTestService;
            this.testService = testService;
        }

        

        public IActionResult IncrementCounter()
        {
            testService.IncrementCounter();
            var anotherCounter = anotherTestService.IncrementCounter();

            var res = new
            {
                TestCounter = testService.Counter,
                AnotherTestCounter = anotherCounter
            };

            return Ok(res);
        }

    }
}
