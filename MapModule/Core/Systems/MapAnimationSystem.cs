using System.Collections.Generic;
using MapModule.Core.Data;
using Core.Types;
using MapModule.Core.Events;

namespace MapModule.Core.Systems
{
    public class MapAnimationSystem : IMapSystem, IPausable
    {
        private readonly MapConfig config;
        private readonly List<Vector2D> activeBlocks = new();
        private float animationTimer;
        private bool isAnimating;
        private bool isPaused;

        public MapAnimationSystem(MapConfig config)
        {
            this.config = config;
            isPaused = false;
        }

        public void Pause()
        {
            isPaused = true;
            MapManager.Instance.PublishEvent(
                MapEvents.AnimationPaused,
                new AnimationEventData(activeBlocks[0], animationTimer)
            );
        }

        public void Resume()
        {
            isPaused = false;
            MapManager.Instance.PublishEvent(
                MapEvents.AnimationResumed,
                new AnimationEventData(activeBlocks[0], animationTimer)
            );
        }

        public void StartResetAnimation(Vector2D startPosition)
        {
            if (isAnimating || isPaused) return;
            
            isAnimating = true;
            animationTimer = 0f;
            activeBlocks.Clear();
            activeBlocks.Add(startPosition);
            
            MapManager.Instance.PublishEvent(
                MapEvents.AnimationStarted,
                new AnimationEventData(startPosition, config.ResetAnimationDuration)
            );
        }

        public void Update(float deltaTime)
        {
            if (!isAnimating || isPaused) return;

            animationTimer += deltaTime;
            float waveProgress = animationTimer * config.WavePropagationSpeed;

            // 更新每个方块的动画状态
            for (int i = 0; i < activeBlocks.Count; i++)
            {
                var block = activeBlocks[i];
                float blockProgress = waveProgress - block.DistanceTo(activeBlocks[0]);
                
                if (blockProgress < 0) continue;
                
                float height = CalculateBlockHeight(blockProgress);
                MapManager.Instance.PublishEvent(
                    MapEvents.BlockPositionUpdated,
                    new BlockPositionEventData(block, height)
                );
            }

            // 检查动画是否完成
            if (animationTimer >= config.ResetAnimationDuration)
            {
                isAnimating = false;
                MapManager.Instance.PublishEvent(
                    MapEvents.AnimationCompleted,
                    new AnimationEventData(activeBlocks[0], config.ResetAnimationDuration)
                );
            }
        }

        private float CalculateBlockHeight(float progress)
        {
            float normalizedTime = progress / config.BlockAnimationDuration;
            if (normalizedTime > 1f) return 0f;

            // 使用正弦波实现上下浮动效果
            return config.MaxBlockHeight * MathF.Sin(normalizedTime * MathF.PI);
        }

        public void Initialize(IMapManager manager)
        {
            // 初始化时不需要额外操作
        }

        public void Dispose()
        {
            activeBlocks.Clear();
        }
    }
}
