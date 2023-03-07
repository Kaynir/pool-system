using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.Pools
{
    public class DynamicFactory<T> : FactoryBehaviour<T> where T : Component
    {
        [SerializeField] private List<PoolData> _startPoolDataList = new List<PoolData>();

        private Dictionary<T, IObjectPool<T>> _pools;

        public T TakeObject(T prefab)
        {
            if (_pools == null) Init();
            
            CheckForObjectPool(prefab, 1);
            return _pools[prefab].Take();
        }

        public T TakeObject(int prefabIndex)
        {
            return TakeObject(_startPoolDataList[prefabIndex].prefab);
        }

        public override void Init()
        {
            _pools = new Dictionary<T, IObjectPool<T>>();
            _startPoolDataList.ForEach(poolData =>
            {
                CheckForObjectPool(poolData.prefab, poolData.startSize);
            });
        }

        public override void Clear()
        {
            foreach (var pool in _pools)
            {
                pool.Value.Clear();
            }

            _pools.Clear();
        }

        private void CheckForObjectPool(T prefab, int startSize)
        {
            if (_pools.ContainsKey(prefab)) return;

            _pools[prefab] = CreateObjectPool(prefab, startSize);
        }

        [System.Serializable]
        private struct PoolData
        {
            public T prefab;
            [Min(0)] public int startSize;
        }
    }
}