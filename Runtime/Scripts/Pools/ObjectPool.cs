using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.Pools
{
    public class ObjectPool<T> : IObjectPool<T> where T : Component
    {
        public delegate void PoolAction(T obj);

        private Stack<T> _stack;
        private T _prefab;
        private PoolAction _onTaken;
        private PoolAction _onReleased;
        private PoolAction _onDestroyed;

        public ObjectPool(T prefab, PoolAction onTaken, PoolAction onReleased, PoolAction onDestroyed, int startCount)
        {
            _stack = new Stack<T>();
            _prefab = prefab;
            _onTaken = onTaken;
            _onReleased = onReleased;
            _onDestroyed = onDestroyed;

            Expand(startCount);
        }

        public ObjectPool(T prefab, int startCount) : this(prefab, null, null, null, startCount) { }

        public void Release(T obj)
        {
            _onReleased?.Invoke(obj);
            _stack.Push(obj);
        }

        public T Take()
        {
            if (!_stack.TryPop(out T obj))
            {
                obj = CreateObject();
            }

            _onTaken?.Invoke(obj);
            return obj;
        }

        public void Clear()
        {
            for (int i = 0; i < _stack.Count; i++)
            {
                T obj = _stack.Pop();
                
                _onDestroyed?.Invoke(obj);

                Object.Destroy(obj.gameObject);
            }
        }

        private T CreateObject()
        {
            T obj = Object.Instantiate(_prefab);
            
            if (!obj.TryGetComponent(out IPoolable poolable))
            {
                poolable = obj.gameObject.AddComponent<PoolableObject>();
            }

            poolable.Init(() => Release(obj));
            return obj;
        }

        private void Expand(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Release(CreateObject());
            }
        }
    }
}