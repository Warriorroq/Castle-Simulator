using System.Collections.Generic;
using UnityEngine;
using UnitSpace.Interfaces;
using UnityEngine.Events;
using UnitSpace.Orders;

namespace UnitSpace
{
    public class UnitOrderUser : MonoBehaviour
    {
        public UnityEvent<Order> doOrder;
        private Queue<Order> _orders;
        private Order _currentOrder;
        private Unit _owner;
        public void ClearOrders()
            => _orders.Clear();
        public void AddOrder(Order order)
            => _orders.Enqueue(order);
        public void StopImmediate()
            => _currentOrder = null;
        public void StopOrder()
            =>_currentOrder?.EndOrder();
        public void AddToStart(params Order[] tasks)
        {
            var lastOrders = _orders;
            _orders = new Queue<Order>(tasks);
            foreach(var lastOrder in lastOrders)
                _orders.Enqueue(lastOrder);
        }
        private void Awake()
        {
            _orders = new Queue<Order>();
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
            {
                _currentOrder = null;
            }
        }
        private void DequeueNextOrder()
        {
            if (_orders.Count > 0)
                TakeOrder(_orders.Dequeue());
        }
        private void TakeOrder(Order order)
        {
            _currentOrder = order;
            _currentOrder.SetUnitOwner(_owner);
            _currentOrder.StartOrder();
            doOrder?.Invoke(order);
        }
    }
}
