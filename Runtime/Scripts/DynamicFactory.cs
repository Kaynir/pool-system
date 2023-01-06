using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.Pools
{
    public class DynamicFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField, Min(0)] protected int _startPoolSize = 0;

        protected Dictionary<T, MonoPool<T>> _pools;

        private void Awake()
        {
            Initialize();
        }

        public T TakeObject(T prefab)
        {
            if (!_pools.ContainsKey(prefab))
            {
                _pools[prefab] = CreatePool(prefab);
            }

            return _pools[prefab].Take();
        }

        protected virtual void Initialize()
        {
            _pools = new Dictionary<T, MonoPool<T>>();
        }

        protected virtual MonoPool<T> CreatePool(T prefab)
        {
            return new MonoPool<T>(prefab,
                                   OnObjectTaken,
                                   OnObjectReleased,
                                   _startPoolSize);
        }

        protected virtual void OnObjectTaken(T obj)
        {
            obj.transform.SetParent(transform);
        }

        protected virtual void OnObjectReleased(T obj)
        {
            obj.transform.SetParent(transform);
        }
    }
}