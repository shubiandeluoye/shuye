namespace UIModule.Core.Types
{
    public enum UILayer
    {
        None = 0,
        Background = 1,    // 背景层
        Normal = 2,        // 普通层
        Pop = 3,          // 弹出层
        Top = 4,          // 顶层
        System = 5         // 系统层
    }

    public enum UIState
    {
        None = 0,
        Loading = 1,      // 加载中
        Ready = 2,        // 就绪
        Showing = 3,      // 显示中
        Hiding = 4,       // 隐藏中
        Hidden = 5        // 已隐藏
    }

    public struct UIData
    {
        public string Name { get; set; }
        public UILayer Layer { get; set; }
        public bool IsPopup { get; set; }
        public bool KeepAlive { get; set; }
        public object[] Parameters { get; set; }
    }

    public struct UITransitionData
    {
        public float Duration { get; set; }
        public bool FadeIn { get; set; }
        public bool FadeOut { get; set; }
        public float Delay { get; set; }
    }
} 