namespace UnitSpace.Interfaces
{
    public interface IPoolable
    {
        public abstract void GetReadyForPull();
        public abstract void SetReadyForPull();
    }
}
