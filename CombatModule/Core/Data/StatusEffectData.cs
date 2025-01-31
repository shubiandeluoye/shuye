namespace CombatModule.Core.Data
{
    public class StatusEffectData
    {
        public StatusEffectType Type { get; set; }
        public float Value { get; set; }
        public float Duration { get; set; }
        public int MaxStacks { get; set; }
    }

    public enum StatusEffectType
    {
        Stun,
        Slow,
        Dot
    }
} 