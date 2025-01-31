using Core.Types;

namespace MapModule.Core.Data
{
    public class MapConfig
    {
        public Vector2D CentralAreaSize { get; set; }
        public Vector2D ActiveAreaSize { get; set; }
        public float VerticalFloatRange { get; set; }
        public float ShapeChangeInterval { get; set; }
        public float TransitionDuration { get; set; }
        
        // 新增动画相关配置
        public float BlockAnimationDuration { get; set; } = 0.2f;
        public float WavePropagationSpeed { get; set; } = 5f;
        public float MaxBlockHeight { get; set; } = 1.5f;
        public float ResetAnimationDuration { get; set; } = 2f;
        
        public ShapeTypeConfig[] ShapeConfigs { get; set; }
    }

    public class ShapeTypeConfig
    {
        public ShapeType Type { get; set; }
        public Vector2D Size { get; set; }
        public float SpawnChance { get; set; } = 1f;
        public bool Enabled { get; set; } = true;
    }
}
