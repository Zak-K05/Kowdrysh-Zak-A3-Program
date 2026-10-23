// Include code libraries you need below (use the namespace).
using System;
using System.Numerics;

// The namespace your code is in.
namespace Game10003
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        const float maxSpawnInterval = 5f;
        const float minSpawnInterval = 1f;

        float timeLastSinceCheck = 0f;
        float spawnWaitTime = 0f;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.TargetFPS = 60;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            CheckRectTrigger();
        }


        public void CheckRectTrigger()
        {
            timeLastSinceCheck += Time.DeltaTime;

            if (timeLastSinceCheck > spawnWaitTime) 
            {
                timeLastSinceCheck = 0f;
                spawnWaitTime = Random.Float(minSpawnInterval, maxSpawnInterval);

                //Put trigger rect func here
                Console.WriteLine("Trigger rect");
            }
        }
    }
}
