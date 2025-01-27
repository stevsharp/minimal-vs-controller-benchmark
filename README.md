Minimal APIs vs Controllers Benchmark
This project benchmarks the performance of Minimal APIs and Controllers in an ASP.NET Core application. It compares the two approaches for lightweight and data-heavy operations, 
helping developers evaluate the trade-offs between Minimal APIs and traditional Controllers.

Features
Benchmarking Framework: Uses BenchmarkDotNet for precise and reliable performance measurements.
Scenarios Benchmarked:
Lightweight GET endpoints (/hello).
Data-heavy POST endpoints (/data).
Memory Usage Insights: Measures memory allocation for each operation.
Comparison of Execution Time: Detailed analysis of response times for Minimal APIs vs Controllers.
Iterative Testing: Benchmarks run with multiple iterations for consistent results.
