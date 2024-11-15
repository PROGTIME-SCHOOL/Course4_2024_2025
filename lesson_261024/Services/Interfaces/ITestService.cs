using System;

namespace lesson_261024.Services.Interfaces;

public interface ITestService
{
    int Counter {get;set;}
    void IncrementCounter();
}