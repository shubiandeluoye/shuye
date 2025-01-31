using System;

namespace GameModule.Core.Data
{
    public class PlayerData
    {
        public float health = 100f;
        public int score = 0;
        public bool isAlive = true;
        public PlayerState state = PlayerState.None;
        public Vector2Data position;
    }

    public struct Vector2Data
    {
        public float X;
        public float Y;

        public Vector2Data(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
} 