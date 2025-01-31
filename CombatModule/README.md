# Combat Module (战斗模块)

## 目录结构
```
CombatModule/
├── Core/                           # 核心层（纯C#）
│   ├── Combat/
│   │   ├── CombatSystem.cs        # 战斗系统核心
│   │   ├── DamageCalculator.cs    # 伤害计算器
│   │   ├── StatusEffectSystem.cs  # 状态系统
│   │   └── IStatusEffectHandler.cs # 状态接口
│   ├── Data/
│   │   ├── CombatConfig.cs        # 配置数据
│   │   ├── DamageData.cs          # 伤害数据
│   │   └── StatusEffectData.cs    # 状态数据
│   └── Events/
│       └── CombatEventData.cs     # 事件定义
└── Unity/                         # Unity实现层
    ├── Components/
    │   ├── DamageableObject.cs    # 可受伤组件
    │   ├── StatusEffectView.cs    # 状态显示
    │   ├── StatusEffectHandler.cs # 状态处理
    │   └── DamagePopup.cs        # 伤害显示
    ├── Systems/
    │   ├── CombatSystemBehaviour.cs # 战斗管理器
    │   ├── CombatEffectManager.cs  # 特效管理
    │   ├── StatusEffectManager.cs  # 状态管理
    │   ├── DamagePopupManager.cs   # 伤害显示管理
    │   └── CombatSceneManager.cs   # 场景管理
    ├── Network/
    │   └── CombatNetworkSync.cs   # 网络同步
    └── Data/
        ├── UnityDamageConfig.cs   # Unity配置
        ├── UnityDamageData.cs     # Unity数据
        └── CombatPrefabConfig.cs  # 预制体配置
```

## 模块结构

### 1. Core层 (纯C#实现)
- 完全不依赖Unity，可独立测试

#### Combat/ - 战斗核心
- `CombatSystem.cs`: 战斗系统核心逻辑
- `DamageCalculator.cs`: 伤害计算器
- `StatusEffectSystem.cs`: 状态效果系统
- `IStatusEffectHandler.cs`: 状态效果处理接口

#### Data/ - 数据定义
- `CombatConfig.cs`: 战斗配置数据
- `DamageData.cs`: 伤害数据结构
- `StatusEffectData.cs`: 状态效果数据

#### Events/ - 事件定义
- `CombatEventData.cs`: 战斗相关事件

### 2. Unity层 (Unity实现)
- 处理所有与Unity相关的功能

#### Components/ - Unity组件
- `DamageableObject.cs`: 可受伤害物体组件
- `StatusEffectView.cs`: 状态效果显示组件
- `StatusEffectHandler.cs`: 状态效果处理组件
- `DamagePopup.cs`: 伤害数字显示组件

#### Systems/ - 系统管理
- `CombatSystemBehaviour.cs`: Unity战斗管理器
- `CombatEffectManager.cs`: 战斗特效管理器
- `StatusEffectManager.cs`: 状态效果管理器
- `DamagePopupManager.cs`: 伤害显示管理器
- `CombatSceneManager.cs`: 战斗场景管理器

#### Network/ - 网络同步
- `CombatNetworkSync.cs`: 战斗网络同步组件

#### Data/ - Unity配置
- `UnityDamageConfig.cs`: Unity伤害配置
- `UnityDamageData.cs`: Unity伤害数据
- `CombatPrefabConfig.cs`: 预制体配置

## 使用方式

### 1. Core层使用
```csharp
// 创建伤害数据
var damageData = new DamageData 
{
    BaseDamage = 10,
    DamageType = DamageType.Normal,
    CritChance = 0.1f
};

// 使用战斗系统
combatSystem.HandleDamage(attackerId, targetId, damageData);
```

### 2. Unity层使用
```csharp
// 添加可受伤害组件
public class Enemy : MonoBehaviour
{
    private void Start()
    {
        // 注册伤害回调
        CombatSystemBehaviour.Instance.RegisterDamageCallback(gameObject, OnDamageReceived);
    }

    private void OnDamageReceived(float damage)
    {
        // 处理伤害
    }
}
```

## 注意事项

1. **核心设计原则**
   - Core层完全不依赖Unity
   - 所有表现效果由Unity层处理
   - 通过事件系统通信
   - 配置数据在Unity层设置

2. **性能考虑**
   - 使用对象池管理特效
   - 优化事件派发
   - 减少GC分配

3. **网络同步**
   - 伤害计算在服务器执行
   - 客户端只处理表现
   - 使用RPC同步关键数据

4. **扩展性**
   - 可以方便添加新的伤害类型
   - 可以自定义状态效果
   - 可以扩展表现效果

## 依赖关系
- Core.EventSystem
- Core.Network
- Core.ObjectPool