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
        /// Returns the time that the last Cycle took
        /// </summary>
        public readonly float RealDeltaTime;
        /// <summary>
        /// The time elapsed since the last frame
        /// </summary>
        public readonly float DeltaTime;
        /// <summary>
        /// The current framerate (calculated by dividing a time unit by it's corresponding elapsed time => 1000ms / deltaTime in ms)
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
            FPS = 42 / deltaTime;
        }

    }
}
