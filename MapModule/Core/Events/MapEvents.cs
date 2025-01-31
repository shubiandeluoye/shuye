using MapModule.Core.Data;

namespace MapModule.Core.Events
{
    public static class MapEvents
    {
        // 动画相关事件
        public const string AnimationStarted = "MapAnimationStarted";
        public const string AnimationCompleted = "MapAnimationCompleted";
        public const string AnimationPaused = "MapAnimationPaused";
        public const string AnimationResumed = "MapAnimationResumed";
        
        // 方块位置更新事件
        public const string BlockPositionUpdated = "BlockPositionUpdated";
        
        public class AnimationEventData
        {
            public Vector2D StartPosition { get; }
            public float Duration { get; }
            public float CurrentTime { get; }
            
            public AnimationEventData(Vector2D startPosition, float duration)
            {
                StartPosition = startPosition;
                Duration = duration;
                CurrentTime = 0f;
            }

            public AnimationEventData(Vector2D startPosition, float duration, float currentTime)
            {
                StartPosition = startPosition;
                Duration = duration;
                CurrentTime = currentTime;
            }
        }
        
        public class BlockPositionEventData
        {
            public Vector2D Position { get; }
            public float Height { get; }
            
            public BlockPositionEventData(Vector2D position, float height)
            {
                Position = position;
                Height = height;
            }
        }
    }
}
