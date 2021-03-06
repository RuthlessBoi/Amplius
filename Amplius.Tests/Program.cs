﻿using Amplius.Commands;
using Amplius.Logging;
using Amplius.Utils;
using System;

/// <license>
/// MIT License
/// 
/// Copyright(c) 2020 RuthlessBoi
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
/// </license>

namespace Amplius.Tests
{
#nullable enable
    /*
     * Will convert this into a proper unit test suite.
     * 
     * For now, I'll be using this as a more REPL/interactive based testing suite.
     */
    internal sealed class Program : ICommandSource
    {
        internal static readonly Logger LOGGER = new Logger(Namespace.Amplius_Tests.From("tests"), Console.Out);
        internal static void Main(string[] args) => new Program(args);

        private Program(string[] args)
        {
            while (true)
            {
                TestCommandsBleeding.Write("> ", ConsoleColor.White);
                var line = Console.ReadLine();
                TestCommandsBleeding.Dispatch(this, line);
            }
        }

        public string? GetName() => GetType().Name;
    }
}