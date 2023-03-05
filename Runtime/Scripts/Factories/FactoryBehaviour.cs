using UnityEngine;

namespace Kaynir.Pools
{
    public abstract class FactoryBehaviour<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private Transform _objectParent = null;

        public abstract void Init();
        public abstract void Clear();

        protected IObjectPool<T> CreateObjectPool(T prefab, int startSize)
        {
            return new ObjectPool<T>(prefab,
                                     OnObjectTaken,
                                     OnObjectReleased,
                                     OnObjectDestroyed,
                                     startSize);
        }

        protected virtual void OnObjectTaken(T obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(_objectParent);
        }

        protected virtual void OnObjectReleased(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_objectParent);
        }

        protected virtual void OnObjectDestroyed(T obj) { }
    }
}