using UnityEngine;

namespace Kaynir.Pools
{
    public class SimpleFactory<T> : FactoryBehaviour<T> where T : Component
    {
        [SerializeField] private T _prefab = null;
        [SerializeField, Min(0)] private int _startSize = 0;

        private IObjectPool<T> _pool;

        public T TakeObject()
        {
            if (_pool == null) Init();
            
            return _pool.Take();
        }

        public override void Init()
        {
            _pool = CreateObjectPool(_prefab, _startSize);
        }

        public override void Clear()
        {
            _pool.Clear();
        }
    }
}