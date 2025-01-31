using System.Collections.Generic;
using SkillModule.Core.Types;

namespace SkillModule.Core.Skills
{
    public class SkillManager : ISkillManager
    {
        private readonly Dictionary<int, SkillConfigData> _skillConfigs = new();
        private readonly Dictionary<int, BaseSkill> _activeSkills = new();
        private readonly Dictionary<int, float> _skillCooldowns = new();

        public void RegisterSkill(SkillConfigData config)
        {
            if (config == null || _skillConfigs.ContainsKey(config.SkillId))
                return;

            _skillConfigs[config.SkillId] = config;
        }

        public BaseSkill CreateSkill(int skillId, object owner)
        {
            if (!_skillConfigs.TryGetValue(skillId, out var config))
                return null;

            var skill = SkillFactory.CreateSkill(config);
            if (skill == null)
                return null;

            skill.Initialize(owner);
            _activeSkills[skillId] = skill;
            return skill;
        }

        public bool UseSkill(int skillId, SkillContext context)
        {
            if (!_activeSkills.TryGetValue(skillId, out var skill) || !IsSkillReady(skillId))
                return false;

            if (!skill.Use(context))
                return false;

            _skillCooldowns[skillId] = skill.Cooldown;
            return true;
        }

        public SkillConfigData GetSkillConfig(int skillId)
        {
            return _skillConfigs.TryGetValue(skillId, out var config) ? config : null;
        }

        public bool IsSkillReady(int skillId)
        {
            return !_skillCooldowns.ContainsKey(skillId) || _skillCooldowns[skillId] <= 0;
        }

        public float GetSkillCooldown(int skillId)
        {
            return _skillCooldowns.TryGetValue(skillId, out var cooldown) ? cooldown : 0;
        }

        public void Update(float deltaTime)
        {
            foreach (var skillId in _skillCooldowns.Keys)
            {
                _skillCooldowns[skillId] -= deltaTime;
                if (_skillCooldowns[skillId] <= 0)
                    _skillCooldowns.Remove(skillId);
            }
        }

        // 新增方法：处理技能释放事件
        public void HandleSkillReleaseEvent(int skillId, SkillContext context)
        {
            if (UseSkill(skillId, context))
            {
                // 触发技能效果
                var skill = _activeSkills[skillId];
                skill.TriggerEffect(context);
            }
        }
    }
}