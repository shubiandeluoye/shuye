
namespace SkillModule.Core.Types
{
    public enum EffectType
    {
        None = 0,
        Damage = 1,    // 伤害效果
        Heal = 2,      // 治疗效果
        Shield = 3,    // 护盾效果
        Speed = 4,     // 速度效果
        Stun = 5,      // 眩晕效果
        Buff = 6,      // 增益效果
        Area = 7       // 区域效果
    }

    public interface IEffectData
    {
        int EffectId { get; set; }
        EffectType Type { get; set; }
        object Target { get; set; }
    }

    public struct Vector3Data
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3Data(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public struct EffectData : IEffectData
    {
        public int EffectId { get; set; }
        public EffectType Type { get; set; }
        public object Target { get; set; }
        public Vector3Data Position { get; set; }
        public Vector3Data Direction { get; set; }
        public float Duration { get; set; }
        public float[] Parameters { get; set; }
    }

    public struct ProjectileEffectData : IEffectData
    {
        public int EffectId { get; set; }
        public EffectType Type { get; set; }
        public object Target { get; set; }
        public float Speed { get; set; }
        public float Damage { get; set; }
        public Vector3Data Direction { get; set; }
        public float Range { get; set; }
        public bool IsPenetrate { get; set; }
    }

    public struct AreaEffectData : IEffectData
    {
        public int EffectId { get; set; }
        public EffectType Type { get; set; }
        public object Target { get; set; }
        public float Radius { get; set; }
        public float Duration { get; set; }
        public Vector3Data Position { get; set; }
        public float Interval { get; set; }
        public float DamagePerTick { get; set; }
    }

    public struct BarrierEffectData : IEffectData
    {
        public int EffectId { get; set; }
        public EffectType Type { get; set; }
        public object Target { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Health { get; set; }
        public Vector3Data Position { get; set; }
        public float Duration { get; set; }
    }

    public struct BuffEffectData : IEffectData
    {
        public int EffectId { get; set; }
        public EffectType Type { get; set; }
        public object Target { get; set; }
        public float Duration { get; set; }
        public float Value { get; set; }
        public bool IsStackable { get; set; }
        public int MaxStacks { get; set; }
    }
}
