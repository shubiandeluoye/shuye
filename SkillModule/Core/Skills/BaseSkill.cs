using SkillModule.Core.Types;
using SkillModule.Core.Events;

namespace SkillModule.Core.Skills
{
    public abstract class BaseSkill
    {
        protected SkillConfigData skillData;
        protected SkillState currentState;
        protected float lastUseTime;
        protected float cooldownEndTime;
        protected SkillContext skillContext;

        public int SkillId => skillData.SkillId;
        public string SkillName => skillData.SkillName;
        public float Cooldown => skillData.Cooldown;
        public float Duration => skillData.Duration;
        public bool IsActive => currentState == SkillState.Casting;

        protected BaseSkill(SkillConfigData data)
        {
            skillData = data;
            currentState = SkillState.Ready;
            lastUseTime = 0;
            cooldownEndTime = 0;
        }

        public virtual bool CanUse()
        {
            return currentState == SkillState.Ready;
        }

        public virtual void Use(SkillContext context)
        {
            if (!CanUse()) return;

            skillContext = context;
            OnSkillStart();
            currentState = SkillState.Casting;
            lastUseTime = GetCurrentTime();

            // 发布技能开始事件
            var startEvent = new SkillStartEvent
            {
                SkillId = SkillId,
                Position = context.Position,
                Direction = context.Direction,
                Caster = context.Source,
                StartTime = lastUseTime
            };
            // TODO: 通过事件系统发布事件
        }

        public virtual void Cancel()
        {
            if (currentState != SkillState.Casting || !skillData.CanCancel) return;
            
            currentState = SkillState.Ready;
            OnCancel();
            TriggerEndEvent(false, "Cancelled");
        }

        protected abstract void OnSkillStart();
        protected virtual void OnCancel() { }

        protected virtual void TriggerEndEvent(bool wasSuccessful, string reason)
        {
            var endEvent = new SkillEndEvent
            {
                SkillId = SkillId,
                WasSuccessful = wasSuccessful,
                EndReason = reason,
                Duration = GetCurrentTime() - lastUseTime
            };
            // TODO: 通过事件系统发布事件

            // 触发技能效果
            TriggerEffect(skillContext);
        }

        protected virtual float GetCurrentTime()
        {
            return System.DateTime.Now.Ticks / 10000000f; // 转换为秒
        }

        protected float GetRemainingCooldown()
        {
            return System.Math.Max(cooldownEndTime - GetCurrentTime(), 0);
        }

        // 新增方法：触发技能效果
        protected virtual void TriggerEffect(SkillContext context)
        {
            // 子类实现具体的技能效果触发逻辑
        }
    }
}