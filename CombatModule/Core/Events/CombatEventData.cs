using System;
using CombatModule.Core.Data;

namespace CombatModule.Core.Events
{
    public class BulletHitEventData
    {
        public Guid BulletId { get; set; }
        public Guid TargetId { get; set; }
        public DamageData DamageData { get; set; }
    }

    public class SkillHitEventData
    {
        public Guid AttackerId { get; set; }
        public Guid TargetId { get; set; }
        public DamageData DamageData { get; set; }
    }

    public class StatusEffectEventData
    {
        public Guid TargetId { get; set; }
        public StatusEffectData EffectData { get; set; }
    }

    public enum StatusEffectEventType
    {
        Add,
        Remove,
        Stack
    }

    public class DotTickEventData
    {
        public Guid TargetId { get; set; }
        public DamageData DamageData { get; set; }
    }
} 