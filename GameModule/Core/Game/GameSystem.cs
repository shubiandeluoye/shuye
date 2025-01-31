using System;
using System.Collections.Generic;
using GameModule.Core.Data;
using System.Linq;

namespace GameModule.Core.Game
{
    public class GameSystem
    {
        private GameState currentState;
        private GameConfig config;
        private float gameTime;
        private Dictionary<Guid, PlayerData> players;
        private readonly IGameHandler gameHandler;
        private readonly ScoreSystem scoreSystem;
        private readonly GameRuleSystem ruleSystem;
        private readonly GameModeSystem modeSystem;

        public GameSystem(GameConfig config, IGameHandler handler)
        {
            this.config = config;
            this.gameHandler = handler;
            this.players = new Dictionary<Guid, PlayerData>();
            this.scoreSystem = new ScoreSystem(config, handler);
            this.ruleSystem = new GameRuleSystem(config, handler);
            this.modeSystem = new GameModeSystem(config);
            currentState = GameState.None;
        }

        public void AddPlayer(Guid playerId)
        {
            if (!players.ContainsKey(playerId))
            {
                players.Add(playerId, new PlayerData());
                scoreSystem.AddPlayer(playerId);
                gameHandler?.OnPlayerJoined(playerId);
                gameHandler?.OnPlayerStateChanged(playerId, PlayerState.Ready);
            }
        }

        public void RemovePlayer(Guid playerId)
        {
            if (players.ContainsKey(playerId))
            {
                players.Remove(playerId);
                gameHandler?.OnPlayerLeft(playerId);
            }
        }

        public void StartGame()
        {
            if (players.Count < config.minPlayers) return;
            
            currentState = GameState.Playing;
            gameTime = 0;
            gameHandler?.OnGameStateChanged(GameState.Playing);
            gameHandler?.OnGameStarted(modeSystem.GetCurrentMode(), players.Count);

            // 设置所有玩家状态
            foreach (var playerId in players.Keys)
            {
                gameHandler?.OnPlayerStateChanged(playerId, PlayerState.Playing);
            }
        }

        public void Update(float deltaTime)
        {
            if (currentState != GameState.Playing) return;

            gameTime += deltaTime;
            if (gameTime >= config.timeLimit)
            {
                EndGame();
            }
        }

        public void HandleDamage(Guid playerId, float damage, DamageType damageType)
        {
            if (!players.TryGetValue(playerId, out var playerData)) return;
            
            playerData.health -= damage;
            gameHandler?.OnPlayerDamaged(playerId, damage, damageType);

            if (playerData.health <= 0 && playerData.isAlive)
            {
                playerData.isAlive = false;
                playerData.state = PlayerState.Dead;
                gameHandler?.OnPlayerStateChanged(playerId, PlayerState.Dead);
                gameHandler?.OnPlayerDied(playerId);
                CheckGameOver();
            }
        }

        public void HandleKill(Guid killerId, Guid victimId)
        {
            scoreSystem.AddScore(killerId, 100); // 击杀得分
            scoreSystem.ResetKillStreak(victimId);
            
            var killerScore = scoreSystem.GetPlayerScore(killerId);
            gameHandler?.OnScoreUpdated(killerId, killerScore.currentScore, killerScore.killStreak);
        }

        private void CheckGameOver()
        {
            var alivePlayers = players.Where(p => p.Value.isAlive).ToList();
            if (alivePlayers.Count <= 1)
            {
                var winnerId = alivePlayers.FirstOrDefault().Key;
                EndGame(winnerId);
            }
        }

        private void EndGame(Guid winnerId = default)
        {
            currentState = GameState.GameOver;
            gameHandler?.OnGameStateChanged(GameState.GameOver);
            gameHandler?.OnGameOver(winnerId, scoreSystem.GetFinalScores());
        }
    }
} 