using MapModule.Core.Data;
using MapModule.Core.Utils;

namespace MapModule.Core.Shapes
{
    public interface IShape
    {
        void Initialize(ShapeConfig config);
        void HandleSkillHit(int skillId, Vector3D hitPoint);
        void Update(float deltaTime);
        ShapeState GetState();
        ShapeType GetShapeType();
        void SetPosition(Vector3D position);
        void Reset();
        bool IsActive();
        
        // 新增暂停/恢复方法
        void Pause();
        void Resume();
    }
}
