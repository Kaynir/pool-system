using System;
using System.Collections;
using UnityEngine;

namespace Kaynir.Pools
{
    public class PoolableObject : MonoBehaviour
    {
        private Action _poolAction;

        public void Initialize(Action poolAction) => _poolAction = poolAction;

        public void Release()
        {
            if (_poolAction == null)
            {
                Debug.LogWarning($"Failed to release {name}. Pool action is not assigned.");
                return;
            }

            _poolAction.Invoke();
        }

        public void Release(float delayInSeconds)
        {
            StopAllCoroutines();
            StartCoroutine(ReleaseRoutine(delayInSeconds));
        }

        private IEnumerator ReleaseRoutine(float delayInSeconds)
        {
            for (float t = 0; t < delayInSeconds; t += Time.deltaTime)
            {
                yield return null;
            }

            Release();
        }
    }
}