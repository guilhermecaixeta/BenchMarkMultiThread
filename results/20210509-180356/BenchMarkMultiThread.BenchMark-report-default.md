
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.928 (2004/?/20H1)
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.202
  [Host]     : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT  [AttachedDebugger]
  Job-OPYBUF : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT

EvaluateOverhead=True  Runtime=.NET Core 5.0  InvocationCount=1  
RunStrategy=Monitoring  UnrollFactor=1  

                                                        Method | iterations |       Mean |       Error |      StdDev |     Median |     Gen 0 |     Gen 1 |     Gen 2 | Allocated | Completed Work Items | Lock Contentions |
-------------------------------------------------------------- |----------- |-----------:|------------:|------------:|-----------:|----------:|----------:|----------:|----------:|---------------------:|-----------------:|
     **WriteFileStream_AsParallel_AutoBuffered_ImplicityDisposed** |       **1000** |   **506.4 ms** |   **120.22 ms** |    **79.52 ms** |   **491.4 ms** |         **-** |         **-** |         **-** |   **1.46 MB** |            **2005.0000** |                **-** |
      WriteFileStream_AsParallel_AutoBuffered_ExplicitDisposed |       1000 |   555.7 ms |   242.72 ms |   160.54 ms |   560.8 ms |         - |         - |         - |   1.32 MB |            2005.0000 |                - |
 WriteBufferedStream_AsParallel_AutoBuffered_ImplicityDisposed |       1000 |   463.6 ms |    50.84 ms |    33.63 ms |   458.1 ms |         - |         - |         - |   1.79 MB |            2005.0000 |                - |
  WriteBufferedStream_AsParallel_AutoBuffered_ExplicitDisposed |       1000 |   531.1 ms |   185.08 ms |   122.42 ms |   491.3 ms |         - |         - |         - |   1.64 MB |            2005.0000 |                - |
             WriteFileStream_ParallelForEach_ImplicityDisposed |       1000 |   550.5 ms |   218.83 ms |   144.74 ms |   512.5 ms |         - |         - |         - |   1.82 MB |            1693.0000 |           1.0000 |
             WriteFileStream_ParallelForEach_ExplicityDisposed |       1000 |   880.6 ms |   979.53 ms |   647.90 ms |   513.1 ms |         - |         - |         - |   1.66 MB |            1252.0000 |                - |
                                     WriteBufferedStream_Async |       1000 | 1,113.0 ms |   859.19 ms |   568.30 ms |   841.4 ms |         - |         - |         - |   1.55 MB |            2001.0000 |                - |
                                         WriteFileStream_Async |       1000 |   842.6 ms |   157.34 ms |   104.07 ms |   774.6 ms |         - |         - |         - |   1.23 MB |            1002.0000 |                - |
     **WriteFileStream_AsParallel_AutoBuffered_ImplicityDisposed** |       **3000** | **1,517.3 ms** |   **319.89 ms** |   **211.59 ms** | **1,484.4 ms** | **1000.0000** |         **-** |         **-** |   **4.36 MB** |            **6004.0000** |                **-** |
      WriteFileStream_AsParallel_AutoBuffered_ExplicitDisposed |       3000 | 1,440.8 ms |   121.04 ms |    80.06 ms | 1,414.7 ms | 1000.0000 |         - |         - |   3.95 MB |            6006.0000 |                - |
 WriteBufferedStream_AsParallel_AutoBuffered_ImplicityDisposed |       3000 | 2,391.7 ms | 1,609.67 ms | 1,064.70 ms | 2,038.0 ms | 1000.0000 |         - |         - |   5.36 MB |            6005.0000 |                - |
  WriteBufferedStream_AsParallel_AutoBuffered_ExplicitDisposed |       3000 | 1,930.7 ms | 1,130.67 ms |   747.87 ms | 1,616.1 ms | 1000.0000 |         - |         - |   4.91 MB |            6005.0000 |                - |
             WriteFileStream_ParallelForEach_ImplicityDisposed |       3000 | 1,447.2 ms |   212.76 ms |   140.73 ms | 1,452.3 ms | 1000.0000 |         - |         - |    5.4 MB |            6120.0000 |                - |
             WriteFileStream_ParallelForEach_ExplicityDisposed |       3000 | 1,753.8 ms | 1,113.91 ms |   736.78 ms | 1,488.6 ms |         - |         - |         - |   4.95 MB |            6046.0000 |                - |
                                     WriteBufferedStream_Async |       3000 | 3,289.5 ms |   808.14 ms |   534.54 ms | 3,241.5 ms | 1000.0000 |         - |         - |   4.68 MB |            6002.0000 |                - |
                                         WriteFileStream_Async |       3000 | 2,395.6 ms |   160.13 ms |   105.92 ms | 2,377.5 ms | 1000.0000 |         - |         - |   3.72 MB |            3002.0000 |                - |
     **WriteFileStream_AsParallel_AutoBuffered_ImplicityDisposed** |       **5000** | **2,751.5 ms** |   **946.64 ms** |   **626.14 ms** | **2,586.3 ms** | **2000.0000** | **1000.0000** |         **-** |   **7.26 MB** |           **10004.0000** |                **-** |
      WriteFileStream_AsParallel_AutoBuffered_ExplicitDisposed |       5000 | 3,364.4 ms | 1,836.74 ms | 1,214.89 ms | 2,885.8 ms | 2000.0000 | 1000.0000 |         - |   6.57 MB |           10005.0000 |                - |
 WriteBufferedStream_AsParallel_AutoBuffered_ImplicityDisposed |       5000 | 2,641.8 ms |   504.21 ms |   333.51 ms | 2,634.5 ms | 2000.0000 | 1000.0000 |         - |    8.9 MB |           10005.0000 |                - |
  WriteBufferedStream_AsParallel_AutoBuffered_ExplicitDisposed |       5000 | 3,100.8 ms | 2,254.63 ms | 1,491.30 ms | 2,418.2 ms | 3000.0000 | 1000.0000 |         - |   8.18 MB |           10005.0000 |                - |
             WriteFileStream_ParallelForEach_ImplicityDisposed |       5000 | 3,078.0 ms | 1,339.80 ms |   886.20 ms | 2,893.2 ms | 2000.0000 |         - |         - |   8.96 MB |           10163.0000 |                - |
             WriteFileStream_ParallelForEach_ExplicityDisposed |       5000 | 2,650.6 ms |   356.02 ms |   235.49 ms | 2,567.8 ms | 2000.0000 |         - |         - |   8.23 MB |           10230.0000 |                - |
                                     WriteBufferedStream_Async |       5000 | 5,217.8 ms | 1,525.05 ms | 1,008.73 ms | 5,180.7 ms | 3000.0000 | 1000.0000 | 1000.0000 |   7.84 MB |           10001.0000 |                - |
                                         WriteFileStream_Async |       5000 | 4,046.8 ms |   496.07 ms |   328.12 ms | 3,951.4 ms | 3000.0000 | 1000.0000 |         - |   6.24 MB |            5002.0000 |                - |
