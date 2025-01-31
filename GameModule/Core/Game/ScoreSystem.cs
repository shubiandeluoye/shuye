using System;
using System.Collections.Generic;
using GameModule.Core.Data;

namespace GameModule.Core.Game
{
    public class ScoreSystem
    {
        private readonly GameConfig config;
        private readonly Dictionary<Guid, PlayerScore> playerScores;
        private readonly IGameHandler gameHandler;

        public ScoreSystem(GameConfig config, IGameHandler handler)
        {
            this.config = config;
            this.gameHandler = handler;
            this.playerScores = new Dictionary<Guid, PlayerScore>();
        }

        public void AddPlayer(Guid playerId)
        {
            if (!playerScores.ContainsKey(playerId))
            {
                playerScores.Add(playerId, new PlayerScore());
            }
        }

        public void AddScore(Guid playerId, int score)
        {
            if (!playerScores.TryGetValue(playerId, out var playerScore)) return;

            playerScore.currentScore += score;
            playerScore.killStreak++;

            // 检查连杀奖励
            CheckKillStreak(playerId, playerScore);
        }

        private void CheckKillStreak(Guid playerId, PlayerScore score)
        {
            int bonusScore = 0;
            switch (score.killStreak)
            {
                case 3:
                    bonusScore = 50;  // 三连杀
                    break;
                case 5:
                    bonusScore = 100; // 五连杀
                    break;
                case 7:
                    bonusScore = 200; // 七连杀
                    break;
                case 10:
                    bonusScore = 500; // 超神
                    break;
            }

            if (bonusScore > 0)
            {
                score.currentScore += bonusScore;
            }
        }

        public void ResetKillStreak(Guid playerId)
        {
            if (playerScores.TryGetValue(playerId, out var score))
            {
                score.killStreak = 0;
            }
        }

        public PlayerScore GetPlayerScore(Guid playerId)
        {
            if (playerScores.TryGetValue(playerId, out var score))
            {
                return score;
            }
            return null;
        }

        public Dictionary<Guid, int> GetFinalScores()
        {
            var finalScores = new Dictionary<Guid, int>();
            foreach (var kvp in playerScores)
            {
                finalScores.Add(kvp.Key, kvp.Value.currentScore);
            }
            return finalScores;
        }
    }

    public class PlayerScore
    {
        public int currentScore;
        public int killStreak;
    }
} 