using System;
using System.Collections.Generic;
using GameModule.Core.Data;

namespace GameModule.Core.Events
{
    public class GameStartEventData
    {
        public GameMode GameMode { get; set; }
        public int PlayerCount { get; set; }
    }

    public class GameEndEventData
    {
        public Guid WinnerId { get; set; }
        public float GameTime { get; set; }
        public Dictionary<Guid, int> FinalScores { get; set; }
    }

    public class PlayerDamageEventData
    {
        public Guid PlayerId { get; set; }
        public float Damage { get; set; }
        public DamageType DamageType { get; set; }
    }

    public class PlayerStateChangeEventData
    {
        public Guid PlayerId { get; set; }
        public PlayerState NewState { get; set; }
    }

    public enum DamageType
    {
        Normal,
        OutOfBounds,
        Fall
    }
} 