using SkillModule.Core.Types;

namespace SkillModule.Core.Events
{
    public class SkillStartEvent
    {
        public int SkillId { get; set; }
        public Vector3Data Position { get; set; }
        public Vector3Data Direction { get; set; }
        public object Caster { get; set; }
        public float StartTime { get; set; }
    }

    public class SkillEndEvent
    {
        public int SkillId { get; set; }
        public bool WasSuccessful { get; set; }
        public string EndReason { get; set; }
        public float Duration { get; set; }
    }

    public class SkillCooldownEvent
    {
        public int SkillId { get; set; }
        public float CooldownDuration { get; set; }
        public float RemainingCooldown { get; set; }
        public bool IsReady { get; set; }
    }

    public class SkillStateChangeEvent
    {
        public int SkillId { get; set; }
        public SkillState OldState { get; set; }
        public SkillState NewState { get; set; }
        public float StateChangeTime { get; set; }
    }
} 