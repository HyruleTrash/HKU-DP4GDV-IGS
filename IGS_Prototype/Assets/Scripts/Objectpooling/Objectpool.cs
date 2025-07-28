using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LucasCustomClasses
{
    public class ObjectPool<T> where T : IPoolable
    {
        private List<T> activePool = new List<T>();
        private List<T> inactivePool = new List<T>();

        public void AddToPool(T item)
        {
            if (activePool.Contains(item) || inactivePool.Contains(item))
                return;
            if (item.active)
                activePool.Add(item);
            else
                inactivePool.Add(item);
        }

        public void ActivateObject(T item)
        {
            item.active = true;
            item.OnEnableObject();
            
            if (!activePool.Contains(item))
                activePool.Add(item);
            if (inactivePool.Contains(item))
                inactivePool.Remove(item);
        }

        public void DeactivateObject(T item)
        {
            item.active = false;
            item.OnDisableObject();
            
            if (activePool.Contains(item))
                activePool.Remove(item);
            if (!inactivePool.Contains(item))
                inactivePool.Add(item);
        }

        public T[] GetActiveObjects()
        {
            return activePool.ToArray();
        }

        public T[] GetActiveObjects(System.Type objectType)
        {
            if (activePool.Count <= 0)
                return Array.Empty<T>();
            
            List<T> result = new List<T>();
            for (int i = 0; i < activePool.Count; i++)
            {
                if (activePool[i].active && activePool[i].GetType() == objectType)
                    result.Add(activePool[i]);
            }
            
            return result.ToArray();
        }
        
        public bool GetActiveObject(System.Type objectType, out T result)
        {
            result = default(T);
            if (activePool.Count <= 0)
                return false;
            
            for (int i = 0; i < activePool.Count; i++)
            {
                if (activePool[i].active && activePool[i].GetType() == objectType)
                {
                    result = activePool[i];
                    return true;
                }
            }
            
            return false;
        }
    }
}