using System;
using System.Collections.Generic;

namespace Core.Pool
{
    public class ObjectPool<T> where T : class, IPoolable
    {
        private readonly Func<T> createFunc;
        private readonly Stack<T> pool;
        private readonly HashSet<T> activeObjects;
        private readonly int maxSize;

        public ObjectPool(Func<T> createFunc, int initialSize = 10, int maxSize = 100)
        {
            this.createFunc = createFunc;
            this.maxSize = maxSize;
            pool = new Stack<T>(initialSize);
            activeObjects = new HashSet<T>();

            for (int i = 0; i < initialSize; i++)
            {
                pool.Push(createFunc());
            }
        }

        public T Spawn()
        {
            T obj = pool.Count > 0 ? pool.Pop() : createFunc();
            activeObjects.Add(obj);
            obj.OnSpawn();
            return obj;
        }

        public void Despawn(T obj)
        {
            if (!activeObjects.Contains(obj)) return;

            obj.OnDespawn();
            activeObjects.Remove(obj);

            if (pool.Count < maxSize)
            {
                pool.Push(obj);
            }
        }

        public void Clear()
        {
            pool.Clear();
            activeObjects.Clear();
        }

        public int ActiveCount => activeObjects.Count;
        public int PooledCount => pool.Count;
    }
} 