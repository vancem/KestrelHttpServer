// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using BenchmarkDotNet.Running;

namespace Microsoft.AspNetCore.Server.Kestrel.Performance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new ResponseHeadersWritingBenchmark();
            p.Type = ResponseHeadersWritingBenchmark.BenchmarkTypes.TechEmpowerPlaintext;
            p.Setup();
            Console.WriteLine("Setup done");
            Console.ReadKey();
            for (int i = 0; i < 10000000; i++)
            {
                p.Output().GetAwaiter().GetResult();
            }

            // BenchmarkSwitcher.FromAssembly(typeof(Program).GetTypeInfo().Assembly).Run(args);
        }
    }
}
