namespace Core.Utils
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static readonly object _lock = new object();
        private static volatile T _instance;
        private static bool _isInitialized;
        private static bool _isDestroyed;

        public static T Instance
        {
            get
            {
                if (_isDestroyed)
                {
                    Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed. Returning null.");
                    return null;
                }

                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                            _isInitialized = true;
                        }
                    }
                }

                return _instance;
            }
        }

        protected Singleton()
        {
            if (_instance != null)
            {
                throw new System.Exception($"[Singleton] Cannot create multiple instances of singleton class {typeof(T)}");
            }

            Initialize();
        }

        protected virtual void Initialize()
        {
            // 子类可以重写此方法进行初始化
        }

        public static bool IsInitialized
        {
            get
            {
                if (_isDestroyed) return false;
                return _isInitialized;
            }
        }

        protected virtual void OnDestroy()
        {
            _isDestroyed = true;
            _instance = null;
            _isInitialized = false;
        }

        public static void DestroySingleton()
        {
            if (_instance != null)
            {
                (_instance as Singleton<T>)?.OnDestroy();
            }
        }
    }
} 
 