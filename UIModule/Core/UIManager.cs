using System;
using System.Collections.Generic;
using UIModule.Core.Types;
using UIModule.Core.Events;

namespace UIModule.Core
{
    public class UIManager
    {
        private static UIManager instance;
        public static UIManager Instance => instance ??= new UIManager();

        private Dictionary<string, UIBase> uiCache = new Dictionary<string, UIBase>();
        private Dictionary<UILayer, List<UIBase>> layerUIs = new Dictionary<UILayer, List<UIBase>>();
        private Stack<UIBase> uiStack = new Stack<UIBase>();

        private UIManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (UILayer layer in Enum.GetValues(typeof(UILayer)))
            {
                layerUIs[layer] = new List<UIBase>();
            }
        }

        public void Show(string uiName, object[] parameters = null)
        {
            if (uiCache.TryGetValue(uiName, out var ui))
            {
                ShowUI(ui, parameters);
            }
            else
            {
                // TODO: 通过工厂创建UI
            }
        }

        public void Hide(string uiName, bool force = false)
        {
            if (uiCache.TryGetValue(uiName, out var ui))
            {
                HideUI(ui, force);
            }
        }

        private void ShowUI(UIBase ui, object[] parameters = null)
        {
            // 处理UI层级
            if (!layerUIs[ui.Layer].Contains(ui))
            {
                layerUIs[ui.Layer].Add(ui);
            }

            // 处理弹出栈
            if (ui.IsPopup)
            {
                uiStack.Push(ui);
            }

            // 显示UI
            ui.Show();
        }

        private void HideUI(UIBase ui, bool force)
        {
            // 处理UI层级
            layerUIs[ui.Layer].Remove(ui);

            // 处理弹出栈
            if (ui.IsPopup && uiStack.Count > 0 && uiStack.Peek() == ui)
            {
                uiStack.Pop();
            }

            // 隐藏UI
            ui.Hide(force);

            // 如果不保持存活，则销毁
            if (!ui.KeepAlive)
            {
                DestroyUI(ui);
            }
        }

        private void DestroyUI(UIBase ui)
        {
            ui.Dispose();
            uiCache.Remove(ui.Name);
        }

        public UIBase GetUI(string uiName)
        {
            uiCache.TryGetValue(uiName, out var ui);
            return ui;
        }

        public void Clear(bool force = false)
        {
            foreach (var ui in uiCache.Values)
            {
                if (force || !ui.KeepAlive)
                {
                    DestroyUI(ui);
                }
            }
            uiStack.Clear();
        }
    }
} 