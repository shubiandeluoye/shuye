using System;
using System.Collections.Generic;
using PlayerModule.Core.Data;

namespace PlayerModule.Core.Systems
{
    public interface IPlayerEventSystem
    {
        void Subscribe<TEvent>(Action<TEvent> handler);
        void Unsubscribe<TEvent>(Action<TEvent> handler);
        void Publish<TEvent>(TEvent eventData);
        void Clear();
    }

    public class PlayerEventSystem : IPlayerEventSystem
    {
        private static readonly Lazy<PlayerEventSystem> instance = 
            new Lazy<PlayerEventSystem>(() => new PlayerEventSystem());
        
        public static PlayerEventSystem Instance => instance.Value;

        private readonly Dictionary<Type, List<Delegate>> eventHandlers = new Dictionary<Type, List<Delegate>>();

        private PlayerEventSystem() {}

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            var eventType = typeof(TEvent);
            ValidateEventType(eventType);

            if (!eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType] = new List<Delegate>();
            }

            eventHandlers[eventType].Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            var eventType = typeof(TEvent);
            ValidateEventType(eventType);

            if (eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType].Remove(handler);
            }
        }

        public void Publish<TEvent>(TEvent eventData)
        {
            if (eventData == null)
                throw new ArgumentNullException(nameof(eventData));

            var eventType = typeof(TEvent);
            ValidateEventType(eventType);

            if (eventHandlers.ContainsKey(eventType))
            {
                foreach (var handler in eventHandlers[eventType])
                {
                    try
                    {
                        ((Action<TEvent>)handler).Invoke(eventData);
                    }
                    catch (Exception e)
                    {
                        throw new PlayerEventSystemException($"Error handling event {eventType.Name}", e);
                    }
                }
            }
        }

        public void Clear()
        {
            eventHandlers.Clear();
        }

        private void ValidateEventType(Type eventType)
        {
            if (!eventType.Namespace.StartsWith("PlayerModule.Core.Data"))
            {
                throw new PlayerEventSystemException(
                    $"Invalid event type: {eventType.FullName}. " +
                    "Player events must be defined in PlayerModule.Core.Data namespace");
            }
        }
    }

    public class PlayerEventSystemException : Exception
    {
        public PlayerEventSystemException(string message) : base(message) { }
        public PlayerEventSystemException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
