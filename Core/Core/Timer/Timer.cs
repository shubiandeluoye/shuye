using System;

namespace Core.Timer
{
    public class Timer
    {
        public int Id { get; private set; }
        public float RemainingTime { get; private set; }
        public Action Callback { get; private set; }
        public bool IsActive { get; private set; }

        public Timer(int id, float duration, Action callback)
        {
            Id = id;
            RemainingTime = duration;
            Callback = callback;
            IsActive = true;
        }

        public void Update(float deltaTime)
        {
            if (!IsActive) return;

            RemainingTime -= deltaTime;
            if (RemainingTime <= 0)
            {
                Callback?.Invoke();
                IsActive = false;
            }
        }

        public void Stop()
        {
            IsActive = false;
        }
    }
} 