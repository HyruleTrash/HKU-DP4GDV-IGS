using UnityEngine;

namespace LucasCustomClasses
{
    public class Timer
    {
        private double _currentTime = 0;
        private float _maxTime = 0;
        public System.Action onEnd;
        public System.Action<double> onPlaying;

        public Timer(float maxTime, System.Action onEnd)
        {
            this._maxTime = maxTime;
            this.onEnd = onEnd;
        }
        
        public void Reset()
        {
            _currentTime = 0;
        }

        public void Update(double dt)
        {
            _currentTime += dt;
            onPlaying?.Invoke(_currentTime);
            CheckIfEndIsReached();
        }

        public void CheckIfEndIsReached()
        {
            if (_currentTime >= _maxTime)
            {
                onEnd?.Invoke();
                Reset();
            }
        }

        public double GetCurrentTime()
        {
            return _currentTime;
        }
    }
}