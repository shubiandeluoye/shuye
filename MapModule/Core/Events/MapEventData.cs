using System;

namespace MapModule.Core.Events
{
    public abstract class MapEventData
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public MapEventType EventType { get; protected set; }
        
        protected MapEventData(MapEventType eventType)
        {
            EventType = eventType;
        }
    }

    public class ShapeEventData : MapEventData
    {
        public string ShapeId { get; }
        public string ShapeType { get; }
        
        public ShapeEventData(MapEventType eventType, string shapeId, string shapeType) 
            : base(eventType)
        {
            ShapeId = shapeId;
            ShapeType = shapeType;
        }
    }

    public class AnimationEventData : MapEventData
    {
        public string AnimationName { get; }
        public float Duration { get; }
        
        public AnimationEventData(MapEventType eventType, string animationName, float duration)
            : base(eventType)
        {
            AnimationName = animationName;
            Duration = duration;
        }
    }

    public class MapStateEventData : MapEventData
    {
        public string StateName { get; }
        
        public MapStateEventData(MapEventType eventType, string stateName)
            : base(eventType)
        {
            StateName = stateName;
        }
    }

    public class AreaEventData : MapEventData
    {
        public string AreaId { get; }
        public string PlayerId { get; }
        
        public AreaEventData(MapEventType eventType, string areaId, string playerId)
            : base(eventType)
        {
            AreaId = areaId;
            PlayerId = playerId;
        }
    }
}
