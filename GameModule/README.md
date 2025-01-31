# Game Module (游戏模块)

## 模块职责
- 游戏流程控制
- 玩家管理
- 游戏模式管理
- 分数系统
- 游戏事件分发

## 文件结构
```
GameModule/
├── Core/                           # 核心层（纯C#）
│   ├── Game/
│   │   ├── GameSystem.cs          # 游戏系统核心（流程控制、玩家管理）
│   │   ├── GameModeSystem.cs      # 游戏模式系统（模式规则、玩家数量）
│   │   ├── ScoreSystem.cs         # 分数系统（得分、连杀）
│   │   ├── GameRuleSystem.cs      # 规则系统（边界、伤害）
│   │   ├── GameEventSystem.cs     # 事件系统（游戏事件分发）
│   │   └── IGameHandler.cs        # 游戏处理接口（Core到Unity的桥接）
│   ├── Data/
│   │   ├── GameConfig.cs          # 游戏配置（基础配置、规则配置）
│   │   ├── GameModeConfig.cs      # 模式配置
│   │   ├── PlayerData.cs          # 玩家数据（生命值、分数、状态）
│   │   └── Vector2Data.cs         # 2D向量数据
│   └── Events/
│       └── GameEventData.cs       # 事件数据定义
└── Unity/                         # Unity实现层（不在此模块中实现）
```

## 使用方式

### 1. 初始化游戏系统
```csharp
// 创建配置
var config = new GameConfig 
{
    gameTime = 300f,
    maxPlayers = 4,
    minPlayers = 2
};

// 创建游戏系统
var gameSystem = new GameSystem(config, gameHandler);
```

### 2. 玩家管理
```csharp
// 添加玩家
gameSystem.AddPlayer(playerId);

// 移除玩家
gameSystem.RemovePlayer(playerId);
```

### 3. 游戏流程控制
```csharp
// 开始游戏
gameSystem.StartGame();

// 更新游戏
gameSystem.Update(Time.deltaTime);

// 处理伤害
gameSystem.HandleDamage(playerId, damage, DamageType.Normal);

// 处理击杀
gameSystem.HandleKill(killerId, victimId);
```

## 回调系统
通过IGameHandler接口接收游戏事件：
```csharp
public interface IGameHandler
{
    // 游戏状态回调
    void OnGameStateChanged(GameState newState);
    
    // 玩家相关回调
    void OnPlayerJoined(Guid playerId);
    void OnPlayerLeft(Guid playerId);
    void OnPlayerStateChanged(Guid playerId, PlayerState newState);
    void OnPlayerDamaged(Guid playerId, float damage, DamageType damageType);
    void OnPlayerDied(Guid playerId);
    
    // 游戏进程回调
    void OnGameStarted(GameMode mode, int playerCount);
    void OnGameOver(Guid winnerId, Dictionary<Guid, int> finalScores);
    
    // 分数相关回调
    void OnScoreUpdated(Guid playerId, int newScore, int killStreak);
}
```

## 注意事项

1. **核心设计原则**
   - Core层完全不依赖Unity
   - 所有表现效果由Unity层处理
   - 通过IGameHandler接口通信
   - 配置数据在Unity层设置

2. **游戏模式**
   - Normal: 标准模式
   - Practice: 练习模式（允许重生）
   - OneVsOne: 1v1对战
   - TwoVsTwo: 2v2对战

3. **分数系统**
   - 击杀得分：100分
   - 三连杀：+50分
   - 五连杀：+100分
   - 七连杀：+200分
   - 十连杀：+500分

4. **游戏状态**
   - None: 未开始
   - WaitingForPlayers: 等待玩家
   - Playing: 游戏中
   - GameOver: 游戏结束

5. **玩家状态**
   - None: 初始状态
   - Ready: 准备就绪
   - Playing: 游戏中
   - Dead: 已死亡
   - Spectating: 观战中

## 依赖关系
- Core.EventSystem
- Core.Network (可选)