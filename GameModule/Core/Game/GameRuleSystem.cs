using System;
using GameModule.Core.Data;
using GameModule.Core.Events;

namespace GameModule.Core.Game
{
    public class GameRuleSystem
    {
        private readonly GameConfig config;
        private readonly IGameHandler gameHandler;

        public GameRuleSystem(GameConfig config, IGameHandler handler)
        {
            this.config = config;
            this.gameHandler = handler;
        }

        public void CheckOutOfBounds(Guid playerId, Vector2Data position)
        {
            // 检查是否超出7x7范围
            if (Mathf.Abs(position.X) > 3.5f || Mathf.Abs(position.Y) > 3.5f)
            {
                gameHandler?.OnPlayerDamaged(playerId, config.outOfBoundsDamage, DamageType.OutOfBounds);
            }
        }

        public void CheckFallDamage(Guid playerId, float fallHeight)
        {
            if (fallHeight < -50f)
            {
                gameHandler?.OnPlayerDamaged(playerId, config.fallDamage, DamageType.Fall);
            }
        }

        public bool IsValidPosition(Vector2Data position)
        {
            // 检查是否在有效范围内
            return Mathf.Abs(position.X) <= 3.5f && 
                   Mathf.Abs(position.Y) <= 3.5f;
        }
    }
} 