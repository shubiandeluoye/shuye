namespace CombatModule.Core.Data
{
    public class CombatConfig
    {
        public DamageConfig DamageConfig { get; set; }
        public StatusEffectConfig StatusEffectConfig { get; set; }
    }

    public class DamageConfig
    {
        public float CritDamageMultiplier { get; set; } = 2f;
        public float SkillDamageMultiplier { get; set; } = 1.5f;
        public float StatusDamageMultiplier { get; set; } = 0.8f;
        public float DamageVariance { get; set; } = 0.1f;
    }

    public class StatusEffectConfig
    {
        public float DefaultDuration { get; set; } = 5f;
        public float DefaultTickInterval { get; set; } = 1f;
        public int DefaultMaxStacks { get; set; } = 3;
    }
} 