using UnityEngine;

namespace Kaynir.Pools
{
    public static class PoolExtensions
    {
        public static void Release(this GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out PoolableObject poolable))
            {
                poolable.Release();
            }

            Object.Destroy(gameObject);
        }

        public static void Release(this GameObject gameObject, float delayInSeconds)
        {
            if (gameObject.TryGetComponent(out PoolableObject poolable))
            {
                poolable.Release(delayInSeconds);
                return;
            }

            Object.Destroy(gameObject, delayInSeconds);
        }
    }
}