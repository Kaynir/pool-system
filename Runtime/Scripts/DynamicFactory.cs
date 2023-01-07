using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.Pools
{
    public class DynamicFactory<T> : BaseFactory<T> where T : MonoBehaviour
    {
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

        protected override void Initialize()
        {
            _pools = new Dictionary<T, MonoPool<T>>();
        }
    }
}