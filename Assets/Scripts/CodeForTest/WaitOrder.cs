using System.Collections;
using System.Collections.Generic;
using UnitSpace;
using UnitSpace.Interfaces;
using UnityEngine;

namespace UnitSpace.Orders
{
    public class WaitOrder : Order
    {
        private IOrder.OrderState _state;
        private float _timer;
        private float time;
        public WaitOrder(float seconds)
        {
            time = seconds;
        }
        public IOrder.OrderState GetState()
            => _state;
        public override void UpdateOrder()
        {
            if (_timer > time)
                EndOrder();
            _timer += Time.deltaTime;
        }
    }
}
