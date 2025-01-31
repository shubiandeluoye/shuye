using System;
using System.Collections.Generic;
using PlayerModule.Core.Data;
using PlayerModule.Core.Events;

namespace PlayerModule.Core.Systems
{
    public interface IPlayerManager
    {
        void RegisterPlayer(int playerId, PlayerConfig config);
        void UnregisterPlayer(int playerId);
        PlayerState GetPlayerState(int playerId);
        void UpdatePlayer(int playerId, float deltaTime);
        void HandleInput(int playerId, PlayerInputData input);
        void Clear();
    }

    public class PlayerManager : IPlayerManager
    {
        private readonly Dictionary<int, PlayerState> players = new Dictionary<int, PlayerState>();
        private readonly IPlayerEventSystem eventSystem;

        public PlayerManager(IPlayerEventSystem eventSystem)
        {
            this.eventSystem = eventSystem ?? throw new ArgumentNullException(nameof(eventSystem));
        }

        public void RegisterPlayer(int playerId, PlayerConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (players.ContainsKey(playerId))
                throw new ArgumentException($"Player {playerId} already exists");

            var state = new PlayerState
            {
                PlayerId = playerId,
                Config = config,
                HealthSystem = new HealthSystem(config.HealthConfig),
                MovementSystem = new MovementSystem(config.MovementConfig),
                ShootingSystem = new ShootingSystem(config.ShootingConfig)
            };

            players[playerId] = state;

            eventSystem.Publish(new PlayerCreatedEvent
            {
                PlayerId = playerId,
                Config = config
            });
        }

        public void UnregisterPlayer(int playerId)
        {
            if (!players.ContainsKey(playerId))
                throw new ArgumentException($"Player {playerId} not found");

            eventSystem.Publish(new PlayerRemovedEvent
            {
                PlayerId = playerId
            });

            players.Remove(playerId);
        }

        public PlayerState GetPlayerState(int playerId)
        {
            if (!players.TryGetValue(playerId, out var state))
                throw new ArgumentException($"Player {playerId} not found");

            return state;
        }

        public void UpdatePlayer(int playerId, float deltaTime)
        {
            var state = GetPlayerState(playerId);
            state.HealthSystem.Update(deltaTime);
            state.MovementSystem.Update(deltaTime);
            state.ShootingSystem.Update(deltaTime);
        }

        public void HandleInput(int playerId, PlayerInputData input)
        {
            var state = GetPlayerState(playerId);
            state.MovementSystem.HandleInput(input);
            state.ShootingSystem.HandleInput(input);
        }

        public void Clear()
        {
            players.Clear();
            eventSystem.Clear();
        }
    }
}
