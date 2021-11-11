using UnitSpace.Interfaces;
namespace UnitSpace.Orders
{
    public abstract class Order : IOrder
    {
        protected IOrder.OrderState _state;
        protected Unit _owner;
        public virtual void EndOrder()
        {
            _state = IOrder.OrderState.Finished;
        }
        public virtual IOrder.OrderState GetState()
            => _state;
        public virtual void SetUnitOwner(Unit owner)
        {
            _state = IOrder.OrderState.Ready;
            _owner = owner;
        }
        public virtual void StartOrder()
        {
            _state = IOrder.OrderState.InProgress;
        }

        public void UpdateOrder(){
            if (!_owner){
                EndOrder();
                return;
            }
            OnUpdateOrder();
        }
        protected virtual void OnUpdateOrder(){}
    }
}
