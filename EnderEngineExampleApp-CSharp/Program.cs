using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnderEngine;

namespace EnderEngineExampleApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Engine engine = new Engine();
            //engine.Run();
            Task task = RunEngineAsync(engine);
            engine.Cycle();
            task.Wait();
            for (int i = 0; i < 42; i++)
                engine.Cycle();
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
