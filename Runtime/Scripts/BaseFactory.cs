using UnityEngine;

namespace Kaynir.Pools
{
    public abstract class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected Transform _objectParent = null;
        [SerializeField, Min(0)] protected int _startPoolSize = 0;

        private void Awake() => Initialize();

        protected abstract void Initialize();

        protected MonoPool<T> CreatePool(T prefab)
        {
            return new MonoPool<T>(prefab,
                                   OnObjectTaken,
                                   OnObjectReleased,
                                   _startPoolSize);
        }

        protected virtual void OnObjectTaken(T obj)
        {
            obj.transform.SetParent(_objectParent);
        }

        protected virtual void OnObjectReleased(T obj)
        {
            obj.transform.SetParent(_objectParent);
        }
    }
}