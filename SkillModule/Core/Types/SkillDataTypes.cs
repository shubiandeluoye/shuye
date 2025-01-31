namespace SkillModule.Core.Types
{
    public enum SkillType
    {
        None = 0,
        Attack = 1,    // 攻击技能
        Heal = 2,      // 治疗技能
        Barrier = 3,   // 屏障技能
        Box = 4,       // 盒子技能
        Shoot = 5,     // 射击技能
        Buff = 6       // 增益技能
    }

    public enum SkillState
    {
        None = 0,
        Ready = 1,     // 就绪
        Casting = 2,   // 施法中
        Cooldown = 3,  // 冷却中
        Disabled = 4   // 禁用
    }

    public class SkillConfigData
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public SkillType Type { get; set; }
        public float Cooldown { get; set; }
        public float Duration { get; set; }
        public bool CanCancel { get; set; }
    }
} 