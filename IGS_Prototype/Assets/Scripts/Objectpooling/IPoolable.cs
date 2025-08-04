using System;

namespace LucasCustomClasses
{
    public interface IPoolable
    {
        public bool Active { get; set; }
        
        public void OnEnableObject();
        public void OnDisableObject();
        public void DoDie();
        public void Destroy();
    }
}