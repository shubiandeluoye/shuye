using System;
using MapModule.Core.Data;
using MapModule.Core.Shapes;
using MapModule.Core.Utils;

namespace MapModule.Core.Systems
{
    public class ShapeSystem : IMapSystem, IPausable
    {
        private readonly MapConfig config;
        private readonly ShapeFactory shapeFactory;
        private readonly CentralAreaSystem centralAreaSystem;
        private IShape currentShape;
        private float shapeTimer;
        private IMapManager manager;
        private bool isPaused;

        public ShapeSystem(MapConfig config, CentralAreaSystem centralArea)
        {
            this.config = config;
            this.centralAreaSystem = centralArea;
            shapeTimer = 0;
            isPaused = false;
        }

        public void Initialize(IMapManager manager)
        {
            this.manager = manager;
            shapeFactory = new ShapeFactory(manager);
        }

        public void Update(float deltaTime)
        {
            if (isPaused) return;

            TimeUtils.Update(TimeUtils.GetCurrentTime() + deltaTime, deltaTime);

            if (currentShape == null) return;

            // 更新形状
            currentShape.Update(deltaTime);

            // 检查形状切换
            shapeTimer += deltaTime;
            if (shapeTimer >= config.ShapeChangeInterval)
            {
                TriggerShapeChange();
            }
        }

        public void Pause()
        {
            isPaused = true;
            currentShape?.Pause();
            manager.PublishEvent(MapEvents.AnimationPaused, new AnimationEventData(
                currentShape?.GetState().Position.ToVector2D() ?? Vector2D.Zero,
                config.ShapeChangeInterval - shapeTimer
            ));
        }

        public void Resume()
        {
            isPaused = false;
            currentShape?.Resume();
            manager.PublishEvent(MapEvents.AnimationResumed, new AnimationEventData(
                currentShape?.GetState().Position.ToVector2D() ?? Vector2D.Zero,
                config.ShapeChangeInterval - shapeTimer
            ));
        }

        public void ChangeShape(ShapeType type)
        {
            // 回收当前形状
            if (currentShape != null)
            {
                shapeFactory.ReturnShape(currentShape);
            }

            // 创建新形状
            currentShape = shapeFactory.GetShape(type);
            if (currentShape != null)
            {
                var randomPosition = centralAreaSystem.GetRandomPosition();
                currentShape.SetPosition(randomPosition);
            }
            shapeTimer = 0;

            // 触发事件
            manager.PublishEvent(MapEvents.ShapeChanged, new ShapeChangedEvent
            {
                Type = type,
                Position = currentShape?.GetState().Position ?? Vector3D.Zero
            });
        }

        public void HandleSkillHit(int skillId, Vector3D hitPoint)
        {
            currentShape?.HandleSkillHit(skillId, hitPoint);
        }

        private void TriggerShapeChange()
        {
            ShapeType newType = GetRandomEnabledShapeType();
            ChangeShape(newType);
        }

        private ShapeType GetRandomEnabledShapeType()
        {
            float totalChance = 0;
            foreach (var shapeConfig in config.ShapeConfigs)
            {
                if (shapeConfig.Enabled)
                {
                    totalChance += shapeConfig.SpawnChance;
                }
            }

            float random = MathUtils.GetRandomRange(0, totalChance);
            float currentChance = 0;

            foreach (var shapeConfig in config.ShapeConfigs)
            {
                if (shapeConfig.Enabled)
                {
                    currentChance += shapeConfig.SpawnChance;
                    if (random <= currentChance)
                    {
                        return shapeConfig.Type;
                    }
                }
            }

            return ShapeType.Circle;
        }

        public void Dispose()
        {
            if (currentShape != null)
            {
                shapeFactory.ReturnShape(currentShape);
                currentShape = null;
            }
            shapeFactory.Clear();
        }
    }
}
