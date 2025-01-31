namespace CombatModule.Core.Data
{
    public class ActiveStatusEffect
    {
        public StatusEffectData Data { get; set; }
        public float RemainingTime { get; set; }
        public int StackCount { get; set; }
    }
}
