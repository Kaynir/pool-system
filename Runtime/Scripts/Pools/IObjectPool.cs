namespace Kaynir.Pools
{
    public interface IObjectPool<T>
    {
        T Take();
        void Release(T obj);
        void Clear();
    }
}