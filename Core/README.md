# Core Module (核心模块)

## 目录结构
```
Core/
├── Core/                           # 核心层（纯C#）
│   ├── Events/                     # 事件系统
│   │   ├── EventTypes.cs          # 事件类型定义
│   │   └── EventData.cs           # 事件数据结构
│   ├── FSM/                       # 状态机系统
│   │   ├── IState.cs             # 状态接口
│   │   ├── StateBase.cs          # 状态基类
│   │   └── StateMachine.cs       # 状态机实现
│   ├── Pool/                      # 对象池系统
│   │   ├── IPoolable.cs          # 对象池接口
│   │   └── ObjectPool.cs         # 对象池实现
│   └── Utils/                     # 工具类
│       ├── Singleton.cs          # 单例基类
│       └── Timer.cs              # 计时器实现
└── Unity/                         # Unity实现层
    ├── Components/               # Unity组件
    │   ├── UnityTimer.cs        # Unity计时器
    │   ├── UnityPool.cs         # Unity对象池
    │   └── UnityStateMachine.cs # Unity状态机
    └── Utils/                    # Unity工具
        └── UnitySingleton.cs    # Unity单例基类
```

## 核心功能

### 1. 事件系统
- 事件类型定义
- 事件数据结构
- 事件分发机制

### 2. 状态机系统
- 状态接口定义
- 状态基类实现
- 状态机管理

### 3. 对象池系统
- 对象池接口
- 通用对象池实现
- Unity对象池封装

### 4. 工具系统
- 单例模式实现
- 计时器系统
- Unity工具封装

## 使用方式

### 1. 事件系统
```csharp
// 订阅事件
EventManager.Instance.Subscribe<GameStateChangedEvent>(OnGameStateChanged);

// 发布事件
EventManager.Instance.Publish(new GameStateChangedEvent { NewState = GameState.Playing });
```

### 2. 状态机
```csharp
// 创建状态机
var stateMachine = new StateMachine();
stateMachine.AddState("Idle", new IdleState(stateMachine));
stateMachine.SwitchState("Idle");
```

### 3. 对象池
```csharp
// Core层使用
var pool = new ObjectPool<MyObject>(() => new MyObject());
var obj = pool.Spawn();
pool.Despawn(obj);

// Unity层使用
var prefabInstance = UnityPool.Instance.Spawn(prefab);
UnityPool.Instance.Despawn(prefabInstance);
```

### 4. 单例
```csharp
// Core层单例
public class GameManager : Singleton<GameManager>
{
    // 实现
}

// Unity层单例
public class GameController : UnitySingleton<GameController>
{
    // 实现
}
```

## 注意事项

1. **Core层设计原则**
   - 不依赖Unity
   - 纯C#实现
   - 接口分离
   - 线程安全

2. **Unity层实现**
   - MonoBehaviour封装
   - 场景生命周期
   - 预制体管理
   - 组件交互

3. **性能考虑**
   - 对象池复用
   - 事件系统优化
   - 内存管理
   - GC优化

4. **扩展性**
   - 接口设计
   - 基类复用
   - 依赖注入
   - 模块解耦

## 依赖说明
- 不依赖其他模块
- 被其他所有模块依赖
- 提供基础设施 