using System;
using lesson_261024.Services.Interfaces;

namespace lesson_261024.Services;

public class TestService : ITestService
{
    public int Counter { get; set; } = 0;

    public void IncrementCounter()
    {
        Counter++;
    }
}
