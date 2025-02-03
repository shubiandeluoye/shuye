namespace Core.Interfaces
{
    public interface ITileInteractionService
    {
        /// <summary>
        /// 修改指定位置的图块状态
        /// </summary>
        /// <param name="tilePos">图块坐标（基于地图坐标系）</param>
        /// <param name="newState">新的图块状态</param>
        void ModifyTileState(Vector2Data tilePos, int newState);

        /// <summary>
        /// 获取指定区域的图块状态
        /// </summary>
        /// <param name="center">中心坐标</param>
        /// <param name="radius">作用半径</param>
        TileAreaInfo GetTileAreaInfo(Vector2Data center, float radius);
    }

    public struct TileAreaInfo
    {
        public Vector2Data Center;
        public float Radius;
        public int[] TileStates;
        public bool HasSpecialTerrain;
    }
}
