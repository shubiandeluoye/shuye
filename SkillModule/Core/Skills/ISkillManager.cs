using SkillModule.Core.Types;

namespace SkillModule.Core.Skills
{
    public interface ISkillManager
    {
        void RegisterSkill(SkillConfigData config);
        BaseSkill CreateSkill(int skillId, object owner);
        bool UseSkill(int skillId, SkillContext context);
        SkillConfigData GetSkillConfig(int skillId);
        bool IsSkillReady(int skillId);
        float GetSkillCooldown(int skillId);
        void Update(float deltaTime);
    }
}
