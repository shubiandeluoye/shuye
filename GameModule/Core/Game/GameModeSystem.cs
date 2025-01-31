using System;
using GameModule.Core.Data;

namespace GameModule.Core.Game
{
    public class GameModeSystem
    {
        private readonly GameConfig config;
        private GameMode currentMode;

        public GameModeSystem(GameConfig config)
        {
            this.config = config;
            this.currentMode = config.gameMode;
        }

        public bool CanStartGame(int playerCount)
        {
            switch (currentMode)
            {
                case GameMode.OneVsOne:
                    return playerCount == 2;
                case GameMode.TwoVsTwo:
                    return playerCount == 4;
                case GameMode.Practice:
                    return playerCount >= 1;
                default:
                    return playerCount >= config.minPlayers;
            }
        }

        public bool IsPracticeMode()
        {
            return currentMode == GameMode.Practice;
        }

        public bool AllowRespawn()
        {
            return currentMode == GameMode.Practice;
        }

        public GameMode GetCurrentMode()
        {
            return currentMode;
        }
    }
} 