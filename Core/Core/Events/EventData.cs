using Core.Types;

namespace Core.Events
{
    public struct GameStateChangedData
    {
        public GameState NewState { get; set; }
    }

    public struct PlayerDamagedData
    {
        public string PlayerId { get; set; }
        public float Damage { get; set; }
        public float RemainingHealth { get; set; }
    }

    public struct GameEndData
    {
        public string WinnerId { get; set; }
        public string LoserId { get; set; }
        public string EndReason { get; set; }
    }

    public struct PlayerShootData
    {
        public string PlayerId { get; set; }
        public float Angle { get; set; }
    }

    public struct CentralAreaStateData
    {
        public CentralAreaState OldState { get; set; }
        public CentralAreaState NewState { get; set; }
        public float StateTime { get; set; }
        public int CollectedCount { get; set; }
    }

    public struct ShapeStateData
    {
        public int ShapeId { get; set; }
        public Vector3Data Position { get; set; }
        public QuaternionData Rotation { get; set; }
        public Vector3Data Scale { get; set; }
    }
} 