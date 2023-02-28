using System;
using UnityEngine;

namespace Kaynir.Pools
{
    public class PoolableObject : MonoBehaviour, IPoolable
    {
        private Action _poolAction;

        public void Init(Action poolAction) => _poolAction = poolAction;

        public void Release()
        {
            if (_poolAction == null)
            {
                Debug.LogWarning($"Failed to release {name}. Pool action is not assigned.");
                return;
            }

            _poolAction.Invoke();
        }
    }
}