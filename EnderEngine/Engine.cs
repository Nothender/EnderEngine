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

        #region StaticGlobalEngineCode
        private static bool assemblyInitialized = false;
        internal static Logger engineLogger = new Logger("EnderEngineAssembly");
        
        /// <summary>
        /// Initializes the whole assembly, call once at the start of your program
        /// </summary>
        public static void Init()
        {
            if (assemblyInitialized) // Checks and ensures the Init is run only once
            {
                engineLogger.Log("Cannot initialize the assembly more than 1 time, skipping initialization", Logger.LogLevel.WARN);
                return;
            }

            Time.Init();

            assemblyInitialized = true;
            
            engineLogger.Log("Log files can be found in $\"{ExecutionDir}/Logs/\"", Logger.LogLevel.INFO, Logger.LogMethod.TO_CONSOLE);
        }
        #endregion StaticCode

        #region Time
        /// <summary>
        /// The TimeStep given at the beginning of the update
        /// </summary>
        public TimeStep currentTimeStep;
        /// <summary>
        /// The TimeScale is a value, that multiplies RealDeltaTime to get RealDeltaTime in the TimeStep (useful if you wanna slow down or accelerate the time in your program)
        /// </summary>
        public float timeScale = 0.5f;

        private float deltaTime;
        private double oldTime = 0;
        private double newTime = 0;
        #endregion Time

        /// <summary>
        /// This bool indicates wether the Engine has it's Run() method called
        /// </summary>
        private bool isRunning = false;

        public readonly Logger logger;
        public readonly int Id;

        /// <summary>
        /// Constructor of the engine
        /// </summary>
        /// <param name="id">
        /// The Id is to help differenciate the engines if you have multiple instances.
        /// If the Id is set -1, the engine will set it to 0 and will act as if it is the only instance (you can still have multiple engine instances without declaring IDs).
        /// If the value is negative it will be changed to its absolute value
        /// </param>
        public Engine(int id = -1)
        {
            //Constructor values
            Id = Math.Abs(id);
            //Construction logic
            if (id == -1)
            {
                Id = 0;
                logger = new Logger("EnderEngine");
            }
            else
                logger = new Logger("EnderEngine-" + Id);
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
                logger.Log("You can't run the Cycle yourself if the Engine is already managing it", Logger.LogLevel.WARN); //Change to log when logging system implemented
            else
                EngineCycle();
        }

        /// <summary>
        /// This method is called each loop iteration, handles update and rendering logic every frame.
        /// </summary>
        private void EngineCycle()
        {
            //Time logic
            newTime = Time.SecondsElapsedSinceStart;
            deltaTime = (float) (newTime - oldTime);
            oldTime = newTime;
            currentTimeStep = new TimeStep(deltaTime, timeScale);

            logger.Log($"Update - {currentTimeStep}", Logger.LogLevel.DEBUG); // Test line -> to remove when done
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