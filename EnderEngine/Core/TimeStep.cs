using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine.Core
{

    /// <summary>
    /// A simple struct made to keep information about The elapsed
    /// </summary>
    public readonly struct TimeStep
    {
        /// <summary>
        /// The time in seconds elapsed since the last frame, not modified by timescale
        /// </summary>
        public readonly float RealDeltaTime;
        /// <summary>
        /// The time in seconds elapsed since the last frame
        /// </summary>
        public readonly float DeltaTime;
        /// <summary>
        /// The current framerate (calculated by dividing a time unit by it's corresponding elapsed time => 1s / DeltaTime in seconds)
        /// </summary>
        public readonly float FPS;

        /// <summary>
        /// The constructor of the TimeStep
        /// </summary>
        /// <param name="deltaTime">deltaTime is the time since the last frame/Cycle, RealDeltaTime will be equal to deltaTime, and DeltaTime will be equal to deltaTime * timeScale</param>
        /// <param name="timeScale">timeScale is the speed at which your game is running (1 is normal speed), the FPS is not modified by timeScale</param>
        public TimeStep(float deltaTime, float timeScale)
        {
            RealDeltaTime = deltaTime;
            DeltaTime = deltaTime * timeScale;
            FPS = 1 / deltaTime;
        }

        /// <summary>
        /// Returns a formatted string with all the values of the current TimeStep instance
        /// </summary>
        /// <returns> The formatted string </returns>
        public override string ToString()
        {
            
            return $"RealDeltaTime: {RealDeltaTime}s, DeltaTime: {DeltaTime}s, FPS: {FPS}";

        }

    }
}
