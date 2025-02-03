using System;
using MapModule.Core.Data;
using MapModule.Core.Events;
using MapModule.Core.Utils;

namespace MapModule.Core.Systems
{
    public class CentralAreaSystem : IMapSystem
    {
        private readonly MapConfig config;
        private IMapManager manager;
        private Vector3D centerPosition;
        private float radius;

        public CentralAreaSystem(MapConfig config)
        {
            this.config = config;
            centerPosition = config.CenterPosition;
            radius = config.CentralAreaRadius;
        }

        public void Initialize(IMapManager manager)
        {
            this.manager = manager;
            
            MapEventSystem.Instance.Publish(MapEventType.MapSystemInitialized, 
                new MapStateEventData(MapEventType.MapSystemInitialized, "CentralAreaSystem"));
        }

        public Vector3D GetRandomPosition()
        {
            var randomPos = MathUtils.GetRandomPointInCircle(centerPosition.ToVector2D(), radius);
            return new Vector3D(randomPos.X, randomPos.Y, 0);
        }

        public void Update(float deltaTime)
        {
            // 检查玩家进入/离开中央区域
            var players = manager.GetPlayersInArea(centerPosition, radius);
            foreach (var player in players)
            {
                MapEventSystem.Instance.Publish(MapEventType.PlayerEnterArea, 
                    new AreaEventData(MapEventType.PlayerEnterArea, "CentralArea", player.Id));
            }
        }

        public void Dispose()
        {
            MapEventSystem.Instance.Publish(MapEventType.MapSystemShutdown, 
                new MapStateEventData(MapEventType.MapSystemShutdown, "CentralAreaSystem"));
        }
    }
}
