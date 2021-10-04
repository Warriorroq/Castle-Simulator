using System.Collections.Generic;
using UnityEngine;
using UnitSpace.Interfaces;
namespace UnitSpace
{
    public class UnitOrderUser : MonoBehaviour
    {
        private Queue<IOrder> _orders;
        private IOrder _currentOrder;
        private Unit _owner;
        public void ClearOrders()
            => _orders.Clear();
        public void AddOrder(IOrder order)
            => _orders.Enqueue(order);
        private void Awake()
        {
            _orders = new Queue<IOrder>();
            TryGetComponent(out _owner);
        }
        private void Update()
        {
            if (!(_currentOrder is null))
                UpdateOrder();
            else
                DequeueNextOrder();
        }
        private void UpdateOrder()
        {
            if (_currentOrder.GetState() != IOrder.OrderState.Finished)
                _currentOrder.UpdateOrder();
            else
                _currentOrder = null;
        }
        private void DequeueNextOrder()
        {
            if (_orders.Count > 0)
                TakeOrder(_orders.Dequeue());
        }
        private void TakeOrder(IOrder order)
        {
            _currentOrder = order;
            _currentOrder.SetUnitOwner(_owner);
            _currentOrder.StartOrder();
        }
    }
}
