using System;
using GameModule.Core.Data;
using System.Collections.Generic;

namespace GameModule.Core.Game
{
    public interface IGameHandler
    {
        // 游戏状态回调
        void OnGameStateChanged(GameState newState);
        
        // 玩家相关回调
        void OnPlayerJoined(Guid playerId);
        void OnPlayerLeft(Guid playerId);
        void OnPlayerStateChanged(Guid playerId, PlayerState newState);
        void OnPlayerDamaged(Guid playerId, float damage, DamageType damageType);
        void OnPlayerDied(Guid playerId);
        
        // 游戏进程回调
        void OnGameStarted(GameMode mode, int playerCount);
        void OnGameOver(Guid winnerId, Dictionary<Guid, int> finalScores);
        
        // 分数相关回调
        void OnScoreUpdated(Guid playerId, int newScore, int killStreak);
    }
} 