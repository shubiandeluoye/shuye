using SkillModule.Core.Types;

namespace SkillModule.Core.Utils
{
    public static class SkillUtils
    {
        /// <summary>
        /// 获取当前时间（秒）
        /// </summary>
        public static float GetCurrentTime()
        {
            return System.DateTime.Now.Ticks / 10000000f;
        }

        /// <summary>
        /// 计算两点之间的距离
        /// </summary>
        public static float CalculateDistance(Vector3Data a, Vector3Data b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            float dz = a.Z - b.Z;
            return System.MathF.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// 规范化角度到 0-360 度
        /// </summary>
        public static float NormalizeAngle(float angle)
        {
            while (angle < 0) angle += 360;
            while (angle >= 360) angle -= 360;
            return angle;
        }

        /// <summary>
        /// 检查值是否在范围内
        /// </summary>
        public static bool IsInRange(float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// 生成唯一ID
        /// </summary>
        private static int idCounter = 0;
        public static int GenerateUniqueId()
        {
            return System.Threading.Interlocked.Increment(ref idCounter);
        }

        /// <summary>
        /// 获取方向向量
        /// </summary>
        public static Vector3Data GetDirectionFromAngle(float angle)
        {
            float rad = angle * System.MathF.PI / 180f;
            return new Vector3Data(
                System.MathF.Cos(rad),
                0,
                System.MathF.Sin(rad)
            );
        }

        /// <summary>
        /// 检查目标是否在扇形区域内
        /// </summary>
        public static bool IsInSector(Vector3Data center, Vector3Data target, Vector3Data forward, float angle, float radius)
        {
            if (CalculateDistance(center, target) > radius) return false;

            var dirToTarget = new Vector3Data(
                target.X - center.X,
                target.Y - center.Y,
                target.Z - center.Z
            );

            // 计算角度
            float dot = forward.X * dirToTarget.X + forward.Z * dirToTarget.Z;
            float targetAngle = System.MathF.Acos(dot) * 180f / System.MathF.PI;

            return targetAngle <= angle * 0.5f;
        }
    }
} 