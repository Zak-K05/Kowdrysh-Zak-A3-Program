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
        // Blocks Constants
        const float maxSpawnInterval = 3f;
        const float minSpawnInterval = 1f;
        const float incrementAmount = 0.05f;

        // Blocks Tracking
        bool[] activeBlocks = {false, false, false, false, false, false};
        float[] blocksProgress = {0f, 0f, 0f, 0f, 0f, 0f};

        //
        float timeLastSinceCheck = 0f;
        float spawnWaitTime = 0f;
        int chosenBlockIndex = 0;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.TargetFPS = 60;

            spawnWaitTime = Random.Float(minSpawnInterval, maxSpawnInterval);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            CheckRectTrigger(); // Check if its time to trigger blocks

            CheckActiveBlocks(); // Handle active and inactive blocks
        }


        void CheckActiveBlocks()
        {
            for (int i = 0; i < activeBlocks.Length; i++)
            {
                if (activeBlocks[i] == true)
                {
                    blocksProgress[i] += incrementAmount;
                    if (blocksProgress[i] > 1f)
                    {
                        blocksProgress[i] = 1f;
                    } 

                } 
                else
                {
                    blocksProgress[i] -= incrementAmount;
                    if (blocksProgress[i] < 0f)
                    {
                        blocksProgress[i] = 0f;
                    }
                }
            }
        }


        void CheckRectTrigger()
        {
            // Add time to last check
            timeLastSinceCheck += Time.DeltaTime;

            // Trigger block when its been long enough and reset
            if (timeLastSinceCheck > spawnWaitTime) 
            {
                timeLastSinceCheck = 0f;
                spawnWaitTime = Random.Float(minSpawnInterval, maxSpawnInterval);

                TriggerRandomBlock();
            }
        }

        void TriggerRandomBlock()
        {
            chosenBlockIndex = Random.Integer(activeBlocks.Length);
            if (activeBlocks[chosenBlockIndex] == false)
            {
                activeBlocks[chosenBlockIndex] = true;
            }
        }
    }
}
