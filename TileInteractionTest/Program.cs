using System;
using Core.Interfaces;
using Core.Types;
using MapModule.Core.Systems;
using Core.Utils;

namespace TileInteractionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 初始化地图管理系统
            var mapManager = new MapManager();
            
            // 注册地图服务
            ServiceLocator.Register<ITileInteractionService>(mapManager);
            
            // 获取地图服务实例
            var tileService = ServiceLocator.Get<ITileInteractionService>();
            
            // 测试修改地图块状态
            var testPosition = new Vector2Data { X = 10, Y = 15 };
            tileService.ModifyTileState(testPosition, 2);
            
            // 测试获取区域信息
            var areaInfo = tileService.GetTileAreaInfo(testPosition, 5f);
            
            Console.WriteLine($"中心坐标: ({areaInfo.Center.X}, {areaInfo.Center.Y})");
            Console.WriteLine($"区域半径: {areaInfo.Radius}");
            Console.WriteLine($"包含图块数量: {areaInfo.TileStates?.Length ?? 0}");
        }
    }
}
