using UnityEngine;

namespace Kaynir.Pools
{
    public static class PoolExtensions
    {
        public static void Release(this GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IPoolable poolable))
            {
                poolable.Release();
                return;
            }

            Object.Destroy(gameObject);
        }
    }
}