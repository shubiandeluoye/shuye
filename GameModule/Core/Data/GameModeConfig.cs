namespace GameModule.Core.Data
{
    public class GameModeConfig
    {
        public GameMode GameMode = GameMode.Normal;
        public bool AllowLateJoin = true;
        public bool EnableSpectator = true;
        public float RoundTime = 180f;
    }
} 