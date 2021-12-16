namespace UnitSpace.Interfaces
{
    public interface IPool<T> where T : IPoolable
    {
        public abstract T TakeOne();
        public abstract bool IsEmpty();
        public abstract void PushOne(T obj);
        public abstract void CreateSomeForPull(int count);
        public abstract void Reflesh();
    }
}
