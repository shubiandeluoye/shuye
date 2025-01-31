using MapModule.Core.Data;
using MapModule.Core.Utils;

namespace MapModule.Core.Shapes
{
    public class RectangleShape : BaseShape
    {
        private float width;
        private float height;

        public RectangleShape(IMapManager manager) : base(manager)
        {
        }

        public override void Initialize(ShapeConfig config)
        {
            base.Initialize(config);
            width = config.Width;
            height = config.Height;
        }

        public override ShapeState GetState()
        {
            return new ShapeState
            {
                Position = position,
                Width = width,
                Height = height,
                Type = ShapeType.Rectangle,
                IsActive = isActive && !isPaused
            };
        }

        protected override void OnShapeHit(int skillId, Vector3D hitPoint)
        {
            if (isPaused) return;
            // 矩形被击中逻辑
        }

        protected override void ValidateConfig(ShapeConfig config)
        {
            if (config.Width <= 0 || config.Height <= 0)
            {
                throw new ArgumentException("Invalid rectangle dimensions");
            }
        }

        public override void Update(float deltaTime)
        {
            if (isPaused) return;
            // 矩形更新逻辑
        }
    }
}
