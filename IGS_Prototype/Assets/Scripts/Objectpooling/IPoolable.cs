using System;

namespace LucasCustomClasses
{
    public interface IPoolable
    {
        public bool active { get; set; }
        
        void OnEnableObject();
        void OnDisableObject();
        void DoDie();
    }
}