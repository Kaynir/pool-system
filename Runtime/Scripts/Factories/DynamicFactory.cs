using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.Pools
{
    public class DynamicFactory<T> : AbstractFactory<T> where T : Component
    {
        [SerializeField] private List<T> _startPrefabs = new List<T>();

        private Dictionary<T, IObjectPool<T>> _pools;

        public T TakeObject(T prefab)
        {
            CheckForObjectPool(prefab);
            return _pools[prefab].Take();
        }

        protected override void Init()
        {
            _pools = new Dictionary<T, IObjectPool<T>>();
            _startPrefabs.ForEach(prefab =>
            {
                CheckForObjectPool(prefab);
            });
        }

        private void CheckForObjectPool(T prefab)
        {
            if (_pools.ContainsKey(prefab)) return;

            _pools[prefab] = CreateObjectPool(prefab);
        }
    }
}