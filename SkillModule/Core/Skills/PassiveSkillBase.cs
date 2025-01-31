using SkillModule.Core.Types;
using SkillModule.Core.Events;

namespace SkillModule.Core.Skills
{
    public abstract class PassiveSkillBase
    {
        protected SkillConfigData skillData;
        protected float lastTriggerTime;
        protected bool isEnabled = true;

        public int SkillId => skillData.SkillId;
        public string SkillName => skillData.SkillName;
        public float Cooldown => skillData.Cooldown;

        protected PassiveSkillBase(SkillConfigData data)
        {
            skillData = data;
            lastTriggerTime = -Cooldown;
            Initialize();
        }

        public virtual void Initialize()
        {
            RegisterEvents();
        }

        protected virtual void RegisterEvents()
        {
            // 子类实现具体的事件注册
        }

        protected virtual void UnregisterEvents()
        {
            // 子类实现具体的事件注销
        }

        protected virtual bool CanTrigger(SkillContext context)
        {
            if (!isEnabled) return false;
            if (GetCurrentTime() - lastTriggerTime < Cooldown) return false;
            
            return ValidateParameters(context);
        }

        protected virtual bool ValidateParameters(SkillContext context)
        {
            // 子类重写以实现具体的参数验证
            return true;
        }

        protected abstract void ExecuteSkill(SkillContext context);

        public virtual void Enable() => isEnabled = true;
        public virtual void Disable() => isEnabled = false;
        public virtual bool IsReady() => GetCurrentTime() - lastTriggerTime >= Cooldown;
        
        protected virtual float GetCurrentTime()
        {
            return System.DateTime.Now.Ticks / 10000000f; // 转换为秒
        }

        public virtual void Trigger(SkillContext context)
        {
            if (!CanTrigger(context)) return;

            ExecuteSkill(context);
            lastTriggerTime = GetCurrentTime();

            // 触发技能事件
            var startEvent = new SkillStartEvent
            {
                SkillId = SkillId,
                Position = context.Position,
                Direction = context.Direction,
                Caster = context.Source,
                StartTime = lastTriggerTime
            };
            // TODO: 通过事件系统发布事件

            // 触发技能效果
            TriggerEffect(context);
        }

        // 新增方法：触发技能效果
        protected virtual void TriggerEffect(SkillContext context)
        {
            // 子类实现具体的技能效果触发逻辑
        }
    }
}