using System;

namespace CombatModule.Core.Data
{
    public class DamageData
    {
        public float BaseDamage { get; set; }
        public DamageType DamageType { get; set; }
        public float CritChance { get; set; }
    }

    public enum DamageType
    {
        Normal,
        Skill,
        Status
    }
} 