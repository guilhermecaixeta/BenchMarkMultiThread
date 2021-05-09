``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.928 (2004/?/20H1)
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.202
  [Host]     : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT  [AttachedDebugger]
  Job-BBMNON : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT

EvaluateOverhead=True  Runtime=.NET Core 5.0  InvocationCount=1  
RunStrategy=Monitoring  UnrollFactor=1  

```
|                                                        Method | iterations |       Mean |       Error |      StdDev |     Median |     Gen 0 |     Gen 1 | Gen 2 | Allocated | Completed Work Items | Lock Contentions |
|-------------------------------------------------------------- |----------- |-----------:|------------:|------------:|-----------:|----------:|----------:|------:|----------:|---------------------:|-----------------:|
|     **WriteFileStream_AsParallel_AutoBuffered_ImplicityDisposed** |       **1000** |   **513.1 ms** |   **162.43 ms** |   **107.44 ms** |   **486.6 ms** |         **-** |         **-** |     **-** |   **1.46 MB** |            **2005.0000** |                **-** |
|      WriteFileStream_AsParallel_AutoBuffered_ExplicitDisposed |       1000 |   741.3 ms |   596.99 ms |   394.88 ms |   575.1 ms |         - |         - |     - |   1.32 MB |            2006.0000 |                - |
| WriteBufferedStream_AsParallel_AutoBuffered_ImplicityDisposed |       1000 |   908.4 ms |   990.08 ms |   654.88 ms |   559.5 ms |         - |         - |     - |   1.79 MB |            2005.0000 |                - |
|  WriteBufferedStream_AsParallel_AutoBuffered_ExplicitDisposed |       1000 |   551.8 ms |   141.20 ms |    93.40 ms |   551.9 ms |         - |         - |     - |   1.64 MB |            2005.0000 |                - |
|             WriteFileStream_ParallelForEach_ImplicityDisposed |       1000 |   528.6 ms |   140.33 ms |    92.82 ms |   497.5 ms |         - |         - |     - |    1.8 MB |            2015.0000 |                - |
|             WriteFileStream_ParallelForEach_ExplicityDisposed |       1000 |   552.6 ms |   256.40 ms |   169.60 ms |   456.8 ms |         - |         - |     - |   1.65 MB |            2011.0000 |                - |
|                                     WriteBufferedStream_Async |       1000 |   802.7 ms |    75.41 ms |    49.88 ms |   781.6 ms |         - |         - |     - |   1.55 MB |            2002.0000 |                - |
|                                         WriteFileStream_Async |       1000 | 1,108.3 ms |   473.62 ms |   313.27 ms | 1,072.6 ms |         - |         - |     - |   1.23 MB |            1002.0000 |                - |
|     **WriteFileStream_AsParallel_AutoBuffered_ImplicityDisposed** |       **3000** | **2,109.0 ms** | **1,398.84 ms** |   **925.24 ms** | **1,804.6 ms** | **1000.0000** |         **-** |     **-** |   **4.36 MB** |            **6003.0000** |                **-** |
|      WriteFileStream_AsParallel_AutoBuffered_ExplicitDisposed |       3000 | 1,607.6 ms |   670.31 ms |   443.37 ms | 1,435.5 ms | 1000.0000 |         - |     - |   3.95 MB |            6003.0000 |                - |
| WriteBufferedStream_AsParallel_AutoBuffered_ImplicityDisposed |       3000 | 1,507.1 ms |   172.92 ms |   114.37 ms | 1,530.6 ms | 1000.0000 |         - |     - |   5.34 MB |            6003.0000 |                - |
|  WriteBufferedStream_AsParallel_AutoBuffered_ExplicitDisposed |       3000 | 1,974.8 ms | 1,530.65 ms | 1,012.43 ms | 1,617.9 ms | 1000.0000 |         - |     - |   4.91 MB |            6005.0000 |                - |
|             WriteFileStream_ParallelForEach_ImplicityDisposed |       3000 | 2,472.2 ms | 1,619.09 ms | 1,070.93 ms | 2,396.8 ms | 1000.0000 |         - |     - |   5.36 MB |            6039.0000 |                - |
|             WriteFileStream_ParallelForEach_ExplicityDisposed |       3000 | 1,433.0 ms |   269.15 ms |   178.03 ms | 1,371.7 ms |         - |         - |     - |   4.92 MB |            6026.0000 |                - |
|                                     WriteBufferedStream_Async |       3000 | 3,438.1 ms | 1,168.06 ms |   772.60 ms | 3,335.5 ms | 1000.0000 |         - |     - |   4.68 MB |            6000.0000 |                - |
|                                         WriteFileStream_Async |       3000 | 2,442.8 ms |   283.64 ms |   187.61 ms | 2,361.3 ms | 1000.0000 |         - |     - |   3.72 MB |            3002.0000 |                - |
|     **WriteFileStream_AsParallel_AutoBuffered_ImplicityDisposed** |       **5000** | **2,541.5 ms** |   **298.49 ms** |   **197.43 ms** | **2,503.2 ms** | **2000.0000** | **1000.0000** |     **-** |   **7.26 MB** |           **10002.0000** |                **-** |
|      WriteFileStream_AsParallel_AutoBuffered_ExplicitDisposed |       5000 | 3,810.3 ms | 2,292.28 ms | 1,516.20 ms | 3,260.5 ms | 2000.0000 | 1000.0000 |     - |   6.57 MB |           10005.0000 |                - |
| WriteBufferedStream_AsParallel_AutoBuffered_ImplicityDisposed |       5000 | 2,628.6 ms |   525.38 ms |   347.51 ms | 2,602.9 ms | 2000.0000 | 1000.0000 |     - |    8.9 MB |           10005.0000 |                - |
|  WriteBufferedStream_AsParallel_AutoBuffered_ExplicitDisposed |       5000 | 2,822.1 ms |   830.89 ms |   549.58 ms | 2,784.8 ms | 3000.0000 | 1000.0000 |     - |   8.17 MB |           10000.0000 |                - |
|             WriteFileStream_ParallelForEach_ImplicityDisposed |       5000 | 3,466.3 ms | 1,635.19 ms | 1,081.58 ms | 3,583.9 ms | 2000.0000 |         - |     - |   8.96 MB |           10161.0000 |                - |
|             WriteFileStream_ParallelForEach_ExplicityDisposed |       5000 | 3,415.3 ms | 4,270.67 ms | 2,824.78 ms | 2,496.2 ms | 3000.0000 |         - |     - |   8.27 MB |           10401.0000 |           1.0000 |
|                                     WriteBufferedStream_Async |       5000 | 5,411.0 ms | 5,817.38 ms | 3,847.84 ms | 4,109.5 ms | 1000.0000 |         - |     - |   7.84 MB |           10002.0000 |                - |
|                                         WriteFileStream_Async |       5000 | 4,375.0 ms | 1,190.76 ms |   787.61 ms | 4,122.3 ms | 3000.0000 | 1000.0000 |     - |   6.24 MB |            5005.0000 |                - |
