using System;
using Core.Events;
using MapModule.Core.Events;

namespace MapModule.Core.Systems
{
    public class MapEventSystem : CoreEventSystem
    {
        private static MapEventSystem instance;
        public static MapEventSystem Instance => instance ??= new MapEventSystem();

        public MapEventSystem() : base()
        {
        }

        public void Subscribe(MapEventType eventType, Action<MapEventData> handler)
        {
            base.Subscribe(eventType.ToString(), handler);
        }

        public void Publish(MapEventType eventType, MapEventData eventData)
        {
            base.Publish(eventType.ToString(), eventData);
        }

        public void Subscribe<T>(MapEventType eventType, Action<T> handler) where T : MapEventData
        {
            base.Subscribe(eventType.ToString(), (data) => 
            {
                if (data is T typedData)
                {
                    handler(typedData);
                }
            });
        }

        public void Publish<T>(MapEventType eventType, T eventData) where T : MapEventData
        {
            base.Publish(eventType.ToString(), eventData);
        }

        public void Clear()
        {
            base.Clear();
        }
    }
}
