using System;
using System.IO;

namespace Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Console.Write(Directory.GetCurrentDirectory());
    }
}