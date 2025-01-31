using UIModule.Core.Types;

namespace UIModule.Core.Events
{
    public class UIShowEvent
    {
        public string UIName { get; set; }
        public UILayer Layer { get; set; }
        public object[] Parameters { get; set; }
    }

    public class UIHideEvent
    {
        public string UIName { get; set; }
        public bool Force { get; set; }
        public string Reason { get; set; }
    }

    public class UIStateChangeEvent
    {
        public string UIName { get; set; }
        public UIState OldState { get; set; }
        public UIState NewState { get; set; }
        public float StateChangeTime { get; set; }
    }

    public class UIDataUpdateEvent
    {
        public string UIName { get; set; }
        public string DataKey { get; set; }
        public object Data { get; set; }
    }
} 