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
            if (item.Active)
                ActivateObject(item);
            else
                DeactivateObject(item);
        }

        public void ActivateObject(T item)
        {
            if (inactivePool.Contains(item))
            {
                inactivePool.Remove(item);
            }
            
            item.Active = true;
            activePool.Add(item);
            
            item.OnEnableObject();
        }

        public void DeactivateObject(T item)
        {
            if (activePool.Contains(item))
            {
                activePool.Remove(item);
            }
            
            item.Active = false;
            inactivePool.Add(item);
            
            item.OnDisableObject();
        }

        public T[] GetActiveObjects()
        {
            return activePool.ToArray();
        }
        
        public T[] GetInActiveObjects()
        {
            return inactivePool.ToArray();
        }

        public T[] GetActiveObjects(System.Type objectType)
        {
            return GetObjects(objectType, activePool, true);
        }
        
        public T[] GetInActiveObjects(System.Type objectType)
        {
            return GetObjects(objectType, inactivePool, false);
        }
        
        private T[] GetObjects(System.Type objectType, List<T> pool, bool desiredState)
        {
            if (activePool.Count <= 0)
                return Array.Empty<T>();
            
            List<T> result = new List<T>();
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].Active == desiredState && objectType.IsInstanceOfType(pool[i]))
                    result.Add(pool[i]);
            }
            
            return result.ToArray();
        }
        
        public bool GetActiveObject(System.Type objectType, out T result)
        {
            return GetObject(objectType, out result, activePool, true);
        }
        
        public bool GetInActiveObject(System.Type objectType, out T result)
        {
            return GetObject(objectType, out result, inactivePool, false);
        }
        
        private static bool GetObject(System.Type objectType, out T result, List<T> pool, bool desiredState)
        {
            result = default(T);
            if (pool.Count <= 0)
                return false;
            
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].Active == desiredState && objectType.IsInstanceOfType(pool[i]))
                {
                    result = pool[i];
                    return true;
                }
            }
            
            return false;
        }
    }
}