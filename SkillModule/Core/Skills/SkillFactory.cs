using SkillModule.Core.Types;

namespace SkillModule.Core.Skills
{
    public static class SkillFactory
    {
        public static BaseSkill CreateSkill(SkillConfigData config)
        {
            if (config == null)
                return null;

            return config.SkillType switch
            {
                SkillType.Box => new BoxSkill(config),
                SkillType.Barrier => new BarrierSkill(config),
                SkillType.Heal => new HealSkill(config),
                SkillType.Shoot => new ShootSkill(config),
                _ => null
            };
        }
    }
}
