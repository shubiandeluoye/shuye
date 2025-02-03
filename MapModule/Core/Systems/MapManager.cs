using System;
using System.Collections.Generic;
using MapModule.Core.Data;
using MapModule.Core.Events;
using MapModule.Core.Interfaces;
using MapModule.Core.States;

namespace MapModule.Core.Systems
{
public class MapManager : IMapManager, ITileInteractionService
    {
        private readonly MapConfig config;
        private readonly MapStateMachine stateMachine;
        private readonly List<IMapSystem> systems = new();
        private bool isInitialized;

        public MapManager(MapConfig config)
        {
            this.config = config;
            stateMachine = new MapStateMachine(this);
        }

        public void Initialize()
        {
            if (isInitialized) return;

            // 初始化状态机
            stateMachine.Initialize();

            // 初始化所有系统
            foreach (var system in systems)
            {
                system.Initialize(this);
            }

            isInitialized = true;

            MapEventSystem.Instance.Publish(MapEventType.MapInitialized, 
                new MapStateEventData(MapEventType.MapInitialized, "MapManager"));
        }

        public void AddSystem(IMapSystem system)
        {
            systems.Add(system);
        }

        public void Update(float deltaTime)
        {
            if (!isInitialized) return;

            stateMachine.Update(deltaTime);

            foreach (var system in systems)
            {
                system.Update(deltaTime);
            }
        }

        public void ChangeState(MapStateType stateType)
        {
            stateMachine.ChangeState(stateType);
        }

        public List<PlayerData> GetPlayersInArea(Vector3D center, float radius)
        {
            // TODO: 实现玩家区域检测
            return new List<PlayerData>();
        }

        // 实现ITileInteractionService接口
        public void ModifyTileState(Vector2Data tilePos, int newState)
        {
            // TODO: 实现图块状态修改逻辑
            MapEventSystem.Instance.Publish(MapEventType.TileStateChanged,
                new TileModifyEventData(tilePos, newState));
        }

        public TileAreaInfo GetTileAreaInfo(Vector2Data center, float radius)
        {
            // TODO: 实现区域图块信息获取
            return new TileAreaInfo {
                Center = center,
                Radius = radius,
                TileStates = Array.Empty<int>(),
                HasSpecialTerrain = false
            };
        }

        public void Dispose()
        {
            foreach (var system in systems)
            {
                system.Dispose();
            }

            MapEventSystem.Instance.Publish(MapEventType.MapShutdown, 
                new MapStateEventData(MapEventType.MapShutdown, "MapManager"));
        }
    }
}
