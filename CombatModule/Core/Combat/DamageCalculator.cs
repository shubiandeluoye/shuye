using System;
using CombatModule.Core.Data;

namespace CombatModule.Core.Combat
{
    public class DamageCalculator
    {
        private readonly DamageConfig config;
        private readonly Random random;

        public DamageCalculator(DamageConfig config)
        {
            this.config = config;
            this.random = new Random();
        }

        public float CalculateDamage(DamageData damageData)
        {
            float finalDamage = damageData.BaseDamage;

            // 应用暴击
            if (ShouldCrit(damageData.CritChance))
            {
                finalDamage *= config.CritDamageMultiplier;
            }

            // 应用伤害类型修正
            finalDamage *= GetDamageTypeModifier(damageData.DamageType);

            // 应用随机波动
            finalDamage *= 1 + ((float)random.NextDouble() * 2 - 1) * config.DamageVariance;

            return (float)Math.Round(finalDamage, 2);
        }

        private bool ShouldCrit(float critChance)
        {
            return random.NextDouble() < critChance;
        }

        private float GetDamageTypeModifier(DamageType type)
        {
            switch (type)
            {
                case DamageType.Normal:
                    return 1.0f;
                case DamageType.Skill:
                    return config.SkillDamageMultiplier;
                case DamageType.Status:
                    return config.StatusDamageMultiplier;
                default:
                    return 1.0f;
            }
        }
    }
} 