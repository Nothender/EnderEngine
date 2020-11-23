using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnderEngine;
using EnderEngine.Core;
using Pastel;

namespace EnderEngineExampleApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Engine.Init();
          
            Engine engine = new Engine();

            engine.logger.Log("42", Logger.LogLevel.DEBUG);
            engine.logger.Log("42", Logger.LogLevel.ERROR);
            engine.logger.Log("42", Logger.LogLevel.INFO);
            engine.logger.Log("42", Logger.LogLevel.WARN);
            engine.logger.Log("42", Logger.LogLevel.FATAL);
            engine.logger.Log("42", Logger.LogLevel.DEBUG);

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
