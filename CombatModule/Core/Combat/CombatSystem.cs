using System;
using System.Collections.Generic;
using CombatModule.Core.Data;
using CombatModule.Core.Events;

namespace CombatModule.Core.Combat
{
    public class CombatSystem
    {
        private readonly DamageCalculator damageCalculator;
        private readonly Dictionary<Guid, Action<float>> damageCallbacks;
        private readonly Dictionary<Guid, List<ActiveStatusEffect>> activeEffects;

        public CombatSystem(CombatConfig config)
        {
            damageCalculator = new DamageCalculator(config.DamageConfig);
            damageCallbacks = new Dictionary<Guid, Action<float>>();
            activeEffects = new Dictionary<Guid, List<ActiveStatusEffect>>();
        }

        public void RegisterDamageCallback(Guid targetId, Action<float> callback)
        {
            damageCallbacks[targetId] = callback;
        }

        public void UnregisterDamageCallback(Guid targetId)
        {
            if (damageCallbacks.ContainsKey(targetId))
            {
                damageCallbacks.Remove(targetId);
            }
        }

        public void HandleDamage(Guid attackerId, Guid targetId, DamageData damageData)
        {
            if (!IsValidDamageRequest(attackerId, targetId)) return;

            float finalDamage = damageCalculator.CalculateDamage(damageData);
            if (damageCallbacks.TryGetValue(targetId, out var callback))
            {
                callback.Invoke(finalDamage);
            }
        }

        public void Update(float deltaTime)
        {
            var targets = new List<Guid>(activeEffects.Keys);
            foreach (var targetId in targets)
            {
                UpdateTargetEffects(targetId, deltaTime);
            }
        }

        private void UpdateTargetEffects(Guid targetId, float deltaTime)
        {
            var effects = activeEffects[targetId];
            for (int i = effects.Count - 1; i >= 0; i--)
            {
                var effect = effects[i];
                effect.RemainingTime -= deltaTime;

                if (effect.RemainingTime <= 0)
                {
                    effects.RemoveAt(i);
                }
            }
        }

        public void AddEffect(Guid targetId, StatusEffectData effectData)
        {
            if (!activeEffects.ContainsKey(targetId))
            {
                activeEffects[targetId] = new List<ActiveStatusEffect>();
            }

            var existingEffect = activeEffects[targetId].Find(e => e.Data.Type == effectData.Type);
            if (existingEffect != null)
            {
                existingEffect.RemainingTime = effectData.Duration;
                existingEffect.StackCount++;
            }
            else
            {
                var newEffect = new ActiveStatusEffect
                {
                    Data = effectData,
                    RemainingTime = effectData.Duration,
                    StackCount = 1
                };
                activeEffects[targetId].Add(newEffect);
            }
        }

        public void RemoveEffect(Guid targetId, StatusEffectType effectType)
        {
            if (!activeEffects.ContainsKey(targetId)) return;

            var effects = activeEffects[targetId];
            var effect = effects.Find(e => e.Data.Type == effectType);
            if (effect != null)
            {
                effects.Remove(effect);
            }
        }

        private bool IsValidDamageRequest(Guid attackerId, Guid targetId)
        {
            return attackerId != Guid.Empty && targetId != Guid.Empty && attackerId != targetId;
        }

        public void Clear()
        {
            damageCallbacks.Clear();
            activeEffects.Clear();
        }
    }
}
