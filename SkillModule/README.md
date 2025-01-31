# 技能系统架构说明

## 1. 系统架构
```
SkillModule/
├── Core/                           # 纯C#核心层
│   ├── Types/                      # 数据类型定义
│   │   ├── Vector3Data.cs         # 3D向量数据
│   │   ├── SkillDataTypes.cs      # 技能相关类型
│   │   └── EffectData.cs          # 效果相关类型
│   ├── Events/                     # 事件系统
│   │   ├── EffectEvents.cs        # 效果事件
│   │   └── SkillEvents.cs         # 技能事件
│   ├── Skills/                     # 技能核心
│   │   ├── BaseSkill.cs           # 技能基类
│   │   ├── PassiveSkillBase.cs    # 被动技能
│   │   ├── SkillManager.cs        # 技能管理
│   │   └── SkillContext.cs        # 技能上下文
│   └── Utils/                      # 工具类
│       └── SkillUtils.cs          # 通用工具
└── Unity/                          # Unity实现层
    ├── Components/                 # Unity组件
    │   ├── Skills/                # 技能行为
    │   │   ├── SkillBehaviour.cs  # 基础行为
    │   │   ├── BoxSkillBehaviour.cs
    │   │   ├── BarrierSkillBehaviour.cs
    │   │   ├── HealSkillBehaviour.cs
    │   │   └── ShootSkillBehaviour.cs
    │   └── Effects/               # 效果行为
    │       ├── EffectBehaviour.cs # 效果基类
    │       ├── ProjectileEffect.cs
    │       ├── AreaEffect.cs
    │       ├── BuffEffect.cs
    │       ├── BarrierEffect.cs
    │       └── HealEffect.cs
    ├── Configs/                    # 配置文件
    │   ├── SkillConfigSO.cs       # 配置基类
    │   ├── BoxSkillConfigSO.cs
    │   ├── BarrierSkillConfigSO.cs
    │   ├── HealSkillConfigSO.cs
    │   └── ShootSkillConfigSO.cs
    └── Runtime/                    # 运行时
        ├── SkillRunner.cs         # 技能运行时
        ├── SkillFactory.cs        # 技能工厂
        ├── EffectManager.cs       # 效果管理器
        ├── EffectFactory.cs       # 效果工厂
        └── EffectPool.cs          # 效果对象池
```

## 2. 核心概念

### 2.1 技能系统
- **技能基类 (BaseSkill)**: 所有技能的基础实现
- **技能上下文 (SkillContext)**: 技能执行的环境数据
- **技能管理器 (SkillManager)**: 管理技能的生命周期
- **技能配置 (SkillConfigSO)**: 技能的配置数据

### 2.2 效果系统
- **效果基类 (EffectBehaviour)**: 所有效果的基础实现
- **效果数据 (EffectData)**: 效果的数据定义
- **效果管理器 (EffectManager)**: 管理效果的生命周期
- **效果池 (EffectPool)**: 优化效果对象的重用

## 3. 主要功能

### 3.1 技能类型
- 盒子技能 (BoxSkill)
- 屏障技能 (BarrierSkill)
- 治疗技能 (HealSkill)
- 射击技能 (ShootSkill)

### 3.2 效果类型
- 投射物效果 (ProjectileEffect)
- 区域效果 (AreaEffect)
- 增益效果 (BuffEffect)
- 屏障效果 (BarrierEffect)
- 治疗效果 (HealEffect)

## 4. 使用流程

1. **技能配置**
   - 创建技能配置ScriptableObject
   - 设置技能参数
   - 配置效果预制体

2. **技能使用**
   - 添加SkillBehaviour到游戏对象
   - 设置技能配置
   - 调用UseSkill()方法

3. **效果生成**
   - 通过EffectFactory创建效果
   - 使用EffectPool管理效果对象
   - EffectManager处理效果生命周期

## 5. 注意事项

1. **Core层设计原则**
   - 无Unity依赖
   - 纯C#实现
   - 接口分离

2. **Unity层实现**
   - 处理具体表现
   - 管理资源生命周期
   - 提供MonoBehaviour实现