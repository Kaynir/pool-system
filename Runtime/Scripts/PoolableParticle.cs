using UnityEngine;

namespace Kaynir.Pools
{
    [RequireComponent(typeof(ParticleSystem))]
    public class PoolableParticle : PoolableObject
    {
        private void Awake()
        {
            ParticleSystem system = GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = system.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            Release();
        }
    }
}