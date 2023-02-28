using UnityEngine;

namespace Kaynir.Pools
{
    public abstract class AbstractFactory<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private Transform _objectParent = null;
        [SerializeField, Min(1)] private int _startPoolSize = 0;

        private void Awake() => Init();

        protected abstract void Init();

        protected virtual IObjectPool<T> CreateObjectPool(T prefab)
        {
            return new ObjectPool<T>(prefab,
                                     OnObjectTaken,
                                     OnObjectReleased,
                                     _startPoolSize);
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
    }
}