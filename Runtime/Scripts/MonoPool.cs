using System.Collections.Generic;
using UnityEngine;

namespace Kaynir.Pools
{
    public class MonoPool<T> where T : MonoBehaviour
    {
        public delegate void PoolAction(T obj);

        private T _prefab;
        private Stack<T> _stack;
        private PoolAction _onTaken;
        private PoolAction _onReleased;

        public MonoPool(T prefab, PoolAction onTaken, PoolAction onReleased, int startCount)
        {
            _stack = new Stack<T>();
            _prefab = prefab;
            _onTaken = onTaken;
            _onReleased = onReleased;

            Expand(startCount);
        }

        public MonoPool(T prefab) : this(prefab, null, null, 0) { }
        public MonoPool(T prefab, int startCount) : this(prefab, null, null, startCount) { }
        public MonoPool(T prefab, PoolAction onTaken, PoolAction onReleased) : this(prefab, onTaken, onReleased, 0) { }

        public T Take()
        {
            if (_stack.Count == 0) Expand();

            T obj = _stack.Pop();
            obj.gameObject.SetActive(true);
            
            _onTaken?.Invoke(obj);
            return obj;
        }

        private void Release(T obj)
        {
            obj.gameObject.SetActive(false);

            _onReleased?.Invoke(obj);
            _stack.Push(obj);
        }

        private T CreateObject()
        {
            T obj = Object.Instantiate(_prefab);
            
            if (!obj.TryGetComponent(out PoolableObject poolable))
            {
                poolable = obj.gameObject.AddComponent<PoolableObject>();
            }

            poolable.Initialize(() => Release(obj));
            return obj;
        }

        private void Expand(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Release(CreateObject());
            }
        }

        private void Expand() => Expand(1);
    }
}