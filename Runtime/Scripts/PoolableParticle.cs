using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Kaynir.Pools
{
    [RequireComponent(typeof(ParticleSystem))]
    public class PoolableParticle : PoolableObject
    {
        private ParticleSystem _system;
        private Transform _transform;
        private MainModule _main;
        
        private void Awake()
        {
            _system = GetComponent<ParticleSystem>();
            _transform = transform;
            _main = _system.main;
            _main.stopAction = ParticleSystemStopAction.Callback;
        }

        public void Play(Vector3 position, MinMaxGradient gradient)
        {
            _main.startColor = gradient;
            _transform.position = position;
            _system.Play(true);
        }

        public void Play(Vector3 position) => Play(position, _main.startColor);

        public void Play() => Play(_transform.position, _main.startColor);

        private void OnParticleSystemStopped() => Release();
    }
}