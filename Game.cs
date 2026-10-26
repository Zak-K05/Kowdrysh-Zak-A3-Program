// Include code libraries you need below (use the namespace).
using System;
using System.Drawing;
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
        const float incrementAmount = 0.025f;
        const int inactiveSize = 100;
        const int activeGrowthAmount = 300;


        // Blocks Tracking
        bool[] activeBlocks = {false, false, false, false, false};
        float[] blocksProgress = {0f, 0f, 0f, 0f, 0f};

        //
        float timeLastSinceCheck = 0f;
        float spawnWaitTime = 0f;
        float maxSpawnInterval = 3f;
        float minSpawnInterval = 1f;
        int chosenBlockIndex = 0;
        int playerScore = 0;

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

            DrawBlocks(); // Draw blocks based on progress

            CheckClick(); // Check for player clicks

            DrawScore(); // Show score
        }


        void DrawScore()
        {
            Text.Draw($"Score: {playerScore}", 0, 0);
        }


        void CheckClick()
        {
            if (Input.IsMouseButtonPressed(MouseInput.Left))
            {
                for (int i = 0; i < activeBlocks.Length; i++)
                {
                    if (Input.GetMouseX() > 100 + (i * 150) - 50 && // Left side check
                        Input.GetMouseX() < 100 + (i * 150) - 50 + 100 && // Right side check
                        Input.GetMouseY() > 600 - (inactiveSize + (activeGrowthAmount * blocksProgress[i])) && // Top side check
                        activeBlocks[i] == true)
                    {
                        playerScore ++;
                        activeBlocks[i] = false;

                        minSpawnInterval *= 0.9f;
                        maxSpawnInterval *= 0.9f;
                    }
                }
            }
        }


        void DrawBlocks()
        {
            Draw.FillColor = Color.White;
            Draw.LineSize = 2;

            // Go thru every block and make more red as progress goes up
            for (int i = 0; i < activeBlocks.Length; i++)
            {

                Draw.FillColor = new Color(255, (int)(255 * (1f - blocksProgress[i])), (int)(255 * (1f - blocksProgress[i])));
                Draw.Rectangle(100 + (i * 150) - 50, 600 - (inactiveSize + (activeGrowthAmount * blocksProgress[i])), 
                    100, inactiveSize + (activeGrowthAmount * blocksProgress[i]) + 5);
            }
        }


        void CheckActiveBlocks()
        {
            // Check every block
            // If block is active, add to progress and cap at 1
            // If block is not active, remove from progress and cap at 0
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

            // Trigger block when its been long enough, reset timer and adjust timing
            if (timeLastSinceCheck > spawnWaitTime) 
            {
                timeLastSinceCheck = 0f;
                spawnWaitTime = Random.Float(minSpawnInterval, maxSpawnInterval);

                TriggerRandomBlock();
            }
        }

        void TriggerRandomBlock()
        {
            // Randomly activate/deactivate block
            chosenBlockIndex = Random.Integer(activeBlocks.Length);
            activeBlocks[chosenBlockIndex] = !activeBlocks[chosenBlockIndex];
        }
    }
}
