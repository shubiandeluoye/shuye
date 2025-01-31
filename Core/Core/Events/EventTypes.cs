namespace Core.Events
{
    public enum GameEventType
    {
        None,
        GameStateChanged,
        PlayerDamaged,
        GameEnd,
        GameStart,
        PlayerShoot,
        PlayerSpawned,
        PlayerDespawned,
        CentralAreaStateChanged,
        ShapeStateChanged
    }

    public enum CentralAreaState
    {
        Collecting,
        Charging,
        Firing
    }
} 