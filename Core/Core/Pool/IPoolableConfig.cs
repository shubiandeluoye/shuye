namespace Core.Pool
{
    public interface IPoolableConfig
    {
        float LifeTime { get; }
        bool AutoRecycle { get; }
    }
} 