namespace Logic
{
    public interface IDictionaryPool<in T1, T2>
    {
        public T2 Get(T1 key);
        public bool TryGet(T1 key, out T2 value);
        public T2 Release(T1 key);
        public bool HaveInPool(T1 key);
        public bool IsActive(T1 key);
    }
}