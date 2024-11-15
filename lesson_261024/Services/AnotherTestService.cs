using System;
using lesson_261024.Services.Interfaces;

namespace lesson_261024.Services;

public class AnotherTestService
{
    private ITestService testService;
    public AnotherTestService(ITestService testService)
    {
        this.testService = testService;
    }

    public int IncrementCounter()
    {
        testService.IncrementCounter();
        return testService.Counter;
    }
}
