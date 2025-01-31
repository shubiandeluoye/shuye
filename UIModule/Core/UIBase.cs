using UnityEngine;
using UnityEngine.UI;
using System;
using UIModule.Core.Types;
using UIModule.Core.Models;

namespace UIModule.Core
{
    /// <summary>
    /// UI基础组件接口
    /// </summary>
    public interface IUIComponent
    {
        void Show();
        void Hide();
        void UpdateUI();
    }

    /// <summary>
    /// UI基类，所有UI组件的基础
    /// </summary>
    public abstract class UIBase : MonoBehaviour, IUIComponent
    {
        protected CanvasGroup canvasGroup;
        protected RectTransform rectTransform;
        protected bool isInitialized;
        protected bool isVisible;

        // 动画相关
        protected virtual float showDuration => 0.3f;
        protected virtual float hideDuration => 0.2f;

        // UI数据相关
        protected UIModel model;
        protected UITransitionData transitionData;
        protected Action<string> onComplete;

        public string Name => model?.Name;
        public UILayer Layer => model?.Layer ?? UILayer.None;
        public UIState State => model?.State ?? UIState.None;
        public bool IsPopup => model?.IsPopup ?? false;
        public bool KeepAlive => model?.KeepAlive ?? false;

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
        }

        public virtual void Init(UIData data)
        {
            model = new UIModel(data);
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (isInitialized) return;
            
            if (canvasGroup == null)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
                
            isInitialized = true;
            OnInitialized();
        }

        protected virtual void OnInitialized() { }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show(Action<string> callback = null)
        {
            if (!isInitialized) Initialize();
            
            onComplete = callback;
            isVisible = true;
            
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }

            gameObject.SetActive(true);
            model?.SetState(UIState.Showing);
            OnShow();
        }

        public virtual void Hide(bool force = false)
        {
            isVisible = false;
            
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

            model?.SetState(UIState.Hiding);
            OnHide();
            
            if (force || !KeepAlive)
            {
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnShow() { }
        protected virtual void OnHide() { }

        public virtual void SetData(string key, object value)
        {
            model?.SetData(key, value);
        }

        public virtual T GetData<T>(string key)
        {
            return model != null ? model.GetData<T>(key) : default;
        }

        public virtual void OnStateChanged(UIState oldState, UIState newState)
        {
            model?.SetState(newState);
        }

        public abstract void UpdateUI();

        public virtual void Dispose()
        {
            model?.ClearData();
            model = null;
            onComplete = null;
        }

        protected virtual void OnDestroy()
        {
            Dispose();
        }
    }
} 