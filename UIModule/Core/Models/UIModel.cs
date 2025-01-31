using System.Collections.Generic;
using UIModule.Core.Types;

namespace UIModule.Core.Models
{
    public class UIModel
    {
        public string Name { get; private set; }
        public UILayer Layer { get; private set; }
        public UIState State { get; private set; }
        public bool IsPopup { get; private set; }
        public bool KeepAlive { get; private set; }

        private Dictionary<string, object> dataStore = new Dictionary<string, object>();

        public UIModel(UIData data)
        {
            Name = data.Name;
            Layer = data.Layer;
            IsPopup = data.IsPopup;
            KeepAlive = data.KeepAlive;
            State = UIState.None;
        }

        public void SetState(UIState newState)
        {
            State = newState;
        }

        public void SetData(string key, object value)
        {
            if (value == null)
            {
                dataStore.Remove(key);
            }
            else
            {
                dataStore[key] = value;
            }
        }

        public T GetData<T>(string key)
        {
            if (dataStore.TryGetValue(key, out var value) && value is T typedValue)
            {
                return typedValue;
            }
            return default;
        }

        public bool HasData(string key)
        {
            return dataStore.ContainsKey(key);
        }

        public void ClearData()
        {
            dataStore.Clear();
        }
    }
} 