using UnityEngine;
using SkillModule.Core;

namespace PlayerModule.Core.Data
{
    public struct PlayerDamageEvent
    {
        public int PlayerId { get; set; }
        public int Damage { get; set; }
        public Vector3 DamageDirection { get; set; }
        public bool HasKnockback { get; set; }
    }

    public struct PlayerStunEvent
    {
        public int PlayerId { get; set; }
        public float Duration { get; set; }
    }

    public struct PlayerOutOfBoundsEvent
    {
        public int PlayerId { get; set; }
    }

    public struct PlayerHealthChangedEvent
    {
        public int PlayerId { get; set; }
        public int CurrentHealth { get; set; }
        public int PreviousHealth { get; set; }
        public int ChangeAmount { get; set; }
        public ModifyHealthType ModifyType { get; set; }
    }

    public struct PlayerDeathEvent
    {
        public int PlayerId { get; set; }
        public Vector3 DeathPosition { get; set; }
        public DeathReason DeathReason { get; set; }
    }

    public struct PlayerHealEvent
    {
        public int PlayerId { get; set; }
        public int HealAmount { get; set; }
        public ModifyHealthType HealType { get; set; }
        public object Source { get; set; }
    }

    public struct PlayerInputEvent
    {
        public int PlayerId { get; set; }
        public bool HasMovementInput { get; set; }
        public Vector3 MovementDirection { get; set; }
        public bool HasShootInput { get; set; }
        public ShootInputType ShootInputType { get; set; }
        public bool IsAngleTogglePressed { get; set; }
    }

    public struct PlayerShootEvent
    {
        public int PlayerId { get; set; }
        public int SkillId { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        public float[] Parameters { get; set; }
    }

    public struct PlayerCreatedEvent
    {
        public int PlayerId { get; set; }
        public PlayerConfig Config { get; set; }
    }

    public struct PlayerRemovedEvent
    {
        public int PlayerId { get; set; }
    }

    public struct PlayerUpdatedEvent
    {
        public int PlayerId { get; set; }
        public PlayerState State { get; set; }
        public float DeltaTime { get; set; }
    }

    public struct PlayerStateChangedEvent
    {
        public int PlayerId { get; set; }
        public PlayerState PreviousState { get; set; }
        public PlayerState NewState { get; set; }
    }

    public struct PlayerRevivedEvent
    {
        public int PlayerId { get; set; }
        public Vector3 RespawnPosition { get; set; }
    }

    public struct PlayerMovedEvent
    {
        public int PlayerId { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
    }

    public struct PlayerKnockbackEvent
    {
        public int PlayerId { get; set; }
        public Vector3 Direction { get; set; }
        public float Force { get; set; }
    }

    public struct BulletTypeChangedEvent
    {
        public int PlayerId { get; set; }
        public int BulletType { get; set; }
    }

    public struct WeaponChangedEvent
    {
        public int PlayerId { get; set; }
        public int WeaponType { get; set; }
    }

    public struct PlayerInitializedEvent
    {
        public int PlayerId { get; set; }
        public Vector3 StartPosition { get; set; }
    }
}
