using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnderEngine.Core;

namespace EnderEngine
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Engine
    {

        /// <summary>
        /// This bool indicates wether the Engine has it's Run() method called
        /// </summary>
        private bool isRunning = false;

        /// <summary>
        /// TODO
        /// </summary>
        public Engine()
        {
            //TODO: manage the initialization of components, scripts, etc...
        }

        /// <summary>
        /// Main entry of the program, runs a loop that calls the 'Cycle' method each iteration. Stop can be requested by pressing `escape` in the console.
        /// </summary>
        public void Run()
        {
            Awake();
            isRunning = true;
            while (!IsStopRequested())
            {
                EngineCycle();
            }
            Shutdown();
        }


        /// <summary>
        /// This method is called each loop iteration, handles update and rendering logic every frame.
        /// </summary>
        public void Cycle()
        {
            if (isRunning)
            {
                Console.WriteLine("You can't run the Cycle yourself if the Engine is already managing it"); //Change to log when logging system implemented
                return;
            }
            EngineCycle();
        }

        /// <summary>
        /// this method is called each loop iteration, handles update and rendering logic every frame.
        /// </summary>
        private void EngineCycle()
        {
            Console.WriteLine("Update"); // Test line -> to remove when done
            //Handle game logic, rendering, updates, etc...
        }

        private void Awake()
        {
            //TODO: design, private, internal or public
            //Awake/Init game objects, running logic, start running stuff, etc...
        }

        private void Shutdown()
        {
            //TODO: design, private, internal or public
            //if public or internal : check if engine is already dead, if not kill it
            //Handle logic for shutdown : Killing every process, autosave, etc...
        }

        /// <summary>
        /// This method is here to wait while we have a proper Event and Input system
        /// </summary>
        /// <returns>Returns 'true' if the user presses `escape`</returns>
        private bool IsStopRequested()
        {
            if (Console.KeyAvailable)
            {
                if (Console.ReadKey().Key == ConsoleKey.X)
                {
                    Console.WriteLine();
                    return true;
                }
            }
            return false;
        }

    }
}
