﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnderEngine;
using EnderEngine.Core;

namespace EnderEngineExampleApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Engine.Init();
          
            Engine engine = new Engine();
          
            engine.Run();
        }

        public static async Task RunEngineAsync(Engine engine)
        {
            Console.WriteLine("Running engine");

            await Task.Run(() => {
                engine.Run();
            });

            Console.WriteLine("Finished running engine");
        }

    }
}
