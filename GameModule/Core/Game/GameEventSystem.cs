using System;
using GameModule.Core.Data;
using GameModule.Core.Events;

namespace GameModule.Core.Game
{
    public class GameEventSystem
    {
        private readonly IGameHandler gameHandler;

        public GameEventSystem(IGameHandler handler)
        {
            this.gameHandler = handler;
        }

        public void FireGameStart(GameMode mode, int playerCount)
        {
            var eventData = new GameStartEventData
            {
                GameMode = mode,
                PlayerCount = playerCount
            };
            // 触发游戏开始事件
        }

        public void FireGameEnd(Guid winnerId, float gameTime, Dictionary<Guid, int> scores)
        {
            var eventData = new GameEndEventData
            {
                WinnerId = winnerId,
                GameTime = gameTime,
                FinalScores = scores
            };
            // 触发游戏结束事件
        }
    }
} 