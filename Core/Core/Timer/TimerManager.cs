using System;
using System.Collections.Generic;

namespace Core.Timer
{
    public class TimerManager
    {
        private List<Timer> activeTimers = new List<Timer>();
        private int nextTimerId = 1;

        public int AddTimer(float duration, Action callback)
        {
            var timer = new Timer(nextTimerId++, duration, callback);
            activeTimers.Add(timer);
            return timer.Id;
        }

        public void RemoveTimer(int timerId)
        {
            for (int i = activeTimers.Count - 1; i >= 0; i--)
            {
                if (activeTimers[i].Id == timerId)
                {
                    activeTimers[i].Stop();
                    break;
                }
            }
        }

        public void Update(float deltaTime)
        {
            for (int i = activeTimers.Count - 1; i >= 0; i--)
            {
                var timer = activeTimers[i];
                if (!timer.IsActive)
                {
                    activeTimers.RemoveAt(i);
                    continue;
                }

                timer.Update(deltaTime);
            }
        }
    }
} 