using System;

namespace Kaynir.Pools
{
    public interface IPoolable
    {
        void Init(Action poolAction);
        void Release();
    }
}