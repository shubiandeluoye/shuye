using System;

namespace Core.Events
{
    public class EventBridge
    {
        private readonly CoreEventSystem coreSystem;

        public EventBridge(CoreEventSystem coreSystem)
        {
            this.coreSystem = coreSystem;
        }

        public void Publish(CoreEventType type, object data)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            coreSystem.Publish(type, data);
        }

        public void Subscribe(CoreEventType type, Action<object> handler)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            coreSystem.Subscribe(type, handler);
        }

        public void Unsubscribe(CoreEventType type, Action<object> handler)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            coreSystem.Unsubscribe(type, handler);
        }
    }
}
