

using BenchmarkDotNet.Running;

try
{
    BenchmarkRunner.Run<ApiBenchmark>();

    //ApiBenchmark summary = new ApiBenchmark();

    ///var minimalApiHello = summary.Controller_PostData();

}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

Console.WriteLine("Press any key to exit...");

Console.ReadKey();