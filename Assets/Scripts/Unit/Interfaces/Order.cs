namespace UnitSpace.Interfaces
{
    public interface IOrder : ISetUnitOwner
    {
        public enum OrderState
        {
            Ready,
            InProgress,
            Finished
        }
        public OrderState GetState();
        public abstract void StartOrder();
        public abstract void UpdateOrder();
        public abstract void EndOrder();
    }
}
