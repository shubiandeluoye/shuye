using System.Collections.Generic;
using MapModule.Core.Data;
using MapModule.Core.Events;

namespace MapModule.Core.Systems
{
    public class MapManager : IMapManager, IPausable
    {
        private static MapManager instance;
        public static MapManager Instance => instance ??= new MapManager();

        private readonly MapEventSystem eventSystem;
        private readonly Dictionary<int, ShapeState> shapes = new();
        private readonly List<IMapSystem> systems = new();
        private MapAnimationSystem animationSystem;
        private bool isPaused;

        private MapManager()
        {
            eventSystem = MapEventSystem.Instance;
            isPaused = false;
        }

        public void Initialize(MapConfig config)
        {
            // 初始化各个系统
            var centralAreaSystem = new CentralAreaSystem(config, this);
            var shapeSystem = new ShapeSystem(config, this);
            animationSystem = new MapAnimationSystem(config);

            // 订阅动画相关事件
            eventSystem.Subscribe(MapEvents.AnimationStarted, OnAnimationStarted);
            eventSystem.Subscribe(MapEvents.AnimationCompleted, OnAnimationCompleted);
            eventSystem.Subscribe(MapEvents.BlockPositionUpdated, OnBlockPositionUpdated);

            RegisterSystem(centralAreaSystem);
            RegisterSystem(shapeSystem);
        }

        public void Pause()
        {
            isPaused = true;
            foreach (var system in systems)
            {
                if (system is IPausable pausable)
                {
                    pausable.Pause();
                }
            }
        }

        public void Resume()
        {
            isPaused = false;
            foreach (var system in systems)
            {
                if (system is IPausable pausable)
                {
                    pausable.Resume();
                }
            }
        }

        private void OnAnimationStarted(AnimationEventData data)
        {
            // 暂停其他系统
            foreach (var system in systems)
            {
                if (system is IPausable pausable)
                {
                    pausable.Pause();
                }
            }
        }

        private void OnAnimationCompleted(AnimationEventData data)
        {
            // 恢复其他系统
            foreach (var system in systems)
            {
                if (system is IPausable pausable)
                {
                    pausable.Resume();
                }
            }
        }

        private void OnBlockPositionUpdated(BlockPositionEventData data)
        {
            UpdateBlockPosition(data.Position, data.Height);
        }

        private void UpdateBlockPosition(Vector2D position, float height)
        {
            if (shapes.TryGetValue(position.GetHashCode(), out var shape))
            {
                shape.Position = position;
                shape.Height = height;
                
                // 通知相关系统更新
                PublishEvent(MapEvents.ShapeChanged, new ShapeChangedEvent
                {
                    Position = position,
                    Height = height
                });
            }
        }

        public void Update(float deltaTime)
        {
            if (isPaused) return;
            animationSystem?.Update(deltaTime);
        }

        public void StartResetAnimation(Vector2D startPosition)
        {
            animationSystem?.StartResetAnimation(startPosition);
        }

        public void PublishEvent<T>(string eventName, T eventData)
        {
            eventSystem.Publish(eventName, eventData);
        }

        public void RegisterSystem(IMapSystem system)
        {
            systems.Add(system);
            system.Initialize(this);
        }

        public void Dispose()
        {
            // 取消订阅事件
            eventSystem.Unsubscribe(MapEvents.AnimationStarted, OnAnimationStarted);
            eventSystem.Unsubscribe(MapEvents.AnimationCompleted, OnAnimationCompleted);
            eventSystem.Unsubscribe(MapEvents.BlockPositionUpdated, OnBlockPositionUpdated);

            // 清理系统
            foreach (var system in systems)
            {
                system.Dispose();
            }
            systems.Clear();
            shapes.Clear();
            eventSystem.Clear();
        }
    }
}
