using System;

namespace GameModule.Core.Data
{
    public class GameConfig
    {
        // 游戏基础配置
        public float gameTime = 300f;      // 游戏时间
        public int maxPlayers = 4;         // 最大玩家数
        public int minPlayers = 2;         // 最小玩家数
        
        // 游戏规则配置
        public float outOfBoundsDamage = 10f;  // 出界伤害
        public float fallDamage = 50f;         // 掉落伤害
        public float timeLimit = 300f;         // 时间限制

        // 游戏模式配置
        public GameMode gameMode = GameMode.Normal;
        public bool allowPracticeMode = true;  // 练习模式
    }

    // 从GameState.cs合并的枚举
    public enum GameState
    {
        None,               // 初始状态
        WaitingForPlayers,  // 等待玩家
        Playing,            // 游戏中
        GameOver           // 游戏结束
    }

    public enum GameMode
    {
        Normal,     // 标准模式
        Practice,   // 练习模式
        OneVsOne,   // 1v1对战
        TwoVsTwo    // 2v2对战
    }

    // 从GameEvents.cs合并的事件数据
    public enum DamageType
    {
        Normal,
        OutOfBounds,
        Fall
    }

    public enum PlayerState
    {
        None,
        Ready,
        Playing,
        Dead,
        Spectating
    }
}

[System.Serializable]
public class GameModeConfig
{
    public GameMode GameMode = GameMode.Normal;
    public bool EnablePracticeMode = false;    // 练习模式
    public bool EnableSpectatorMode = true;    // 观战模式
    public bool AllowLateJoin = true;         // 允许中途加入
    public float WarmupTime = 30f;            // 热身时间
    public float RoundTime = 180f;            // 每轮时间
    public int RoundCount = 3;                // 总轮数
}

public class RuleConfig
{
    public float TimeLimit = 300f;        // 游戏时间限制
    public int ScoreToWin = 1000;         // 胜利分数
    public int MinScoreToEnd = -500;      // 失败分数
    public bool EnableRespawn = true;     // 是否允许重生
    public float RespawnDelay = 3f;       // 重生延迟
    public int MaxDeaths = 3;             // 最大死亡次数
    public float OutOfBoundsDamage = 10f; // 出界伤害
    public bool EnableTeamMode = false;   // 是否开启队伍模式
    public int TeamsCount = 2;            // 队伍数量
} 