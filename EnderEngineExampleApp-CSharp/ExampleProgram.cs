using System;
using System.Collections.Generic;
using System.Text;
using EnderEngine;
using EnderEngine.Core;
using EnderEngine.Logging;

namespace EnderEngineExampleApp
{
    public static class ExampleProgram
    {

        public static Engine engine; //The main instance of the engine

        private static void Main(string[] args)
        {
            Engine.Init(); //Initializes the assembly once (can only be initialized once), to use anything from the assembly this method MUST be called

            engine = new Engine();

            int x = 0;
            engine.logger.Log("banana", (LogLevel)x++);
            engine.logger.Log("banana", (LogLevel)x++);
            engine.logger.Log("banana", (LogLevel)x++);
            engine.logger.Log("banana", (LogLevel)x++);
            engine.logger.Log("banana", (LogLevel)x++);
            engine.logger.Log("banana", (LogLevel)x++);

            //engine.Run();
        }

    }
}
