# Core Module (核心模块)

## 目录结构
```
Core/
├── Core/                           # 核心层（纯C#）
│   ├── Events/                     # 事件系统
│   │   ├── EventBridge.cs         # 事件桥接器
│   │   ├── EventData.cs           # 事件数据结构
│   │   └── EventTypes.cs          # 事件类型定义
│   ├── FSM/                       # 状态机系统
│   │   ├── IState.cs             # 状态接口
│   │   ├── StateBase.cs          # 状态基类
│   │   └── StateMachine.cs       # 状态机实现
│   ├── Pool/                      # 对象池系统
│   │   ├── IPoolable.cs          # 可池化对象接口
│   │   ├── IPoolableConfig.cs    # 可池化对象配置接口
│   │   ├── ObjectPool.cs         # 对象池实现
│   │   └── PoolableStatus.cs     # 池化对象状态
│   ├── Timer/                     # 计时器系统
│   │   ├── Timer.cs              # 计时器实现
│   │   └── TimerManager.cs       # 计时器管理器
│   ├── Types/                     # 数据类型定义
│   │   └── CoreTypes.cs          # 核心数据类型
│   └── Utils/                     # 工具类
│       └── Singleton.cs          # 单例基类
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
- **事件桥接器**：`EventBridge.cs` 提供了事件的发布和订阅功能。
- **事件数据结构**：`EventData.cs` 定义了多种事件数据结构。
- **事件类型定义**：`EventTypes.cs` 定义了事件类型和中央区域状态。

### 2. 状态机系统
- **状态接口**：`IState.cs` 定义了状态接口。
- **状态基类**：`StateBase.cs` 提供了状态的基类实现。
- **状态机实现**：`StateMachine.cs` 实现了状态机的核心逻辑。

### 3. 对象池系统
- **可池化对象接口**：`IPoolable.cs` 定义了可池化对象的接口。
- **可池化对象配置接口**：`IPoolableConfig.cs` 定义了可池化对象的配置接口。
- **对象池实现**：`ObjectPool.cs` 实现了对象池的核心逻辑。
- **池化对象状态**：`PoolableStatus.cs` 定义了池化对象的状态。

### 4. 计时器系统
- **计时器实现**：`Timer.cs` 实现了简单的计时器功能。
- **计时器管理器**：`TimerManager.cs` 管理多个计时器。

### 5. 数据类型
- **核心数据类型**：`CoreTypes.cs` 定义了核心数据类型，包括 `GameState` 枚举和 `Vector3Data`、`QuaternionData` 等结构体。

### 6. 工具类
- **单例基类**：`Singleton.cs` 实现了单例模式。

## 使用方式

### 1. 单例模式
```csharp
// 使用Singleton基类创建单例
public class GameManager : Singleton<GameManager>
{
    protected override void Initialize()
    {
        // 初始化逻辑
    }
}

// 销毁单例
GameManager.DestroySingleton();
```

### 2. 数据类型
```csharp
// 使用Vector3Data
var position = new Vector3Data(1.0f, 2.0f, 3.0f);

// 使用GameState
GameState currentState = GameState.Playing;
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