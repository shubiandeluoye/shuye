# UI模块架构说明

## 1. 模块结构
```
UIModule/
├── Core/                           # 核心层
│   ├── Types/                      # 类型定义
│   │   └── UIDataTypes.cs         # UI数据类型（层级、状态等）
│   ├── Events/                     # 事件系统
│   │   └── UIEvents.cs            # UI相关事件定义
│   ├── Models/                     # 数据模型
│   │   └── UIModel.cs             # UI数据模型
│   ├── UIBase.cs                   # UI基类
│   └── UIManager.cs               # UI管理器
└── Unity/                         # Unity实现层（待实现）
    ├── Components/                # UI组件
    ├── Configs/                   # UI配置
    └── Runtime/                   # 运行时
```

## 2. 核心概念

### 2.1 UI层级 (UILayer)
- Background: 背景层
- Normal: 普通层
- Pop: 弹出层
- Top: 顶层
- System: 系统层

### 2.2 UI状态 (UIState)
- None: 初始状态
- Loading: 加载中
- Ready: 就绪
- Showing: 显示中
- Hiding: 隐藏中
- Hidden: 已隐藏

### 2.3 UI数据 (UIData)
- Name: UI名称
- Layer: UI层级
- IsPopup: 是否为弹窗
- KeepAlive: 是否保持存活
- Parameters: 参数数组

## 3. 核心组件

### 3.1 UIBase
基础UI类，提供：
- 生命周期管理（初始化、显示、隐藏、销毁）
- 数据管理（存取数据）
- 状态管理（状态变更）
- 动画支持（显示/隐藏动画）

### 3.2 UIManager
UI管理器，负责：
- UI实例的创建和销毁
- UI层级管理
- UI栈管理（弹窗）
- UI状态同步

### 3.3 UIModel
UI数据模型，处理：
- UI基础数据
- UI状态维护
- 数据存储

## 4. 事件系统

### 4.1 UI事件
- UIShowEvent: UI显示事件
- UIHideEvent: UI隐藏事件
- UIStateChangeEvent: 状态变更事件
- UIDataUpdateEvent: 数据更新事件

## 5. 使用示例

### 5.1 创建UI
```csharp
// 1. 创建UI数据
var uiData = new UIData 
{
    Name = "MainMenu",
    Layer = UILayer.Normal,
    IsPopup = false,
    KeepAlive = true
};

// 2. 显示UI
UIManager.Instance.Show("MainMenu");
```

### 5.2 自定义UI
```csharp
public class MainMenuUI : UIBase
{
    protected override void OnInitialized()
    {
        // 初始化逻辑
    }

    protected override void OnShow()
    {
        // 显示逻辑
    }

    public override void UpdateUI()
    {
        // 更新UI显示
    }
}
```

## 6. 注意事项

### 6.1 生命周期
1. 初始化顺序：
   - Awake: 组件获取
   - Init: 数据初始化
   - Initialize: 内部初始化
   - OnInitialized: 子类初始化

2. 显示流程：
   - Show: 显示调用
   - 状态更新
   - OnShow: 显示回调

3. 隐藏流程：
   - Hide: 隐藏调用
   - 状态更新
   - OnHide: 隐藏回调

### 6.2 最佳实践
1. UI预制体规范：
   - 必须包含RectTransform
   - 建议添加CanvasGroup
   - 合理设置层级

2. 数据管理：
   - 使用SetData/GetData管理数据
   - 及时清理不需要的数据
   - 注意数据更新时机

3. 性能优化：
   - 合理使用KeepAlive
   - 注意UI的显示/隐藏开销
   - 避免频繁创建销毁 