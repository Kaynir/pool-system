using UnityEngine;

namespace Kaynir.Pools
{
    public class SimpleFactory<T> : AbstractFactory<T> where T : Component
    {
        [SerializeField] private T _prefab = null;

        private IObjectPool<T> _pool;

        public T TakeObject()
        {
            return _pool.Take();
        }

        protected override void Init()
        {
            _pool = CreateObjectPool(_prefab);
        }
    }
}