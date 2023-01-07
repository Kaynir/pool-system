using UnityEngine;

namespace Kaynir.Pools
{
    public class SimpleFactory<T> : BaseFactory<T> where T : MonoBehaviour
    {
        [SerializeField] protected T _prefab = null;

        protected MonoPool<T> _pool;

        public T TakeObject()
        {
            return _pool.Take();
        }

        protected override void Initialize()
        {
            _pool = CreatePool(_prefab);
        }
    }
}